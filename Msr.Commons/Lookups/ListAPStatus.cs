using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListAPStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Available",
                    Value = "ap_available",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Consumed",
                    Value = "ap_consumed"
                },
                new SelectListItem
                {
                    Text = @"Filled",
                    Value = "ap_filled"
                },
                new SelectListItem
                {
                    Text = @"Held For Pickup",
                    Value = "ap_held"
                },
                new SelectListItem
                {
                    Text = @"In Call",
                    Value = "ap_in_call"
                },
                new SelectListItem
                {
                    Text = @"In Fill",
                    Value = "ap_in_fill"
                },
                new SelectListItem
                {
                    Text = @"In Transit",
                    Value = "ap_in_transit"
                },
                new SelectListItem
                {
                    Text = @"Installed",
                    Value = "ap_installed"
                },
                new SelectListItem
                {
                    Text = @"Received",
                    Value = "ap_received"
                }
            };
        }
    }
}
