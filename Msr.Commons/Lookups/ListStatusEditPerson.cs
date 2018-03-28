using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListStatusEditPerson()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Active",
                    Value = "Active",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Inactive",
                    Value = "Inactive"
                }
            };
        }
    }
}
