using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> PhoneTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text = "Choose Type", Value ="ChooseType",Selected = true},
                new SelectListItem{ Text = "SYS-PHONE-1", Value ="SYS-PHONE-1"},
                new SelectListItem{ Text = "SYS-PHONE-2", Value ="SYS-PHONE-2"},
                new SelectListItem{ Text = "SYS-PHONE-3", Value ="SYS-PHONE-3"},
                new SelectListItem{ Text = "SYS-PHONE-4", Value ="SYS-PHONE-4"},
                new SelectListItem{ Text = "SYS-PHONE-5", Value ="SYS-PHONE-5"},
                new SelectListItem{ Text = "SYS-PHONE-6", Value ="SYS-PHONE-6"},
                new SelectListItem{ Text = "SYS-PHONE-7", Value ="SYS-PHONE-7"},
                new SelectListItem{ Text = "SYS-PHONE-8", Value ="SYS-PHONE-8"},
                new SelectListItem{ Text = "SYS-PHONE-9", Value ="SYS-PHONE-9"},
                new SelectListItem{ Text = "SYS-PHONE-10", Value ="SYS-PHONE-10"},
                new SelectListItem{ Text = "SYS-PHONE-11", Value ="SYS-PHONE-11"},
                new SelectListItem{ Text = "SYS-PHONE-12", Value ="SYS-PHONE-12"},
                new SelectListItem{ Text = "SYS-PHONE-13", Value ="SYS-PHONE-13"},
                new SelectListItem{ Text = "SYS-PHONE-14", Value ="SYS-PHONE-14"},
                new SelectListItem{ Text = "SYS-PHONE-15", Value ="SYS-PHONE-15"}
            };
        }
    }
}
