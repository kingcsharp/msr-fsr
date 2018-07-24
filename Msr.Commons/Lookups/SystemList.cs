using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
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
                    Text = @"Computerized Test",
                    Value = "SYS_COMP_TEST",
                },
                new SelectListItem
                {
                    Text = @"Consume",
                    Value = "SYS_CONSUME"
                },
                new SelectListItem
                {
                    Text = @"Create",
                    Value = "SYS_CREATE"
                },
                new SelectListItem
                {
                    Text = @"Diagnose And Repair",
                    Value = "SYS_DNR"
                },
                new SelectListItem
                {
                    Text = @"E-Access",
                    Value = "SYS_E_ACCESS"
                },
                new SelectListItem
                {
                    Text = @"Install",
                    Value = "SYS_INSTALL"
                },
                new SelectListItem
                {
                    Text = @"Provide and consume",
                    Value = "SYS_PROVIDE_AND_CONSUMED"
                },
                new SelectListItem
                {
                    Text = @"Provide and stay",
                    Value = "SYS_PROVIDE_AND_STAY"
                },
                new SelectListItem
                {
                    Text = @"Provide and take back",
                    Value = "SYS_PROVIDE_TAKE_BACK"
                },
                new SelectListItem
                {
                    Text = @"Receive",
                    Value = "SYS_RECEIVE"
                },
                new SelectListItem
                {
                    Text = @"Remove",
                    Value = "SYS_REMOVE"
                },
                new SelectListItem
                {
                    Text = @"Send",
                    Value = "SYS_SEND"
                },
                new SelectListItem
                {
                    Text = @"Serialize Parts",
                    Value = "SYS_SERIALIZE"
                },
                new SelectListItem
                {
                    Text = @"Shipping",
                    Value = "SYS_SHIPPING"
                }
            };
        }
    }
}