using System.Collections.Generic;
using System.Web.Mvc;

public static partial class LookupItems
{
    public static List<SelectListItem> System()
    {
        return new List<SelectListItem>
        {
            new SelectListItem
                {
                    Text = @"",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = @"SYS_COMP_TEST",
                    Value = "SYS_COMP_TEST",
                },
                new SelectListItem
                {
                    Text = @"SYS_CONSUME",
                    Value = "SYS_CONSUME"
                },
                new SelectListItem
                {
                    Text = @"SYS_CREATE",
                    Value = "SYS_CREATE"
                },
                new SelectListItem
                {
                    Text = @"SYS_DNR",
                    Value = "SYS_DNR"
                },
                new SelectListItem
                {
                    Text = @"SYS_E_ACCESS",
                    Value = "SYS_E_ACCESS"
                },
                new SelectListItem
                {
                    Text = @"SYS_INSTALL",
                    Value = "SYS_INSTALL"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_AND_CONSUMED",
                    Value = "SYS_PROVIDE_AND_CONSUMED"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_AND_STAY",
                    Value = "SYS_PROVIDE_AND_STAY"
                },
                new SelectListItem
                {
                    Text = @"SYS_PROVIDE_TAKE_BACK",
                    Value = "SYS_PROVIDE_TAKE_BACK"
                },
                new SelectListItem
                {
                    Text = @"SYS_RECEIVE",
                    Value = "SYS_RECEIVE"
                },
                new SelectListItem
                {
                    Text = @"SYS_REMOVE",
                    Value = "SYS_REMOVE"
                },
                new SelectListItem
                {
                    Text = @"SYS_SEND",
                    Value = "SYS_SEND"
                },
                new SelectListItem
                {
                    Text = @"SYS_SERIALIZE",
                    Value = "SYS_SERIALIZE"
                },
                new SelectListItem
                {
                    Text = @"SYS_SHIPPING",
                    Value = "SYS_SHIPPING"
                }
        };
    }
}