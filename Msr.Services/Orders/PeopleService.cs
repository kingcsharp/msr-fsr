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
using Msr.Services.Roles;
using Msr.Services.Users;

namespace Msr.Services.Orders
{
    public class PeopleService
    {
        private readonly MsrDbContext _dbContext;

        private readonly UserService _userService;

        private readonly RoleService _roleService;

        public PeopleService()
        {
            _userService = new UserService();
            _dbContext = new MsrDbContext();
            _roleService = new RoleService();
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
            var result = new ResultNotification<CheckLoginResult>();

            login = login.Trim().ToLower();

            var user = _dbContext.Peoples.FirstOrDefault(x => x.Login == login && (x.Status == PeopleStatusConstants.Approved || x.Status == PeopleStatusConstants.ApprovedButRevising));

            if (user == null)
            {
                result.AddError($"User not found with UserName: {login}");

                return result;
            }
            var aspNetUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.UserName.ToLower() == login);

            if (aspNetUser != null && aspNetUser.PortalUser)
            {
                result.AddError($"You do not have permissions.");
                return result;
            }

            var validatePassword = _userService.ValidatePassword(user.Id, password);

            if (!validatePassword)
            {
                result.AddError("Invalid password");
                return result;
            }

            result.Entity = new CheckLoginResult();

            if (aspNetUser == null)
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

            _roleService.RefreshUserRoles(user.Id);

            result.Entity.Id = user.Id;
            result.Entity.Login = login;
            result.Entity.Password = user.Password;

            return result;
        }
        public IQueryable<PeopleObjectView> GetPeople()
        {
            return _dbContext.PeopleObjectViews;
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

                var loginIdExist = _dbContext.PeopleObjectViews.Any(x => x.LoginId == model.LoginId || x.EmailAddress == model.EmailPrimary);

                if (loginIdExist)
                {
                    responsePeople.AddError($"LoginId or email already exist with '{model.LoginId}'");
                    return responsePeople;
                }

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

                string subject = "ANSWER - Password reminder";

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
                    "SELECT PHONE_NUMBER as PrimaryPhoneNumber,PHONE_TYPE as TypePrimaryPhoneNumber,PHONE_PIN as PinPrimaryPhoneNumber,PHONE_EXTENSION as ExtPrimaryPhoneNumber,ID as PhoneId FROM dbo.A_PHONE_NUMBERS WHERE OBJECT_ID = '" + id + "'")
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
                    "SELECT LOCATION_ID as AddressLocation,ID as LocationId, LOCATION_TYPE as AddressType FROM A_LOCATIONS_OBJECT_LINK WHERE OBJECT_ID = '" + id + "'")
                .FirstOrDefault();
            return result;
        }
        public ResultNotification<string> Update(EditPeopleViewModel model)
        {
            var responsePeople = new ResultNotification<string>();
            try
            {
                var userEmail = _dbContext.PeopleObjectViews.Where(x => x.ObjectId == model.ObjectId).Select(x => x.EmailAddress).SingleOrDefault();

                if (userEmail != null && userEmail != model.EmailPrimary)
                {
                    var hasEmail = _dbContext.PeopleObjectViews.Any(x => x.EmailAddress.Contains(model.EmailPrimary) && (x.Status == "APPROVED" || x.Status == "APPROVED_BUT_REVISING"));

                    if (hasEmail)
                    {
                        responsePeople.AddError($"Email already exist");
                        return responsePeople;
                    }
                }

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
                     "UPDATE A_PHONE_NUMBERS SET PHONE_NUMBER='" + model.PrimaryPhoneNumber + "',PHONE_TYPE='" + model.TypePrimaryPhoneNumber + "'" +
                     ",PHONE_PIN= '" + model.ExtPrimaryPhoneNumber + "',PHONE_EXTENSION='" + model.PinPrimaryPhoneNumber + "', MODBY='" + model.NTLogin + "' ,DRCM=getDate()" +
                     ",OBJECT_ID='" + model.ObjectId + "' WHERE ID = '" + model.PhoneId + "'").SingleOrDefault();

                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "UPDATE A_EMAILS SET ADDY = '" + model.EmailPrimary + "',[TYPE] = '" + model.EmailTypePrimary + "',EMAIL_TYPE = '" + model.EmailTextTypePrimary + "',MODBY = '" + model.NTLogin + "',DRCM = getDate() WHERE ID = '" + model.EmailIdPrimary + "'").SingleOrDefault();

                _dbContext.Database.SqlQuery<EditPeopleViewModel>(
                    "UPDATE A_LOCATIONS_OBJECT_LINK SET LOCATION_ID='" + model.AddressLocation + "',[LOCATION_TYPE]='" +
                    model.AddressType + "'" +
                    ",MODBY= '" + model.NTLogin + "',DRCM=getDate(), OBJECT_ID='" + model.ObjectId + "'  WHERE ID = '" +
                    model.LocationId + "'").SingleOrDefault();

                return responsePeople;
            }
            catch (Exception ex)
            {
                responsePeople.AddError("There is an error when editing user");

                return responsePeople;
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
        public List<PeopleApprovedSearch> GetPeopleApprovedSearch(string ntlogin, string co)
        {
            return _dbContext.Database.SqlQuery<PeopleApprovedSearch>($"exec A_SP_PEOPLE_SEARCH ' (FULL_NAME LIKE ''%%'' OR FULL_NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (POSITION_NAME LIKE ''%%'' OR POSITION_NAME is NULL ) AND  (BOSS_NAME LIKE ''%%'' OR BOSS_NAME is NULL ) AND  (COMPANY_NAME LIKE ''%%'' OR COMPANY_NAME is NULL ) AND (( ROOT_CO_ID LIKE ''%{co}%'' ) ) AND  STATUS LIKE ''APPROVED%'' AND  (LOGIN IS NOT NULL) AND  (LOCATION_NAME LIKE ''%%'' OR LOCATION_NAME is NULL )',' ORDER BY LAST_NAME,NAME',NULL,NULL,'{ntlogin}'").ToList();
        }
    }
}
