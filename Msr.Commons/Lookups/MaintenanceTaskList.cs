using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> MaintenanceTaskList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Add /Replace Media",
                    Value = "Add /Replace Media",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Cleaning",
                    Value = "Cleaning"
                },
                new SelectListItem
                {
                    Text = @"PM",
                    Value = "PM"
                },
                new SelectListItem
                {
                    Text = @"Repair",
                    Value = "Repair"
                }
            };
        }
    }
}
