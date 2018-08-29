using System.Collections.Generic;
using System.Linq;
using Answer.Web.ViewModel;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.Menus;
using Msr.Services.Menus;
using Msr.Services.Roles;
using Msr.Services.Users.Messages;

namespace Answer.Web.Helpers
{
    public static class UIPermissionHelper
    {
        public static bool HasPermission(List<string> menuGroups, string moduleName)
        {
            return menuGroups != null && menuGroups.Any(x => x == moduleName);
        }

        public static MenuViewModel GetUserMenuPermissions(LoggedUserIdResult loggedUserIdResult)
        {
            var menuViewModel = new MenuViewModel {LoggedUserIdResult = loggedUserIdResult};

            var menuService = new MenuService();
            menuViewModel.UserModules = menuService.GetMenu(loggedUserIdResult.Id);

            if (!menuViewModel.UserModules.Any(x=>x.Name == MenuGroupConstants.AnswerAdmin))
            {
                var roleService = new RoleService();
                var roles = roleService.GetAssignedRoles(loggedUserIdResult.Id);

                if (roles.Any(x => x.Role_Name == RoleConstants.Administrators))
                {
                    var adminMenu = menuService.GetMenuByName(MenuGroupConstants.AnswerAdmin);
                    menuViewModel.UserModules.Add(adminMenu);
                }
            }

            return menuViewModel;
        }
    }
}