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
using Msr.Models.Locations;
using Msr.Models.Companies;

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
                var saveCompanyProcedure = new AddCompanyProcedure { Name = model.Name, Type = model.CoType, ParentType = model.Parent, Phone = model.Phone,/*HeadPeople=model.ReferenceFiles,LocationId=model.Location,CoSupProds=model.RootCoID,PicFiles=model.RootCoID, LogoFiles = model.RootCoID, ReferenceFiles = model.RootCoID,*/ NewPersonLogin = model.NewPersonLogin, NewPersonPassword = model.NewPersonPassword, NewPersonFirstName = model.NewPersonFirstName, NewPersonLastName = model.NewPersonLastName, NewPersonEmail = model.NewPersonEmail, StrNTlogin = model.NTLogin };

                var task = _dbContext.Database.ExecuteStoredProcedure<AddCompanyProcedure>(saveCompanyProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Edit(EditCompanyViewModel model)
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
    }
}
