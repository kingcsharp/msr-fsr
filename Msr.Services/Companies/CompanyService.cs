using System;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Repositories;
using Msr.Services.Companies.Procedures;
using Msr.Services.Companies.ViewModels;
using Msr.Models.Documents;
using System.Collections.Generic;
using System.Data;
using System.Web;
using ExcelDataReader;
using Msr.Models.Locations;
using Msr.Models.Companies;
using Msr.Services.Documents;
using Msr.Services.Users.Messages;

namespace Msr.Services.Companies
{
    public class CompanyService
    {
        private readonly MsrDbContext _dbContext;

        public CompanyService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<CompanyView> GetCompaniesQueryable()
        {
            return _dbContext.CompanyViews;
        }

        public IQueryable<ApprovedCompaniesView> GetCompaniesApprovedQueryable(string co)
        {
            return _dbContext.ApprovedCompaniesViews.Where(x => x.CoType == "DEPARTMENT" && x.CreatingCo == co || x.CoType == "COMPANY");
        }

        public CompanyView GetCompanyById(string Id)
        {
            return _dbContext.CompanyViews.Where(x => x.ObjectId == Id).SingleOrDefault();
        }

        public EditCompanyViewModel GetCompanyByObjId(string Id)
        {
            EditCompanyViewModel editCompanyViewModel = new EditCompanyViewModel();
            var getComapny = _dbContext.CompanyViews.Where(x => x.ObjectId == Id).SingleOrDefault();
            editCompanyViewModel.Id = getComapny.Id;
            editCompanyViewModel.ObjectId = getComapny.ObjectId;
            editCompanyViewModel.Name = getComapny.Name;
            editCompanyViewModel.Parent = getComapny.Parent;
            editCompanyViewModel.ParentName = getComapny.ParentName;
            editCompanyViewModel.Phone = getComapny.Phone;
            editCompanyViewModel.CoType = getComapny.CoType;
            editCompanyViewModel.Location = getComapny.Location;
            editCompanyViewModel.LocationName = getComapny.LocationName;
            return editCompanyViewModel;
        }       

