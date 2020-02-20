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
                new SelectListItem {Text = "87459", Value = "480-705-9717"},
                new SelectListItem {Text = "87666", Value = "503-492-0100"},
                new SelectListItem {Text = "87873", Value = "083731730"},
                new SelectListItem {Text = "88080", Value = "353 (0) 45435829"}

            };
        }
    }
}
