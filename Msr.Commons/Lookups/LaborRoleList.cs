using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> LaborRoleList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"LABOR_OWNER",
                    Value = "LABOR_OWNER"
                },
                new SelectListItem
                {
                    Text = @"LABOR_ASSISTANT",
                    Value = "LABOR_ASSISTANT"
                }
            };
        }
    }
}
