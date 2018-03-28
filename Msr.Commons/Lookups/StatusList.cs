using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> StatusList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "INVOICED",
                    Value = "INVOICED",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "OUTSTANDING",
                    Value = "OUTSTANDING"
                },
                new SelectListItem
                {
                    Text = "OVERDUE",
                    Value = "OVERDUE"
                },
                new SelectListItem
                {
                    Text = "CLOSED",
                    Value = "CLOSED"
                }
            };
        }
    }
}
