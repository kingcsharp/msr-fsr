using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> SecurityLevels()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"1 View What All Users Are Allowed to View",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"2 View What Managers & Below Are Allowed to View",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = @"3 View What Directors & Below Are Allowed to View",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = @"4 View What VP's & Below Are Allowed to View",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = @"System",
                    Value = "5"
                }
            };
        }
    }
}
