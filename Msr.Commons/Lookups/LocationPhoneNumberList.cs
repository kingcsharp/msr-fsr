using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> LocationPhoneNumberList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem {Text = "87459", Value = "XXX-555-2368"},
                new SelectListItem {Text = "87666", Value = "XXX-555-2368"},
                new SelectListItem {Text = "87873", Value = "XXX-555-2368"},
                new SelectListItem {Text = "88080", Value = "XXX-555-2368"}
            };
        }
    }
}
