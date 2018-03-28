using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> ListRealUserTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Answer User",
                    Value = "ANSWER_USER",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Answer Contact",
                    Value = "NON_ANSWER_USER"
                }
            };
        }
    }
}
