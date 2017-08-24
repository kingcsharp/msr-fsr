using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Msr.Infrastructure.Helpers;
using Msr.Models.Parts;
using Msr.Models.People;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;
using Msr.Services.Parts.Procedures;
using Msr.Services.People.Procedures;
using Msr.Services.People.ViewModels;

namespace Msr.Services.Orders
{
   public class PeopleService
    {
        private readonly MsrDbContext _dbContext;

        public PeopleService()
        {
            _dbContext = new MsrDbContext();
        }

       public bool CheckUserExists(string login, string password)
       {
           var user = _dbContext.ApprovedPeoples.SingleOrDefault(x => x.Login == login && password == password);

           if (user != null && user.SystemStatus != "ACTIVE")
           {
               return false;
           }

           return true;

       }

       public CheckLoginResult GetAnswerUser(string login, string password)
        {
            var user = _dbContext.Peoples.SingleOrDefault(x => x.Login == login && x.Status.Contains("APPROVED"));

           if (user == null) return null;

           var loginParm = new SqlParameter("@Login", login);
           var passwordParm = new SqlParameter("@Password", AuthenticationHelper.PassWordEncrypt(password));
           var passwordNonEncParm = new SqlParameter("@PASSWORD_NON_ENCRYPT", password);

           var result = _dbContext.Database.SqlQuery<CheckLoginResult>("Portal_Check_Login @Login, @Password,@PASSWORD_NON_ENCRYPT", loginParm,
               passwordParm, passwordNonEncParm).SingleOrDefault();

           if (result != null)
           {
               var portalAccount = _dbContext.AspNetUsers.SingleOrDefault(x => x.UserName.ToLower() == login.ToLower());

               if (portalAccount == null)
               {
                   var context = new ApplicationDbContext();

                   var store = new UserStore<ApplicationUser>(context);
                   var usermanager = new UserManager<ApplicationUser>(store);
                   var id = Guid.NewGuid().ToString();
                   var newUser = new ApplicationUser
                   {
                       UserName = login,
                       Id = id,
                       FirstName = result.Name,
                       LastName = result.LastName,
                       TimeZone = "10000", // dummy id for Answer Users
                       IsActive = true,
                       CreatedDate = DateTime.UtcNow,
                       AnswerId = result.Id

                   };

                   usermanager.Create(newUser, "msr" + result.Id + "$");
                   usermanager.AddToRole(id, RolesConstants.AnswerUser);
                   context.SaveChanges();
               }
               else
               {
                   return new CheckLoginResult
                   {
                       Id = result.Id,
                       Login = login
                   };
               }

           }

           return result;
        }
        public IQueryable<PeopleObjectView> GetPeople()
        {
            return _dbContext.PeopleObjectViews;
        }
        public IQueryable<PeopleObjectView> GetApprovedPeople()
        {
            return _dbContext.PeopleObjectViews.Where(x=>x.Status =="APPROVED").AsQueryable();
        }
        public IQueryable<LanguagesView> GetLanguages()
        {
            return _dbContext.LanguagesViews;
        }
        public IQueryable<TimeZonesView> GetTimeZones()
        {
            return _dbContext.TimeZonesViews.OrderBy(x => x.Num);
        }
        public IQueryable<OfficialPositionView> GetOfficialPosition()
        {
            return _dbContext.OfficialPositionViews;
        }
        public List<CompanyEditPersonView> GetCompanyEditPersons()
        {
            var result = _dbContext.Database.SqlQuery<CompanyEditPersonView>("select distinct * from A_V_COMPANIES_DROP_SEARCH").ToList();
            return result;
        }
        public List<BossInfoView> GetBossInfo()
        {
            var result = _dbContext.Database.SqlQuery<BossInfoView>("SELECT DISTINCT TOP 500 FULL_NAME as FullName,ID as Id,LAST_NAME,NAME FROM A_V_PEOPLE_DATA_QUICK ORDER BY LAST_NAME,NAME").ToList();
            return result;
        }
        public bool Create(AddPeopleViewModel model)
        {
            try
            {
                var savePeopleProcedure = new SavePeopleProcedure
                {
                    Name = model.FirstName,
                    Login = model.LoginId,
                    LastName = model.LastName,
                    ScreenType = model.ScreenType,
                    Language = model.LanguageCode,
                    IsHead = model.IsDepartmentHead,
                    Position = model.OfficialPosition,
                    Boss = model.BossName,
                    TimeZone = model.TimeZone,
                    HireDate = model.HireDate,
                    Status = model.StatusEditPerson,
                    StrNTlogin = model.NTLogin,
                    Company = model.CompanyEditPerson
                };
               
                var result = _dbContext.Database.ExecuteStoredProcedure<SavePeopleProcedure>(savePeopleProcedure);

                var savePasswrodProcedure = new SavePasswrodProcedure
                {
                    ObjID = savePeopleProcedure.NewObjId,
                    Password = model.Password,
                    StrNTlogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure<SavePasswrodProcedure>(savePasswrodProcedure);

                var deletePictureFileProcedure = new DeleteFileProcedure() { ObjID = savePeopleProcedure.NewObjId, Type = "PICTURE", NTLogin = model.NTLogin };
                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);

                foreach (var file in model.PictureFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = savePeopleProcedure.NewObjId, DocID = file, Type = "PICTURE", NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = savePeopleProcedure.NewObjId, Type = null, NTLogin = model.NTLogin };
                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = savePeopleProcedure.NewObjId, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                _dbContext.Database.SqlQuery<AddPeopleViewModel>(
                    "INSERT INTO A_PHONE_NUMBERS(ID, PHONE_NUMBER, PHONE_TYPE, PHONE_PIN, PHONE_EXTENSION, MODBY, DRCM, OBJECT_ID)VALUES(newID(), '" +
                    model.PrimaryPhoneNumber + "', '" + model.TypePrimaryPhoneNumber + "', '" +
                    model.ExtPrimaryPhoneNumber + "', '" + model.PinPrimaryPhoneNumber + "', '" + model.NTLogin +
                    "', getDate(), '" + savePeopleProcedure.NewObjId + "')").SingleOrDefault();
                _dbContext.Database.SqlQuery<AddPeopleViewModel>(
                    "INSERT INTO A_EMAILS (ID,ADDY,[TYPE],EMAIL_TYPE,MODBY,DRCM,OBJECT_ID)VALUES (newID(), '" +
                    model.EmailPrimary + "', '" + model.EmailTypePrimary + "', '" +
                    model.EmailTextTypePrimary + "', '" + model.NTLogin +
                    "', getDate(), '" + savePeopleProcedure.NewObjId + "')").SingleOrDefault();
                _dbContext.Database.SqlQuery<AddPeopleViewModel>(
                    "INSERT INTO A_LOCATIONS_OBJECT_LINK (ID,LOCATION_ID,[LOCATION_TYPE],MODBY,DRCM,OBJECT_ID)VALUES (newID(), '" +
                    model.AddressLocation + "', '" + model.AddressType + "', '" + model.NTLogin +
                    "', getDate(), '" + savePeopleProcedure.NewObjId + "')").SingleOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public PeopleObjectView GetPeopleById(string id)
        {
            return _dbContext.PeopleObjectViews.Where(x => x.ObjectId == id).SingleOrDefault();
        }
        public EditPeopleViewModel GetPhoneInfoByObjId(string id)
        {
            var result = _dbContext.Database
                .SqlQuery<EditPeopleViewModel>(
                    "SELECT PHONE_NUMBER as PrimaryPhoneNumber,PHONE_TYPE as TypePrimaryPhoneNumber,PHONE_PIN as PinPrimaryPhoneNumber,PHONE_EXTENSION as ExtPrimaryPhoneNumber FROM dbo.A_PHONE_NUMBERS WHERE OBJECT_ID = '" + id + "'")
                .FirstOrDefault();
            return result;
        }
        public EditPeopleViewModel GetEmailInfoByObjId(string id)
        {
            var result = _dbContext.Database
                .SqlQuery<EditPeopleViewModel>(
                    "SELECT ID as EmailIdPrimary, ADDY as EmailPrimary,TYPE as EmailTypePrimary,EMAIL_TYPE as EmailTextTypePrimary FROM dbo.A_EMAILS WHERE OBJECT_ID = '" + id + "'")
                .FirstOrDefault();
            return result;
        }
        public EditPeopleViewModel GetLocationInfoByObjId(string id)
        {
            var result = _dbContext.Database
                .SqlQuery<EditPeopleViewModel>(
                    "SELECT LOCATION_ID as AddressLocation,LOCATION_TYPE as AddressType FROM A_LOCATIONS_OBJECT_LINK WHERE OBJECT_ID = '" + id + "'")
                .FirstOrDefault();
            return result;
        }
        public bool Edit(EditPeopleViewModel model)
        {
            try
            {
                var savePeopleProcedure = new EditPeopleProcedure
                {
                    ObjID = model.ObjectId,
                    Name = model.FirstName,
                    Login = model.LoginId,
                    LastName = model.LastName,
                    ScreenType = model.ScreenType,
                    Language = model.LanguageCode,
                    IsHead = model.IsDepartmentHead,
                    Position = model.OfficialPosition,
                    Boss = model.BossName,
                    TimeZone = model.TimeZone,
                    HireDate = model.HireDate,
                    Status = model.StatusEditPerson,
                    StrNTlogin = model.NTLogin,
                    ////Company = model.CompanyEditPerson TODO timeout issue
                }; 
                var result = _dbContext.Database.ExecuteStoredProcedure<EditPeopleProcedure>(savePeopleProcedure);

                var savePasswrodProcedure = new SavePasswrodProcedure
                {
                    ObjID = savePeopleProcedure.NewObjId,
                    Password = model.Password,
                    StrNTlogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure<SavePasswrodProcedure>(savePasswrodProcedure);

                var deletePictureFileProcedure = new DeleteFileProcedure() { ObjID = savePeopleProcedure.NewObjId, Type = "PICTURE", NTLogin = model.NTLogin };
                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);

                foreach (var file in model.PictureFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = savePeopleProcedure.NewObjId, DocID = file, Type = "PICTURE", NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = savePeopleProcedure.NewObjId, Type = null, NTLogin = model.NTLogin };
                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = savePeopleProcedure.NewObjId, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "INSERT INTO A_PHONE_NUMBERS(ID, PHONE_NUMBER, PHONE_TYPE, PHONE_PIN, PHONE_EXTENSION, MODBY, DRCM, OBJECT_ID)VALUES(newID(), '" +
                    model.PrimaryPhoneNumber + "', '" + model.TypePrimaryPhoneNumber + "', '" +
                    model.ExtPrimaryPhoneNumber + "', '" + model.PinPrimaryPhoneNumber + "', '" + model.NTLogin +
                    "', getDate(), '" + savePeopleProcedure.NewObjId + "')").SingleOrDefault();
                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "UPDATE A_EMAILS SET ADDY = '"+ model.EmailPrimary + "',[TYPE] = '" + model.EmailTypePrimary + "',EMAIL_TYPE = '" + model.EmailTextTypePrimary + "',MODBY = '" + model.NTLogin + "',DRCM = getDate() WHERE ID = '" + model.EmailIdPrimary + "'").SingleOrDefault();
                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "INSERT INTO A_LOCATIONS_OBJECT_LINK (ID,LOCATION_ID,[LOCATION_TYPE],MODBY,DRCM,OBJECT_ID)VALUES (newID(), '" +
                    model.AddressLocation + "', '" + model.AddressType + "', '" + model.NTLogin +
                    "', getDate(), '" + savePeopleProcedure.NewObjId + "')").SingleOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }

        }
        public bool Delete(string objectId, string loginId)
        {
            try
            {
                var deletePeopleProcedure = new DeletePeopleProcedure
                {
                    ObjID = objectId,
                    StrNTlogin = loginId
                };

                _dbContext.Database.ExecuteStoredProcedure(deletePeopleProcedure);

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
