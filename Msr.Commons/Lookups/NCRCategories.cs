using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> NCRCategories()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Damaged in Handling", Value = "Damaged in Handling"},
                new SelectListItem {Text = "Damaged in Storage", Value = "Damaged in Storage"},
                new SelectListItem {Text = "Damaged in Transit", Value = "Damaged in Transit"},
                new SelectListItem {Text = "Defect Appearance", Value = "Defect Appearance"},
                new SelectListItem {Text = "Defect Functional", Value = "Defect Functional"},
                new SelectListItem {Text = "Defect Material", Value = "Defect Material"},
                new SelectListItem {Text = "Defect Peformance", Value = "Defect Peformance"},
                new SelectListItem {Text = "Defect Process", Value = "Defect Process"},
                new SelectListItem {Text = "Defect Tolerance", Value = "Defect Tolerance"},
                new SelectListItem {Text = "Wrong Product", Value = "Wrong Product"},
                new SelectListItem {Text = "Wrong ID/Traceability", Value = "Wrong ID/Traceability"},
                new SelectListItem {Text = "Cust NC - chips, cracks, breakage", Value = "Cust NC - chips, cracks, breakage"},
                new SelectListItem {Text = "Cust NC - scratches, pitting", Value = "Cust NC - scratches, pitting"},
                new SelectListItem {Text = "Cust NC - other damage", Value = "Cust NC - other damage"},
                new SelectListItem {Text = "Cust NC - end of life", Value = "Cust NC - end of life"},
                new SelectListItem {Text = "Cust NC - false leak check", Value = "Cust NC - false leak check"},
                new SelectListItem {Text = "Cust NC - CU protocol violation", Value = "Cust NC - CU protocol violation"},
                new SelectListItem {Text = "Cust NC - inadequate packaging", Value = "Cust NC - inadequate packaging"},
                new SelectListItem {Text = "Cust NC - missing parts/subparts", Value = "Cust NC - missing parts/subparts"},
                new SelectListItem {Text = "Cust NC - wrong product", Value = "Cust NC - wrong product"},
                new SelectListItem {Text = "Cust NC - shipped to wrong location", Value = "Cust NC - shipped to wrong location"},
                new SelectListItem {Text = "Cust NC - incorrect paperwork", Value = "Cust NC - incorrect paperwork"},
                new SelectListItem {Text = "Cust NC - cannot disassemble", Value = "Cust NC - cannot disassemble"},
                new SelectListItem {Text = "Cust NC - other damage", Value = "Cust NC - other damage"}
            };
        }
    }
}