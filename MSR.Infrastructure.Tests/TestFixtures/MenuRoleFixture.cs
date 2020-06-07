
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Tests.TestFixtures
{
    public static class MenuRoleFixture
    {
        public static MenuRole SuccessMenuRole => new MenuRole()
        {
            Id = 1,
            MenuItem = MenuItemFixture.SuccessMenuItem,
            MenuItemId = MenuItemFixture.SuccessMenuItem.Id,
            Role = RoleFixture.SuccessRole,
            RoleId = RoleFixture.SuccessRole.Id,
            MenuRolePermission = MenuRolePermissionFixture.FullMenuRolePermission
        };
        public static MenuRole ExistingMenuRole => new MenuRole()
        {
            Id = 2,
            MenuItem = MenuItemFixture.SuccessMenuItem,
            MenuItemId = MenuItemFixture.SuccessMenuItem.Id,
            Role = RoleFixture.SuccessRole,
            RoleId = RoleFixture.SuccessRole.Id,
            MenuRolePermission = MenuRolePermissionFixture.FullMenuRolePermission
        };
    }
}
