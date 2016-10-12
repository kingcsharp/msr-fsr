using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Msr.Models.Users;

namespace Msr.Repositories
{
    public class ContextDbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            SeedRoles(context);

            if (!context.Users.Any(u => u.UserName == "dev"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var usermanager = new UserManager<ApplicationUser>(store);
                var adminUser = new ApplicationUser { UserName = "dev", Id = "B564A2E4-C4AC-4502-9AF7-3814C9A756F5", FirstName = "dev", LastName = "user" , TimeZone = 100, IsActive = true, CreatedDate = DateTime.UtcNow};
                
                usermanager.Create(adminUser, "msr2016!");
                usermanager.AddToRole("B564A2E4-C4AC-4502-9AF7-3814C9A756F5", RolesConstants.SuperAdmin);
            }
            context.SaveChanges();
        }

        public static void SeedRoles(ApplicationDbContext context)
        {
            var adminRole = context.Roles.SingleOrDefault(x => x.Name == RolesConstants.SuperAdmin);

            if (adminRole == null)
            {
                context.Roles.Add(new IdentityRole(RolesConstants.SuperAdmin));
            }

            var clientAdmin = context.Roles.SingleOrDefault(x => x.Name == RolesConstants.ClientAdmin);

            if (clientAdmin == null)
            {
                context.Roles.Add(new IdentityRole(RolesConstants.ClientAdmin));
            }

            var ClientBuyer = context.Roles.SingleOrDefault(x => x.Name == RolesConstants.ClientBuyer);

            if (ClientBuyer == null)
            {
                context.Roles.Add(new IdentityRole(RolesConstants.ClientBuyer));
            }

            var ClientEngineer = context.Roles.SingleOrDefault(x => x.Name == RolesConstants.ClientEngineer);

            if (ClientEngineer == null)
            {
                context.Roles.Add(new IdentityRole(RolesConstants.ClientEngineer));
            }


            context.SaveChanges();
        }
    }
}