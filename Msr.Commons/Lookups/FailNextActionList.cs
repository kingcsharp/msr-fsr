using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> FailNextActionList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "CONTINUE", Value = "CONTINUE"},
                new SelectListItem {Text = "DONOTCLOSE", Value = "DONOTCLOSE"},
                new SelectListItem {Text = "DNR", Value = "DNR"},
                new SelectListItem {Text = "DNR", Value = "DNR"}
            };
        }
    }
}
