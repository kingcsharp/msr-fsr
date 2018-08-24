using System.Collections.Generic;
using System.Linq;

namespace Answer.Web.Helpers
{
    public static class UIPermissionHelper
    {
        public static bool HasPermission(List<string> menuGroups, string moduleName)
        {
            return menuGroups != null && menuGroups.Any(x => x == moduleName);
        }
    }
}