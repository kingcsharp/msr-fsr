using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> AccountTypeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Select Type",
                    Value = "",
                },
                new SelectListItem
                {
                    Text = @"Purchasing",
                    Value = "PURCHASING_ACCOUNT",
                },
                new SelectListItem
                {
                    Text = @"Warranty/Maintenance",
                    Value = "WARRANTY_ACCOUNT",
                }
            };
        }
    }
}
