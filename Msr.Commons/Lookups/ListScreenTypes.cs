using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListScreenTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Normal",
                    Value = "NORMAL_SCREEN",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Work Screen",
                    Value = "WORK_SCREEN"
                },
                new SelectListItem
                {
                    Text = "Shipper Screen",
                    Value = "SHIP_SCREEN",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Delivery Screen",
                    Value = "DELIVERY_SCREEN"
                },
                new SelectListItem
                {
                    Text = "Order Entry Screen",
                    Value = "ORDER_ENTRY_SCREEN",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Admin Screen",
                    Value = "ADMIN_SCREEN"
                }
            };
        }
    }
}
