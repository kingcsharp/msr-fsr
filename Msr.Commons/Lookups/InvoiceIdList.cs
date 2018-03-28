using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> InvoiceIdList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "PDX",
                    Value = "03",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "PHX",
                    Value = "04"
                },
                new SelectListItem
                {
                    Text = "IRE",
                    Value = "05"
                },
            };
        }
    }
}
