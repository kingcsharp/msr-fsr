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
using Msr.Models.People;
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
            userId = userId.ToLower().Trim();

            var user = _dbContext.AspNetUsers.Where(x => x.UserName.ToLower() == userId).Select(s => new UserSummary
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
                Login = s.Login
            }).SingleOrDefault();

            return user;
        }


        public CompanyView GetCompanyId(string id)
        {
            var compannyId = _dbContext.AspNetUsers.Where(x => x.Id.ToLower() == id.ToLower()).Select(x => x.CompanyId).First();

            return _dbContext.CompanyViews.SingleOrDefault(x => x.Id == compannyId);
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
            var sql = $"exec Portal_GetCurrentUser {userId}";

            var result = _dbContext.Database.SqlQuery<LoggedUserIdResult>(sql).Single();

            return result;
        }

        public void UpdatePassword(string id, string password)
        {
            _dbContext.Database.ExecuteSqlCommand($"update A_PEOPLE_HISTORY set PASSWORD='{AuthenticationHelper.PasswordEncrypt(password)}' where OBJECT_ID='{id}'");
        }

    }
}
