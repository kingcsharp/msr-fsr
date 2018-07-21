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
                new SelectListItem {Text = "RECORD AND CONTINUE", Value = "CONTINUE"},
                new SelectListItem {Text = "STOP UNTIL FAULT CLEARED", Value = "DONOTCLOSE"}
            };
        }
    }
}
