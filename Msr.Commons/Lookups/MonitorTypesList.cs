using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> MonitorTypesList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Equipment", Value = "EQUIPMENT"},
                new SelectListItem {Text = "Number", Value = "NUMBER"},
                new SelectListItem {Text = "Yes or No", Value = "YES_NO"},
                new SelectListItem {Text = "Text", Value = "TEXT"},
                new SelectListItem {Text = "Pass or Fail", Value = "PASS_FAIL"},
                new SelectListItem {Text = "Select", Value = "SELECT"}
            };
        }
    }
}
