using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Msr.Infrastructure.Helpers;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;

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

                var existingUser =
                    _dbContext.AspNetUsers.SingleOrDefault(
                        x =>
                            x.UserName.ToLower() == entity.UserName.ToLower());

                if (existingUser != null)
                {
                    response.AddError($"User already exists with UserName : {entity.UserName} or email :{entity.Email}");

                    return response;
                }

                var aspContext = new ApplicationDbContext();

                var userId = Guid.NewGuid().ToString();
                var store = new UserStore<ApplicationUser>(aspContext);
                var usermanager = new UserManager<ApplicationUser>(store);

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

        public void AddClientToUser(string userId, List<string> usderIds, string createdBy)
        {

            foreach (var uId in usderIds)
            {
               var hasUser = _dbContext.ClientUsers.Any(x => x.UserId.ToLower() == userId.ToLower() && x.ClientId.ToLower() == uId.ToLower());

                if (!hasUser)
                {
                    var newUser = new ClientUser
                    {
                        Id = Guid.NewGuid(),
                        ClientId = uId,
                        UserId = userId,
                        CreatedBy = userId,
                        CreatedDate = DateTime.UtcNow
                    };

                    _dbContext.ClientUsers.Add(newUser);
                }
            }

            _dbContext.SaveChanges();
        }

        public List<string> GetClientUsers(string userId)
        {
            return _dbContext.ClientUsers.Where(x => x.UserId.ToLower() == userId.ToLower()).Select(x => x.ClientId).ToList();
        }

        public CompanyView GetCompanyId(string id)
        {
            var compannyId = _dbContext.AspNetUsers.Where(x => x.Id.ToLower() == id.ToLower()).Select(x => x.CompanyId).First();

            return _dbContext.CompanyView.SingleOrDefault(x => x.Id == compannyId);
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
    }
}
