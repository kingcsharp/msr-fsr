using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> TypeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "DEBIT",
                    Value = "INV_ITEM_DEBIT",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "CREDIT",
                    Value = "INV_ITEM_CREDIT"
                },
            };
        }
    }
}
