using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                CreatedDate = s.CreatedDate
            }).Single();

            return user;
        }

        public AddUserMessageResponse AddUser(UserSummary entity)
        {
            var response = new AddUserMessageResponse();

            try
            {

                var existingUser =
                    _dbContext.AspNetUsers.SingleOrDefault(
                        x =>
                            x.UserName.ToLower() == entity.UserName.ToLower() ||
                            x.Email.ToLower() == entity.Email.ToLower());

                if (existingUser != null)
                {
                    response.AddError($"User already exists with UserName : {entity.UserName} or email :{entity.Email}");

                    return response;
                }

                var aspContext = new ApplicationDbContext();

                var userId = Guid.NewGuid().ToString();
                var store = new UserStore<ApplicationUser>(aspContext);
                var usermanager = new UserManager<ApplicationUser>(store);

                var adminUser = new ApplicationUser
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

                usermanager.Create(adminUser, entity.PasswordHash);
                usermanager.AddToRole(userId, entity.RoleName);
                aspContext.SaveChanges();
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
                existingUser.IsActive = entity.IsActive;

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
    }
}
