using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> CompanyTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Company",
                    Value = "COMPANY",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Department",
                    Value = "DEPARTMENT"
                }
            };
        }
    }
}
