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
using System.Configuration;
using System.IO;
using Msr.Infrastructure.Email;
using Msr.Services.Users;

namespace Msr.Services.Orders
{
    public class PeopleService
    {
        private readonly MsrDbContext _dbContext;

        private readonly UserService _userService;

        public PeopleService()
        {
            _userService = new UserService();
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

        public ResultNotification<CheckLoginResult> GetAnswerUser(string login, string password)
        {
            var notificationResult = new ResultNotification<CheckLoginResult>();

            login = login.Trim().ToLower();

            var user = _dbContext.Peoples.FirstOrDefault(x => x.Login == login && (x.Status == PeopleStatusConstants.Approved || x.Status == PeopleStatusConstants.ApprovedButRevising));

            if (user == null)
            {
                notificationResult.AddError($"User not found with UserName: {login}");

                return notificationResult;
            }

            var validatePassword = _userService.ValidatePassword(user.Id, password);

            if (!validatePassword)
            {
                notificationResult.AddError("Invalid password");
                return notificationResult;
            }

            notificationResult.Entity = new CheckLoginResult();

            var portalAccount = _dbContext.AspNetUsers.SingleOrDefault(x => x.UserName.ToLower() == login);

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
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TimeZone = "10000",
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    AnswerId = user.Id
                };

                usermanager.Create(newUser, "msr" + login + "$");
                usermanager.AddToRole(id, RolesConstants.AnswerUser);
                context.SaveChanges();
            }

            notificationResult.Entity.Id = user.Id;
            notificationResult.Entity.Login = login;
            notificationResult.Entity.Password = user.Password;

            return notificationResult;
        }
        public IQueryable<PeopleObjectView> GetPeople()
        {
            return _dbContext.PeopleObjectViews;
        }
        public IQueryable<PeopleObjectView> GetApprovedPeople()
        {
            return _dbContext.PeopleObjectViews.Where(x => x.Status == "APPROVED").AsQueryable();
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

        public ResultNotification<string> Create(AddPeopleViewModel model)
        {
            var responsePeople = new ResultNotification<string>();
            try
            {

                var savePeopleProcedure = new SavePeopleProcedure
                {
                    Name = model.FirstName,
                    Login = model.LoginId,
                    LastName = model.LastName,
                    ScreenType = model.ScreenType,
                    Language = "en",
                    IsHead = model.IsDepartmentHead,
                    Position = model.OfficialPosition,
                    Boss = model.BossName,
                    TimeZone = model.TimeZone,
                    HireDate = model.HireDate.Value,
                    Status = model.StatusEditPerson,
                    StrNTlogin = model.NTLogin,
                    Company = model.CompanyEditPerson
                };

                var result = _dbContext.Database.ExecuteStoredProcedure<SavePeopleProcedure>(savePeopleProcedure);

                model.newID = savePeopleProcedure.NewObjId;

                string strPassword = AuthenticationHelper.GetPassword(7).ToString();
                model.Password = AuthenticationHelper.PasswordEncrypt(strPassword);

                var savePasswrodProcedure = new SavePasswrodProcedure
                {
                    ObjID = savePeopleProcedure.NewObjId,
                    Password = model.Password,
                    StrNTlogin = model.NTLogin
                };
                _dbContext.Database.ExecuteStoredProcedure<SavePasswrodProcedure>(savePasswrodProcedure);

                if (model.ReferenceFiles != null)
                {
                    foreach (var file in model.ReferenceFiles.Split(','))
                    {
                        var saveFileProcedure = new SaveFileProcedure()
                            {
                                ObjID = savePeopleProcedure.NewObjId,
                                DocID = file,
                                Type = null,
                                NTLogin = model.NTLogin
                            };

                        _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                    }
                }

                _dbContext.Database.SqlQuery<AddPeopleViewModel>("INSERT INTO A_PHONE_NUMBERS(ID, PHONE_NUMBER, PHONE_TYPE, PHONE_PIN, PHONE_EXTENSION, MODBY, DRCM, OBJECT_ID)VALUES(newID(), '" +
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

                return responsePeople;
            }
            catch (Exception ex)
            {
                responsePeople.AddError("There is an error when creating user");

                return responsePeople;
            }
        }

        public ResultNotification<bool> PasswordReminderEmail(string userName, string email)
        {
            var result = new ResultNotification<bool>();

            try
            {
                var userService = new UserService();

                var user = userService.GetAnserByUserName(userName);

                if (user == null)
                {
                    result.AddError("User status is not approved");
                    return result;
                }

                var from = ConfigurationManager.AppSettings["From"];
                var websiteUrl = ConfigurationManager.AppSettings["WebsiteUrl"];

                var lnkHref = $"<a href='{websiteUrl}/Account/ResetPassword?token={EncryptionHelper.Encrypt(userName)}'>Reset Password</a>";

                string body = "<b>Please set your password by clicking following link: </b><br/>" + lnkHref;

                string subject = "Password reminder";

                EmailService.SendEmail(from, email, subject, body, null, true);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Error", "There was an error when sending password reminder email");
                result.AddError(ex.Message);
            }

            return result;
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
        public bool Update(EditPeopleViewModel model)
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
                    Language = "en",
                    IsHead = model.IsDepartmentHead,
                    Position = model.OfficialPosition,
                    Boss = model.BossName,
                    TimeZone = model.TimeZone,
                    HireDate = model.HireDate,
                    Status = model.StatusEditPerson,
                    StrNTlogin = model.NTLogin,
                    Company = model.CompanyEditPerson
                };

                var result = _dbContext.Database.ExecuteStoredProcedure<EditPeopleProcedure>(savePeopleProcedure);

                var savePasswrodProcedure = new SavePasswrodProcedure
                {
                    ObjID = savePeopleProcedure.NewObjId,
                    Password = model.Password,
                    StrNTlogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure<SavePasswrodProcedure>(savePasswrodProcedure);

                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "INSERT INTO A_PHONE_NUMBERS(ID, PHONE_NUMBER, PHONE_TYPE, PHONE_PIN, PHONE_EXTENSION, MODBY, DRCM, OBJECT_ID)VALUES(newID(), '" +
                    model.PrimaryPhoneNumber + "', '" + model.TypePrimaryPhoneNumber + "', '" +
                    model.ExtPrimaryPhoneNumber + "', '" + model.PinPrimaryPhoneNumber + "', '" + model.NTLogin +
                    "', getDate(), '" + savePeopleProcedure.NewObjId + "')").SingleOrDefault();
                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "UPDATE A_EMAILS SET ADDY = '" + model.EmailPrimary + "',[TYPE] = '" + model.EmailTypePrimary + "',EMAIL_TYPE = '" + model.EmailTextTypePrimary + "',MODBY = '" + model.NTLogin + "',DRCM = getDate() WHERE ID = '" + model.EmailIdPrimary + "'").SingleOrDefault();
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
