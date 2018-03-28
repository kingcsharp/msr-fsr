using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> SparesPartType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text = @"Not Typically a Spare Part", Value = "PTSPARE_NO" },
                new SelectListItem
                {
                    Text = @"L1 - Stock in location with 1 machine",
                    Value = "PTSPARE_1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"L2 - Stock in location with 10 machine",
                    Value = "PTSPARE_2"
                },
                new SelectListItem
                {
                    Text = @"L3 - Stock in location with 50 machine",
                    Value = "PTSPARE_3"
                }
            };
        }
    }
}
