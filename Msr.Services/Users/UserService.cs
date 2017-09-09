using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Msr.Infrastructure.Email;
using Msr.Infrastructure.Helpers;
using Msr.Models.Companies;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Resources.Templates;
using Msr.Services.Orders.Messaging;
using Msr.Services.Users.Messages;
using Msr.Services.Users.ViewModels;
using RazorEngine;

namespace Msr.Services.Users
{
    public class UserService
    {
        private readonly MsrDbContext _dbContext;

        public UserService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<UserView> GetUserQueryable()
        {
            return _dbContext.UserViews;
        }

        public IQueryable<PeopleView> GetPeoplesQueryable()
        {
            return _dbContext.Peoples;
        }


        public List<AspNetRole> GetRoles()
        {
            return _dbContext.AspNetRoles.ToList();
        }


        public UserSummary GetUser(string id)
        {
            var user = _dbContext.AspNetUsers.Where(x => x.Id == id).Select(s => new UserSummary
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FirstName + " " + s.LastName,
                Phone = s.PhoneNumber,
                Phone2 = s.UserName,
                Email = s.Email,
                UserName = s.UserName,
                IsActive = s.IsActive,
                TimeZone = s.TimeZone,
                CompanyId = s.CompanyId,
                CreatedDate = s.CreatedDate,
                RoleName = s.AspNetRoles.FirstOrDefault().Name
            }).Single();

            return user;
        }

        public UserSummary GetByUserName(string userId)
        {
            var user = _dbContext.AspNetUsers.Where(x => x.UserName == userId).Select(s => new UserSummary
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FirstName + " " + s.LastName,
                Phone = s.PhoneNumber,
                Phone2 = s.UserName,
                Email = s.Email,
                UserName = s.UserName,
                IsActive = s.IsActive,
                TimeZone = s.TimeZone,
                CompanyId = s.CompanyId,
                CreatedDate = s.CreatedDate,
                RoleName = s.AspNetRoles.FirstOrDefault().Name
            }).SingleOrDefault();

