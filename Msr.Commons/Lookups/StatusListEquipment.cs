using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> StatusListEquipment()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = @"Select Status", Value = ""},
                new SelectListItem {Text = @"Requested", Value = "REQUESTED"},
                new SelectListItem {Text = @"Assigned", Value = "ASSIGNED"},
                new SelectListItem {Text = @"Completed", Value = "COMPLETED"},
                new SelectListItem {Text = @"Scheduled", Value = "SCHEDULED"},
            };
        }
    }
}
