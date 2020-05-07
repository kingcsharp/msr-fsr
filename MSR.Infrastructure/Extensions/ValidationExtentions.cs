using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

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
    }
}
