using System.Collections.Generic;
using System.Web.Mvc;

public static partial class LookupItems
{
    public static List<SelectListItem> ShouldBeList()
    {
        return new List<SelectListItem>
        {
            new SelectListItem {Text = "EQUAL", Value = "EQUAL"},
            new SelectListItem {Text = "ABOVE", Value = "ABOVE"},
            new SelectListItem {Text = "BELOW", Value = "BELOW"},
            new SelectListItem {Text = "BETWEEN", Value = "BETWEEN"}
        };
    }
}
