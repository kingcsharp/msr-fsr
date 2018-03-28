using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ShippingMehtodList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Please select....",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = @"UPS",
                    Value = "UPS"
                },
                new SelectListItem
                {
                    Text = @"FEDEX",
                    Value = "FEDEX"
                },
                new SelectListItem
                {
                    Text = @"USPS",
                    Value = "USPS"
                },
                new SelectListItem
                {
                    Text = @"Freight",
                    Value = "Freight"
                },
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                }
            };
        }
    }
}
