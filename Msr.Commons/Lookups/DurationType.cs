using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> DurationType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"SYS_SECONDS",
                    Value = "TIME_SYS_SECONDS"
                },
                new SelectListItem
                {
                    Text = @"SYS_MINUTES",
                    Value = "TIME_SYS_MINUTES",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"SYS_HOURS",
                    Value = "TIME_SYS_HOURS"
                },
                new SelectListItem
                {
                    Text = @"SYS_DAYS",
                    Value = "TIME_SYS_DAYS"
                },
                new SelectListItem
                {
                    Text = @"SYS_WEEKS",
                    Value = "TIME_SYS_WEEKS"
                }
            };
        }
    }
}