        public bool Create(AddCompanyViewModel model)
        {
            try
            {
                var saveCompanyProcedure = new AddCompanyProcedure
                {
                    Name = model.Name,
                    Type = model.CoType,
                    ParentType = model.Parent,
                    Phone = model.Phone,
                    PicFiles = model.PictureFiles != null ? string.Join(", ", model.PictureFiles) : "",
                    LogoFiles = model.LogoFiles != null ? string.Join(", ", model.LogoFiles) : "",
                    ReferenceFiles = model.ReferenceFiles != null ? string.Join(", ", model.ReferenceFiles).Replace(",", ", ") : "",
                    NewPersonLogin = model.NewPersonLogin,
                    NewPersonPassword = model.NewPersonPassword,
                    NewPersonFirstName = model.NewPersonFirstName,
                    NewPersonLastName = model.NewPersonLastName,
                    NewPersonEmail = model.NewPersonEmail,
                    StrNTlogin = model.NTLogin
                };

                var task = _dbContext.Database.ExecuteStoredProcedure<AddCompanyProcedure>(saveCompanyProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Update(EditCompanyViewModel model)
        {
            try
            {
                var documentFilesService = new DocumentFilesService();

                var refFile = string.Join(", ", documentFilesService.GetDocByObjectId(model.ObjectId).Select(x => x.LINKED_DOC_ID).ToList());

                var saveCompanyProcedure = new AddCompanyProcedure
                {
                    ObjID = model.ObjectId,
                    Name = model.Name,
                    Type = model.CoType,
                    ParentType = model.Parent,
                    Phone = model.Phone,
                    HeadPeople = model.HeadPeople,
                    LocationId = model.Location,
                    CoSupProds = model.RootCoID,
                    PicFiles = model.PictureFiles != null ? string.Join(", ", model.PictureFiles) : "",
                    LogoFiles = model.LogoFiles != null ? string.Join(", ", model.LogoFiles) : "",
                    ReferenceFiles = refFile,
                    NewPersonLogin = model.NewPersonLogin,
                    NewPersonPassword = model.NewPersonPassword,
                    NewPersonFirstName = model.NewPersonFirstName,
                    NewPersonLastName = model.NewPersonLastName,
                    NewPersonEmail = model.NewPersonEmail,
                    StrNTlogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveCompanyProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }        

        public IEnumerable<DocumentFilesView> GetFilesByType(string type)
        {
            return _dbContext.DocumentFilesViews.Where(t => t.TYPE == type).ToList();
        }

        public IQueryable<Msr.Models.Companies.HeadPeopleView> GetHeadPeople()
        {
            return _dbContext.HeadPeopleViews;
        }

        public IQueryable<LocationView> GetLocationsQueryable()
        {            
                return _dbContext.LocationViews;            
        }

        public bool Delete(string objectId, string loginId)
        {
            try
            {
                var deleteCompanyProcedure = new DeleteCompanyProcedure
                {
                    ObjID = objectId,                   
                    StrNTlogin = loginId
                };

                _dbContext.Database.ExecuteStoredProcedure(deleteCompanyProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public ResultNotification<List<CompanyImportViewModel>> ImportCompanies(HttpPostedFileBase postedFile, LoggedUserIdResult ntLogin)
        {
            var result = new ResultNotification<List<CompanyImportViewModel>>
            {
                Entity = new List<CompanyImportViewModel>()
            };

            try
            {
                if (!postedFile.FileName.EndsWith(".csv"))
                {
                    result.AddError("The import file must be a tab delimited text file");
                    return result;
                }

                var reader = ExcelReaderFactory.CreateCsvReader(postedFile.InputStream);

                var resultAsDataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                reader.Close();

                var columnNames = (from dc in resultAsDataSet.Tables[0].Columns.Cast<DataColumn>()
                                   select dc.ColumnName).ToList();

               var primes = CompanyImportViewModel.GetHeaderColumns();

                var results = primes.Where(m => !columnNames.Contains(m));

                bool isSubset = primes.Intersect(columnNames).Count() == primes.Count();

                if (!isSubset)
                {
                    result.AddError("Coloums missing : (" + string.Join(",", results) + ") to create Company");
                    return result;
                }

                var list = Enumerable.Select(resultAsDataSet.Tables[0].AsEnumerable(), item => new CompanyImportViewModel()
                {
                    Id = item["Id"].ToString(),
                    Name = item["Name"].ToString(),
                    Address = item["Address"].ToString(),
                    City = item["City"].ToString(),
                    State = item["State"].ToString(),
                    Zip = item["Zip"].ToString(),
                    Country = item["Country"].ToString(),
                    ShipName = item["ShipName"].ToString(),
                    ShipAddress = item["ShipAddress"].ToString(),
                    ShipCity = item["ShipCity"].ToString(),
                    ShipState = item["ShipState"].ToString(),
                    ShipZip = item["ShipZip"].ToString(),
                    ShipCountry = item["ShipCountry"].ToString(),
                    Phone = item["Phone"].ToString(),
                    LinkedId = item["LinkedId"].ToString(),

                }).ToList();

                foreach (var item in list)
                {
                    if (string.IsNullOrWhiteSpace(item.Id))
                    {
                        item.Messages.Add("Id is required");
                    }

                    if (string.IsNullOrWhiteSpace(item.Name))
                    {
                        item.Messages.Add("Name is required");
                    }
                 
                    if (item.Messages.Any())
                    {
                        result.Entity.Add(item);
                        continue;
                    }

                    var importProcedure = new CompaniesImportExternalProcedure();
                    importProcedure.Id = item.Id;
                    importProcedure.Name = item.Name;
                    importProcedure.Address = item.Address;
                    importProcedure.City = item.City;
                    importProcedure.State = item.State;
                    importProcedure.Zip = item.Zip;
                    importProcedure.Country = item.Country;
                    importProcedure.ShipName = item.ShipName;
                    importProcedure.ShipAddress = item.ShipAddress;
                    importProcedure.ShipCity = item.ShipCity;
                    importProcedure.ShipState = item.ShipState;
                    importProcedure.ShipZip = item.ShipZip;
                    importProcedure.ShipCountry = item.ShipCountry;
                    importProcedure.Phone = item.Phone;
                    importProcedure.SupplierId = ntLogin.Root_Company;
                    importProcedure.LinkedId = item.LinkedId;
                    importProcedure.StrNtLogin = ntLogin.Id;
                    _dbContext.Database.ExecuteStoredProcedure(importProcedure);

                    item.Processed = true;
                    item.Messages.Add(importProcedure.NewId);
                    result.Entity.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        public List<PersonRootCompanyTreeView> GetPersonRootCompanyTreeViews(string ntlogin)
        {
            return _dbContext.Database.SqlQuery<PersonRootCompanyTreeView>($"Exec A_SP_COMPANIES_SHOW_PERSONS_ROOT_COMPANY_TREE '{ntlogin}','{ntlogin}'").ToList();
        }
    }
}
