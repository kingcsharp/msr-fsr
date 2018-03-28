using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> EmailTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose Type", Value = "ChooseType", Selected = true},
                new SelectListItem {Text = "SYS-EMAIL-1", Value = "SYS-EMAIL-1"},
                new SelectListItem {Text = "SYS-EMAIL-2", Value = "SYS-EMAIL-2"},
                new SelectListItem {Text = "SYS-EMAIL-3", Value = "SYS-EMAIL-3"},
                new SelectListItem {Text = "SYS-EMAIL-4", Value = "SYS-EMAIL-4"},
                new SelectListItem {Text = "SYS-EMAIL-5", Value = "SYS-EMAIL-5"},
                new SelectListItem {Text = "SYS-EMAIL-6", Value = "SYS-EMAIL-6"},
                new SelectListItem {Text = "SYS-EMAIL-7", Value = "SYS-EMAIL-7"},
                new SelectListItem {Text = "SYS-EMAIL-8", Value = "SYS-EMAIL-8"},
                new SelectListItem {Text = "SYS-EMAIL-9", Value = "SYS-EMAIL-9"},
                new SelectListItem {Text = "SYS-EMAIL-10", Value = "SYS-EMAIL-10"},
                new SelectListItem {Text = "SYS-EMAIL-11", Value = "SYS-EMAIL-11"},
                new SelectListItem {Text = "SYS-EMAIL-12", Value = "SYS-EMAIL-12"},
                new SelectListItem {Text = "SYS-EMAIL-13", Value = "SYS-EMAIL-13"},
                new SelectListItem {Text = "SYS-EMAIL-14", Value = "SYS-EMAIL-14"},
                new SelectListItem {Text = "SYS-EMAIL-15", Value = "SYS-EMAIL-15"}
            };
        }
    }
}
