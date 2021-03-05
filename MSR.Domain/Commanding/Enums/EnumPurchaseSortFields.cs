using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumPurchaseSortFields
    {
        [Description("Id")]
        Id,
        [Description("Mttn")]
        Mttn,
        [Description("PurchaseOrderProductName")]
        PurchaseOrderProductName,
        [Description("StatusId")]
        StatusId,
        [Description("CreatedOn")]
        CreatedOn
    }
}
