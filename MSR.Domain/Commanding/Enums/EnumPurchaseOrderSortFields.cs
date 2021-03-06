using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumPurchaseOrderSortFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("CustomerReferencePO")]
        CustomerReferencePO,
        [Description("InvoicedBalance")]
        InvoicedBalance,
        [Description("UninvoicedBalance")]
        UninvoicedBalance,
        [Description("Balance")]
        Balance,
        [Description("CustomerName")]
        CustomerName,
        [Description("OpenDate")]
        OpenDate,
        [Description("CloseDate")]
        CloseDate,
        [Description("TotalPurchaseLimit")]
        TotalPurchaseLimit,
        [Description("UnusedAmount")]
        UnusedAmount,
        [Description("Revision")]
        Revision,
        [Description("Status")]
        Status
    }
}
