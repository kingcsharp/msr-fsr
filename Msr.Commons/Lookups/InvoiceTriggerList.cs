using System.Collections.Generic;
using System.Web.Mvc;

namespace Msr.Commons.Lookups
{
    public static partial class LookupItems
    {
        public static List<SelectListItem> InvoiceTriggerList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"All Line Items On Purchase Debited",
                    Value = "ALL_PURCHASE_ITEMS",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = @"Each Line Item On Purchase Debited",
                    Value = "EVERY_DEBIT"
                },
                new SelectListItem
                {
                    Text = @"Periodically",
                    Value = "PERIODIC"
                },
                new SelectListItem
                {
                    Text = @"Manually",
                    Value = "MANUAL"
                }
            };
        }
    }
}
