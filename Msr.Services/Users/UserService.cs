using System.Collections.Generic;
using System.Linq;
using Msr.Infrastructure.Helpers;
using Msr.Models.Companies;
using Msr.Models.Orders;
using Msr.Models.People;
using Msr.Repositories;
using Msr.Services.Users.Messages;

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


        public CompanyView GetCompanyId(string id)
        {
            var compannyId = _dbContext.AspNetUsers.Where(x => x.Id.ToLower() == id.ToLower()).Select(x => x.CompanyId).First();

            return _dbContext.CompanyViews.SingleOrDefault(x => x.Id == compannyId);
        }


        public List<SearchPeopleResult> GetSearchUser()
        {
          var users = _dbContext.PeopleObjectViews.Where(x=> x.Status == PeopleStatusConstants.Approved || x.Status == PeopleStatusConstants.ApprovedButRevising) .Select(x => new SearchPeopleResult
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
            var sql = $"exec Portal_GetCurrentUser {userId}";

            var result = _dbContext.Database.SqlQuery<LoggedUserIdResult>(sql).Single();

            return result;
        }

        public void UpdatePassword(string id, string password)
        {
            _dbContext.Database.ExecuteSqlCommand($"update A_PEOPLE_HISTORY set PASSWORD='{AuthenticationHelper.PasswordEncrypt(password)}' WHERE id='{id}'");
        }

        public bool ValidatePassword(string id, string password)
        {
            var curentPassword = _dbContext.Peoples.Where(x => x.Id == id).Select(x => x.Password)
                .SingleOrDefault();

            var encryptedPasswod = AuthenticationHelper.PasswordEncrypt(password);

            return curentPassword == encryptedPasswod;
        }

    }
}
