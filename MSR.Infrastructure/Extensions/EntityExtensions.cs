using MSR.Domain.Commands;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePermissionsFrom(this MenuRolePermission permissions, UpdateMenuRoleMap command)
        {
            permissions.CanActivate = command.CanActivate;
            permissions.CanCreate = command.CanCreate;
            permissions.CanDelete = command.CanDelete;
            permissions.CanEdit = command.CanEdit;
            permissions.CanRead = command.CanRead;
            permissions.CanApprove = command.CanApprove;
        }
    }
}
