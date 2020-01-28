using LazyCache;
using Msr.Infrastructure.Email;
using Msr.Infrastructure.Helpers;
using Msr.Models.Companies;
using Msr.Models.Orders;
using Msr.Models.People;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Users.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Msr.Services.Users
{
    public class UserService
    {
        private readonly MsrDbContext _dbContext;

        public UserService()
        {
            _dbContext = new MsrDbContext();
        }

        public UserView GetEmailNotificationUserForWorkOrder(string companyId, string companyName, string screenName, string roleName)
        {
            var userView = _dbContext.UserViews.FirstOrDefault(s => s.CompanyId == companyId && s.CompanyName == companyName
                                                                              && s.FullName == screenName && s.RoleName == roleName);

            return userView;
        }

        public UserSummary GetUser(string id)
        {
            var user = _dbContext.AspNetUsers.Where(x => x.Id.ToLower() == id.ToLower()).Select(s => new UserSummary
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
            }).OrderByDescending(x => x.Id).FirstOrDefault();

            return user;
        }

        public UserSummary GetUserByAnswerID(string answerUserID)
        {
            var user = _dbContext.AspNetUsers.Where(x => x.AnswerId.ToLower() == answerUserID.ToLower()).Select(s => new UserSummary
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
            }).OrderByDescending(x => x.Id).FirstOrDefault();

            return user;
        }

        public PeopleView GetUserByObjectId(string objId)
        {
            var user = _dbContext.Peoples.Where(i => i.ObjectId == objId).FirstOrDefault();
            return user;
        }
        public UserSummary GetAnserByUserName(string userName)
        {
            userName = userName.ToLower().Trim();

            var user = _dbContext.Peoples.Where(x => x.Login.ToLower() == userName && (x.Status == PeopleStatusConstants.Approved || x.Status == PeopleStatusConstants.ApprovedButRevising)).Select(s => new UserSummary
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FirstName + " " + s.LastName,
                Email = s.Email,
                UserName = s.Login,
                Phone = s.PrimaryPhone,
                CompanyName = s.CompanyName,
                Login = s.Login,
                Title = s.Title
            }).OrderByDescending(x => x.Id).FirstOrDefault();

            return user;
        }

        public UserSummary GetByUserName(string userId)
        {
            userId = userId.ToLower().Trim();

            var user = _dbContext.AspNetUsers.Where(x => x.UserName.ToLower() == userId).Select(s => new UserSummary
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FirstName + " " + s.LastName,
                Phone = s.PhoneNumber,
                Phone2 = s.Phone2,
                Email = s.Email,
                UserName = s.UserName,
                IsActive = s.IsActive,
                TimeZone = s.TimeZone,
                CompanyId = s.CompanyId,
                CreatedDate = s.CreatedDate,
                RoleName = s.AspNetRoles.FirstOrDefault().Name,
                PortalUser = s.PortalUser,
                Login = s.UserName
            }).OrderByDescending(x => x.Id).FirstOrDefault();

            return user;
        }


        public CompanyView GetCompanyId(string id)
        {
            var compannyId = _dbContext.AspNetUsers.Where(x => x.Id.ToLower() == id.ToLower()).Select(x => x.CompanyId).First();

            return _dbContext.CompanyViews.FirstOrDefault(x => x.Id == compannyId);
        }


        public List<SearchPeopleResult> GetSearchUser()
        {
            var users = _dbContext.PeopleObjectViews.Where(x => x.Status == PeopleStatusConstants.Approved || x.Status == PeopleStatusConstants.ApprovedButRevising).Select(x => new SearchPeopleResult
            {
                Id = x.Id,
                Full_Name = x.FirstName + " " + x.LastName,
                Obj_Id = x.ObjectId,
                Root = x.Root
            }).ToList();

            return users;
        }

        public LoggedUserIdResult GetUserId(string userId)
        {
            IAppCache cachingService = new CachingService();

            Func<LoggedUserIdResult> getUserIDDelegate = () => GetUserIDDB(userId);

            string globallyUniqueCacheItemName = $"GetUserId({userId})".Trim().ToUpper();

            LoggedUserIdResult loggedUserIdResult = cachingService.GetOrAdd(globallyUniqueCacheItemName, getUserIDDelegate, DateTimeOffset.Now.AddHours(1));

            if (loggedUserIdResult == null)
            {
                loggedUserIdResult = GetUserIDDB(userId);

                cachingService.Add(globallyUniqueCacheItemName, loggedUserIdResult);
            }

            return loggedUserIdResult;
        }

        private LoggedUserIdResult GetUserIDDB(string userId)
        {
            string sql = $"exec Portal_GetCurrentUser {userId}";

            LoggedUserIdResult result = _dbContext.Database.SqlQuery<LoggedUserIdResult>(sql).Single();

            return result;
        }

        public string GetUserLoginByAnswerUserRootID(string answerUserRootID)
        {
            string sql = $"SELECT [LOGIN] FROM A_APPROVED_PEOPLE WHERE ID = '{answerUserRootID}' ORDER BY ID";

            string loginName = _dbContext.Database.SqlQuery<string>(sql).FirstOrDefault();

            return loginName;
        }

        public bool CheckModulePermissions(string userId, string moduleName)
        {
            var userIdParam = new SqlParameter("@userId", userId);
            var moduleNameParam = new SqlParameter("@moduleName", moduleName);

            var result = _dbContext.Database
                .SqlQuery<int>("Portal_CheckModulePermissions @userId, @moduleName", userIdParam, moduleNameParam)
                .FirstOrDefault();

            return result > 0;
        }

        public void UpdatePassword(string id, string password)
        {
            _dbContext.Database.ExecuteSqlCommand($"update A_PEOPLE_HISTORY set PASSWORD='{AuthenticationHelper.PasswordEncrypt(password)}' WHERE id='{id}'");
        }

        public bool ValidatePassword(string id, string password)
        {
            var curentPassword = _dbContext.Peoples.Where(x => x.Id == id).Select(x => x.Password)
                .FirstOrDefault();

            var encryptedPasswod = AuthenticationHelper.PasswordEncrypt(password);

            return curentPassword == encryptedPasswod;
        }

        public async Task<ResultNotification<bool>> SendUsername(string email)
        {
            var result = new ResultNotification<bool>();

            email = email.Trim().ToLower();

            var user = await _dbContext.Peoples
              .Where(x => x.EmailAddress.ToLower() == email && (x.Status == PeopleStatusConstants.Approved || x.Status == PeopleStatusConstants.ApprovedButRevising))
             .Select(s => new UserSummary
             {
                 UserName = s.Login
             }).FirstOrDefaultAsync();

            if (user == null)
            {
                result.AddError($"User not found with email '{email}'");
                return result;
            }

            var from = ConfigurationManager.AppSettings["From"];

            var body = $"Hello ANSWER user, <br/><br/> <b>Your username is : {user.UserName}</b>";

            var subject = "ANSWER - Forgot Username";

            try
            {
                EmailService.SendEmail(from, email, subject, body, null, true);
            }
            catch (Exception ex)
            {
                result.AddError("There is an error sending email.");
            }

            return result;
        }

        public void RemoveUserCacheItems(string userID, string userName)
        {
            IAppCache cachingService = new CachingService();

            string getUserIDCacheItemName = string.Empty;

            if (string.IsNullOrWhiteSpace(userID) == false)
            {
                userID = userID.Trim();

                getUserIDCacheItemName = $"GetUserId({userID})".Trim().ToUpper();

                cachingService.Remove(getUserIDCacheItemName);
            }

            if (string.IsNullOrWhiteSpace(userName) == false)
            {
                userName = userName.Trim();

                getUserIDCacheItemName = $"GetUserId({userName})".Trim().ToUpper();

                cachingService.Remove(getUserIDCacheItemName);
            }
        }

    }
}
