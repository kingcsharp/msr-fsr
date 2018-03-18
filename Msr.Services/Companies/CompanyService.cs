using System;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Companies.Procedures;
using Msr.Services.Companies.ViewModels;
using System.Data.SqlClient;
using Msr.Models.Documents;
using System.Collections.Generic;
using System.Data;
using System.Web;
using ExcelDataReader;
using Msr.Models.Locations;
using Msr.Models.Companies;
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
                    ReferenceFiles = model.ReferenceFiles != null ? string.Join(", ", model.ReferenceFiles) : "",
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
                var saveCompanyProcedure = new AddCompanyProcedure
                {
                    ObjID = model.ObjectId,
                    Name = model.Name,
                    Type = model.CoType,
                    ParentType = model.Parent,                    
                    Phone = model.Phone,
                    HeadPeople =model.HeadPeople,
                    LocationId =model.Location,
                    CoSupProds =model.RootCoID,
                    PicFiles = model.PictureFiles != null ? string.Join(", ", model.PictureFiles) : "",
                    LogoFiles = model.LogoFiles != null ? string.Join(", ", model.LogoFiles) : "",
                    ReferenceFiles = model.ReferenceFiles !=null ? string.Join(", ", model.ReferenceFiles) : "",
                    NewPersonLogin = model.NewPersonLogin,
                    NewPersonPassword = model.NewPersonPassword, 
                    NewPersonFirstName = model.NewPersonFirstName,
                    NewPersonLastName = model.NewPersonLastName, 
                    NewPersonEmail = model.NewPersonEmail,
                    StrNTlogin = model.NTLogin };

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

        public ResultNotification<string> ImportCompanies(HttpPostedFileBase postedFile, LoggedUserIdResult ntLogin)
        {
            var result = new ResultNotification<string>();
            try
            {
                var stream = postedFile.InputStream;

                IExcelDataReader reader;


                if (postedFile.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (postedFile.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    result.AddError("This file format is not supported");
                    return result;
                }
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
                string[] primes = { "Id", "Name", "Address", "City", "State", "Zip", "Country", "ShipName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "ShipCountry", "Phone", "LinkedId" };

                var results = primes.Where(m => !columnNames.Contains(m));
                bool isSubset = primes.Intersect(columnNames).Count() == primes.Count();
                if (!isSubset)
                {
                    result.AddError("Coloums missing : (" + string.Join(",", results) + ") to create Company");
                    return result;
                }

                var modelList = Enumerable.Select(resultAsDataSet.Tables[0].AsEnumerable(), item => new CompanyImportViewModel()
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

                if (modelList.Count > 0)
                {
                    foreach (var model in modelList)
                    {
                        var companiesImportExternalProcedure = new CompaniesImportExternalProcedure();
                        if (!string.IsNullOrWhiteSpace(model.Name))
                        {
                            companiesImportExternalProcedure.Id = model.Id;
                            companiesImportExternalProcedure.Name = model.Name;
                            companiesImportExternalProcedure.Address = model.Address;
                            companiesImportExternalProcedure.City = model.City;
                            companiesImportExternalProcedure.State = model.State;
                            companiesImportExternalProcedure.Zip = model.Zip;
                            companiesImportExternalProcedure.Country = model.Country;
                            companiesImportExternalProcedure.ShipName = model.ShipName;
                            companiesImportExternalProcedure.ShipAddress = model.ShipAddress;
                            companiesImportExternalProcedure.ShipCity = model.ShipCity;
                            companiesImportExternalProcedure.ShipState = model.ShipState;
                            companiesImportExternalProcedure.ShipZip = model.ShipZip;
                            companiesImportExternalProcedure.ShipCountry = model.ShipCountry;
                            companiesImportExternalProcedure.Phone = model.Phone;
                            companiesImportExternalProcedure.SupplierId = ntLogin.Root_Company;
                            companiesImportExternalProcedure.LinkedId = model.LinkedId;
                            companiesImportExternalProcedure.StrNtLogin = ntLogin.Id;
                            _dbContext.Database.ExecuteStoredProcedure(companiesImportExternalProcedure);

                            result.SuccessMessage = companiesImportExternalProcedure.NewId;
                        }
                        else
                        {
                            result.AddError("Part Name is empty");
                        }
                    }
                }
                else
                {
                    result.AddError("Nothing to upload file is empty");
                }
                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }
    }
}
