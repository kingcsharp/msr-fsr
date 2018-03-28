using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ShippingWeightTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Ounces",
                    Value = "WT_OZ",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Pounds",
                    Value = "WT_LBS"
                },
                new SelectListItem
                {
                    Text = @"Kilograms",
                    Value = "WT_KG"
                },
            };
        }
    }
}
