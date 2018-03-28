using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> Consumables()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"No",
                    Value = "CON_NO",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Yes",
                    Value = "CON_YES"
                }
            };
        }
    }
}
