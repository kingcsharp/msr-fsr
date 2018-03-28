using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListSubPartActions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_PUBLIC",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_SOME_WHAT_CONFIDENTIAL",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_CONFIDENTIAL",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = @"SECURITY_LEVEL_EXTREMELY_CONFIDENTIAL",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = @"SYSTEM",
                    Value = "5"
                }
            };
        }
    }
}
