using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> YesNo()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Yes", Value = "1"},
                new SelectListItem {Text = "No", Value = "0"}
            };
        }
    }
}