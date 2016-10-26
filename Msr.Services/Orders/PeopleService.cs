using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Msr.Infrastructure.Helpers;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;

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
            var user = _dbContext.Peoples.SingleOrDefault(x => x.Login == login);

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
                       TimeZone = 100,
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


    }
}
