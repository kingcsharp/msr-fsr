using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> KitCountList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "1",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "2",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "3",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "4",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "5",
                    Value = "5"
                },
                new SelectListItem
                {
                    Text = "6",
                    Value = "6"
                },
                new SelectListItem
                {
                    Text = "7",
                    Value = "7"
                },
                new SelectListItem
                {
                    Text = "8",
                    Value = "8"
                },
                new SelectListItem
                {
                    Text = "9",
                    Value = "9"
                },
                new SelectListItem
                {
                    Text = "10",
                    Value = "10"
                }
            };
        }
    }
}
