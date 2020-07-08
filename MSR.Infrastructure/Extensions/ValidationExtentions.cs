using MSR.Domain.Commanding.Enums;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Linq;

namespace MSR.Infrastructure.Extensions
{
    public static class ValidationExtentions
    {
        public static bool EmailAlreadyExists(this User user, IUnitOfWork unitOfWork)
        {
            return unitOfWork.Users.Count(i => i.Email == user.Email) > 0;
        }

        public static bool UserNameAlreadyExists(this User user, IUnitOfWork unitOfWork)
        {
            return unitOfWork.Users.Count(i => i.UserName == user.UserName) > 0;
        }

        public static bool CanApprove(this User user, EnumMenuItem menuItem)
        {
            return user != null
                    || user.Roles.Any(i =>
                            i.Role.Menus.Any(j =>
                                j.MenuItem.Name.Replace("/", "").Replace(" ", "") == menuItem.ToString() && j.MenuRolePermission.CanApprove));
        }
    }
}
