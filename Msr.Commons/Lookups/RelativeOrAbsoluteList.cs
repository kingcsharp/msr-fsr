using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> RelativeOrAbsoluteList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = @"Relative",
                    Value = "RELATIVE"
                },
                new SelectListItem()
                {
                    Text = @"Absolute",
                    Value = "ABSOLUTE"
                }
            };
        }
    }
}
