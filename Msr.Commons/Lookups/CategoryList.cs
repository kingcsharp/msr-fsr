using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> CategoryList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Wip",
                    Value = "Wip",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Roles",
                    Value = "Roles"
                },
                new SelectListItem
                {
                    Text = "People",
                    Value = "People"
                },
                new SelectListItem
                {
                    Text = "Invoices",
                    Value = "Invoices"
                }
            };
        }
    }
}
