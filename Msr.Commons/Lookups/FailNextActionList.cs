using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

public static partial class LookupItems
{
    public static List<SelectListItem> FailNextActionList()
    {
        return new List<SelectListItem>
        {
            new SelectListItem {Text = "CONTINUE", Value = "CONTINUE"},
            new SelectListItem {Text = "DONOTCLOSE", Value = "DONOTCLOSE"},
            new SelectListItem {Text = "DNR", Value = "DNR"},
            new SelectListItem {Text = "DNR", Value = "DNR"}
        };
    }
}
