using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListEmailTextTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Choose Type",
                    Value = "ChooseType",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "HTML",
                    Value = "HTML"
                },
                new SelectListItem
                {
                    Text = "Text",
                    Value = "Text"
                }
            };
        }
    }
}