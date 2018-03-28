using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> Spares()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"No",
                    Value = "SPARE_NO",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Level 1",
                    Value = "SPARE_1"
                },
                new SelectListItem
                {
                    Text = @"Level 2",
                    Value = "SPARE_2"
                },
                new SelectListItem
                {
                    Text = @"Level 3",
                    Value = "SPARE_3"
                }
            };
        }
    }
}
