using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ConsumablesPartType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Not Consumable Part",
                    Value = "PTCON_NO"
                },
                new SelectListItem
                {
                    Text = @"Consumable Part",
                    Value = "PTCON_YES",
                    Selected = true
                }
            };
        }
    }
}
