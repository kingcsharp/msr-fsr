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
                new SelectListItem {Text = "NUMBER", Value = "NUMBER"},
                new SelectListItem {Text = "USER_NUMBER", Value = "USER_NUMBER"},
                new SelectListItem {Text = "YES_NO", Value = "YES_NO"},
                new SelectListItem {Text = "MULTIPLE", Value = "MULTIPLE"},
                new SelectListItem {Text = "TEXT", Value = "TEXT"},
                new SelectListItem {Text = "OBJECT", Value = "OBJECT"}
            };
        }
    }
}
