using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ProductTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"PROD_SERVICE",
                    Value = "SERVICE",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"PROD_GOOD",
                    Value = "GOOD"
                }
            };
        }
    }
}
