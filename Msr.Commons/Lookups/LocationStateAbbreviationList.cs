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
        public static List<SelectListItem> LocationStateAbbreviationList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem {Text = "87459", Value = "AZ"},
                new SelectListItem {Text = "87666", Value = "OR"},
                new SelectListItem {Text = "87873", Value = "Israel"},
                new SelectListItem {Text = "88080", Value = "Ireland"}
            };
        }
    }
}
