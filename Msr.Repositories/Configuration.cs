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
            var answerUser = context.Roles.SingleOrDefault(x => x.Name == RolesConstants.AnswerUser);

            if (answerUser == null)
            {
                context.Roles.Add(new IdentityRole(RolesConstants.AnswerUser));
            }

            context.SaveChanges();
        }
    }
}