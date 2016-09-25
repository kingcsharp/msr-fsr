using System;
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

       public AddUserMessageResponse AddUser(UserSummary entity)
       {
           var response = new AddUserMessageResponse();

           var existingUser =
               _dbContext.AspNetUsers.SingleOrDefault(x =>  x.UserName.ToLower() == entity.UserName.ToLower());

           if (existingUser != null)
           {
               response.AddError($"User already exists with UserName{entity.UserName}");

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
           usermanager.AddToRole(userId, entity.RoleId.ToString());
           aspContext.SaveChanges();

           return response;
       }
    }
}