            return user;
        }

        public AddUserMessageResponse AddUser(UserSummary entity, string loggedUserId)
        {
            var response = new AddUserMessageResponse();
            
            try
            {
                if (HasAnswerUser(entity.UserName))
                {
                    response.AddError("Answer User already exists with UserName :" + entity.UserName);
                    return response;
                }

                var existingUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.UserName.ToLower() == entity.UserName.ToLower());

                if (existingUser != null)
                {
                    response.AddError("User already exists with UserName :" + entity.UserName);

                    return response;
                }

                var aspContext = new ApplicationDbContext();

                var userId = Guid.NewGuid().ToString();
                var store = new UserStore<ApplicationUser>(aspContext);
                var usermanager = new UserManager<ApplicationUser>(store);

                entity.PasswordHash = "MSa123@"; //// This will be reset once user created

                var newAspUser = new ApplicationUser
                {
                    UserName = entity.UserName,
                    Id = userId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    TimeZone = entity.TimeZone,
                    PhoneNumber = entity.Phone,
                    Email = entity.Email,
                    IsActive = entity.IsActive,
                    CreatedDate = DateTime.UtcNow
                };

                usermanager.Create(newAspUser, entity.PasswordHash);
                usermanager.AddToRole(userId, entity.RoleName);
                aspContext.SaveChanges();

                var newUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.Id.ToLower() == newAspUser.Id);

                newUser.TimeZone = entity.TimeZone;
                newUser.Phone2 = entity.Phone2;
                newUser.IsActive = entity.IsActive;
                newUser.CompanyId = entity.CompanyId;
                newUser.ParentId = loggedUserId;

                _dbContext.SaveChanges();

                response.UserId = newUser.Id;

            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
            }

            return response;
        }

        public AddUserMessageResponse UpdateUser(UserSummary entity)
        {
            var response = new AddUserMessageResponse();

            try
            {
                if (HasAnswerUser(entity.UserName))
                {
                    response.AddError("Answer User already exists with UserName :"+ entity.UserName);
                    return response;
                }

                var existingUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.Id.ToLower() == entity.Id.ToLower());

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.TimeZone = entity.TimeZone;
                existingUser.PhoneNumber = entity.Phone;
                existingUser.Phone2 = entity.Phone2;
                existingUser.Email = entity.Email;
                existingUser.UserName = entity.UserName;
                existingUser.CompanyId = entity.CompanyId;
                existingUser.IsActive = entity.IsActive;

                var existingRole = existingUser.AspNetRoles.FirstOrDefault();

                if (existingRole !=null && existingRole.Name != entity.RoleName)
                {
                    existingUser.AspNetRoles.Remove(existingRole);

                 var newRole =  _dbContext.AspNetRoles.Single(x => x.Name == entity.RoleName);

                    existingUser.AspNetRoles.Add(newRole);
                }
               else if (existingRole == null)
               {
                   var newRole = _dbContext.AspNetRoles.Single(x => x.Name == entity.RoleName);
                   existingUser.AspNetRoles.Add(newRole);
               }

                _dbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
            }

            return response;
        }

        public AddUserMessageResponse UpdateClientUser(UserSummary entity)
        {
            var response = new AddUserMessageResponse();

            try
            {
                var existingUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.Id.ToLower() == entity.Id.ToLower());

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.TimeZone = entity.TimeZone;
                existingUser.PhoneNumber = entity.Phone;
                existingUser.Phone2 = entity.Phone2;
                existingUser.Email = entity.Email;
                existingUser.UserName = entity.UserName;
                existingUser.IsActive = entity.IsActive;

                var existingRole = existingUser.AspNetRoles.FirstOrDefault();

                if (existingRole != null && existingRole.Name != entity.RoleName)
                {
                    existingUser.AspNetRoles.Remove(existingRole);

                    var newRole = _dbContext.AspNetRoles.Single(x => x.Name == entity.RoleName);

                    existingUser.AspNetRoles.Add(newRole);
                }
                else if (existingRole == null)
                {
                    var newRole = _dbContext.AspNetRoles.Single(x => x.Name == entity.RoleName);
                    existingUser.AspNetRoles.Add(newRole);
                }

                _dbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
            }

            return response;
        }
        public AddUserMessageResponse DeleteUser(string id)
        {
            var response = new AddUserMessageResponse();

            try
            {
                var existingUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.Id.ToLower() == id.ToLower());

                _dbContext.AspNetUsers.Remove(existingUser);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
            }

            return response;
        }

        public List<string> GetClientUsers(string userId)
        {
            return _dbContext.ClientUsers.Where(x => x.UserId.ToLower() == userId.ToLower()).Select(x => x.ClientId).ToList();
        }

        public CompanyView GetCompanyId(string id)
        {
            var compannyId = _dbContext.AspNetUsers.Where(x => x.Id.ToLower() == id.ToLower()).Select(x => x.CompanyId).First();

            return _dbContext.CompanyViews.SingleOrDefault(x => x.Id == compannyId);
        }

        public CheckLoginResult CheckLogin(string login, string password)
        {
            var loginParm = new SqlParameter("@Login", login);
            var passwordParm = new SqlParameter("@Password", AuthenticationHelper.PassWordEncrypt(password));

            var result = _dbContext.Database.SqlQuery<CheckLoginResult>("Portal_Check_Login @Login, @Password", loginParm, passwordParm).Single();

            return result;
        }

        public BaseNotification UpdateUserProfile(UserSummary entity)
        {
            var response = new BaseNotification();

            var existingUser = _dbContext.AspNetUsers.SingleOrDefault(x => x.Id.ToLower() == entity.Id.ToLower());

            existingUser.FirstName = entity.FirstName;
            existingUser.LastName = entity.LastName;
            existingUser.PhoneNumber = entity.Phone;
            existingUser.Phone2 = entity.Phone2;
            existingUser.Email = entity.Email;

            _dbContext.SaveChanges();

            return response;
        }

        public bool SendEmailToUser(string userId, string callBackUrl, string cc=null)
        {
            var user = GetUserView(userId);

            var from = ConfigurationManager.AppSettings["From"];
            var supportEmail = ConfigurationManager.AppSettings["SupportEmail"];
            var body = string.Empty;

            if (user.RoleName == RolesConstants.ClientBuyer)
            {
                var clientBuyerEmailTemplate = new ClientBuyerEmailTemplateViewModel();
                clientBuyerEmailTemplate.CompanyName = user.CompanyName;
                clientBuyerEmailTemplate.SupportEmail = supportEmail;
                clientBuyerEmailTemplate.ResetPasswordUrl = callBackUrl;
                body = Razor.Parse(Emails.ClientBuyer, clientBuyerEmailTemplate);
            }
            else if (user.RoleName == RolesConstants.ClientEngineer)
            {
                var clientEngineerEmailTemplate = new ClientEngineerEmailTemplateViewModel();
                clientEngineerEmailTemplate.CompanyName = user.CompanyName;
                clientEngineerEmailTemplate.SupportEmail = supportEmail;
                clientEngineerEmailTemplate.ResetPasswordUrl = callBackUrl;
                body = Razor.Parse(Emails.ClientEngineer, clientEngineerEmailTemplate);
            }
            else if (user.RoleName == RolesConstants.ClientAdmin)
            {
                var clientEngineerEmailTemplate = new ClientaAdminEmailTemplateViewModel();
                clientEngineerEmailTemplate.CompanyName = user.CompanyName;
                clientEngineerEmailTemplate.SupportEmail = supportEmail;
                clientEngineerEmailTemplate.ResetPasswordUrl = callBackUrl;
                body = Razor.Parse(Emails.ClientAdmin, clientEngineerEmailTemplate);
            }

            return EmailService.SendEmail(from, user.Email, "Portal Login", body, new List<string> {cc}, true);
        }

        public UserView GetUserView(string id)
        {
            return _dbContext.UserViews.SingleOrDefault(x => x.Id == id);
        }

        public List<SearchPeopleResult> GetSearchUser()
        {
            var sql = @"exec A_SP_PEOPLE_SEARCH ' (FULL_NAME LIKE ''%%'' OR FULL_NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (POSITION_NAME LIKE ''%%'' OR POSITION_NAME is NULL ) AND 
 (BOSS_NAME LIKE ''%%'' OR BOSS_NAME is NULL ) AND  (COMPANY_NAME LIKE ''%%'' OR COMPANY_NAME is NULL ) AND (( ROOT_CO_ID LIKE ''%2%'' ) ) 
 AND  STATUS LIKE ''APPROVED%'' AND  (LOGIN IS NOT NULL) AND  (LOCATION_NAME LIKE ''%%'' OR LOCATION_NAME is NULL )',' ORDER BY LAST_NAME,NAME',NULL,NULL,'1618'";

            var result = _dbContext.Database.SqlQuery<SearchPeopleResult>(sql).ToList();

            return result;
        }

        public LoggedUserIdResult GetUserId(string userId)
        {
            var sql = string.Format("exec Portal_GetCurrentUser {0}",userId);

            var result = _dbContext.Database.SqlQuery<LoggedUserIdResult>(sql).Single();

            return result;
        }

        private bool HasAnswerUser(string userName)
        {
            var answerUser = _dbContext.Peoples.SingleOrDefault(x => x.Login.ToLower() == userName.ToLower());

            if (answerUser != null)
            {
                return true;
            }

            return false;
        }
    }
}
