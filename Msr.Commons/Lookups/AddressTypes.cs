using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> AddressTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose Type", Value = "ChooseType", Selected = true},
                new SelectListItem {Text = "SYS-ADDRESS-1", Value = "SYS-ADDRESS-1"},
                new SelectListItem {Text = "SYS-ADDRESS-2", Value = "SYS-ADDRESS-2"},
                new SelectListItem {Text = "SYS-ADDRESS-3", Value = "SYS-ADDRESS-3"},
                new SelectListItem {Text = "SYS-ADDRESS-4", Value = "SYS-ADDRESS-4"},
                new SelectListItem {Text = "SYS-ADDRESS-5", Value = "SYS-ADDRESS-5"},
                new SelectListItem {Text = "SYS-ADDRESS-6", Value = "SYS-ADDRESS-6"},
                new SelectListItem {Text = "SYS-ADDRESS-7", Value = "SYS-ADDRESS-7"},
                new SelectListItem {Text = "SYS-ADDRESS-8", Value = "SYS-ADDRESS-8"},
                new SelectListItem {Text = "SYS-ADDRESS-9", Value = "SYS-ADDRESS-9"},
                new SelectListItem {Text = "SYS-ADDRESS-10", Value = "SYS-ADDRESS-10"},
                new SelectListItem {Text = "SYS-ADDRESS-11", Value = "SYS-ADDRESS-11"},
                new SelectListItem {Text = "SYS-ADDRESS-12", Value = "SYS-ADDRESS-12"},
                new SelectListItem {Text = "SYS-ADDRESS-13", Value = "SYS-ADDRESS-13"},
                new SelectListItem {Text = "SYS-ADDRESS-14", Value = "SYS-ADDRESS-14"},
                new SelectListItem {Text = "SYS-ADDRESS-15", Value = "SYS-ADDRESS-15"}
            };
        }
    }
}
