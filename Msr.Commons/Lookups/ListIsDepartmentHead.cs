using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListIsDepartmentHead()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Just A Worker",
                    Value = "0",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Is Department Head",
                    Value = "1"
                }
            };
        }
    }
}
