using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> InvoicePeriodTypeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Days",
                    Value = "DAYS"
                },
                new SelectListItem
                {
                    Text = @"Weeks",
                    Value = "WEEKS"
                },
                new SelectListItem
                {
                    Text = @"Months",
                    Value = "MONTHS"
                }
            };
        }
    }
}
