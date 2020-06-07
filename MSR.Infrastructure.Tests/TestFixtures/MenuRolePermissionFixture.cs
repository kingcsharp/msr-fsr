using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class MenuRolePermissionFixture
    {
        public static MenuRolePermission FullMenuRolePermission => new MenuRolePermission()
        {
            Id = 1,
            CanActivate = true,
            CanApprove = true,
            CanCreate = true,
            CanDelete = true,
            CanEdit = true,
            CanRead = true,
        };
    }
}
