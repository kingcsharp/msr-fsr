using System;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Companies.Procedures;
using Msr.Services.Companies.ViewModels;

namespace Msr.Services.Orders
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
            return _dbContext.CompanyView;
        }
        public Company GetCompanyById(string Id)
        {
            return _dbContext.Companies.Where(x => x.ID == Id).SingleOrDefault();
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
        public bool Edit(AddCompanyViewModel model)
        {
            try
            {
                var saveCompanyProcedure = new AddCompanyProcedure { Name = model.Name, Type = model.CoType, ParentType = model.Parent, Phone = model.Phone,/*HeadPeople=model.ReferenceFiles,LocationId=model.Location,CoSupProds=model.RootCoID,PicFiles=model.RootCoID, LogoFiles = model.RootCoID, ReferenceFiles = model.RootCoID,*/ NewPersonLogin = model.NewPersonLogin, NewPersonPassword = model.NewPersonPassword, NewPersonFirstName = model.NewPersonFirstName, NewPersonLastName = model.NewPersonLastName, NewPersonEmail = model.NewPersonEmail, StrNTlogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveCompanyProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
    }
}
