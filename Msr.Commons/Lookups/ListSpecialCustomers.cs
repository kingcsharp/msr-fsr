using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListSpecialCustomers()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = @"All Insternal Customers Except My Dept",
                    Value = "ALL_INTERNAL_BUT_ME"
                },
                new SelectListItem
                {
                    Text = @"All External Customers",
                    Value = "ALL_EXTERNAL"
                }
            };
        }
    }
}
