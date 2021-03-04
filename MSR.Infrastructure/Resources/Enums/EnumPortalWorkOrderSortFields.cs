using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Infrastructure.Resources.Enums
{
    public enum EnumPortalWorkOrderSortFields
    {
        [Description("WorkOrderId")]
        WorkOrderId,
        [Description("SerialNumber")]
        SerialNumber,
        [Description("CompanyPartNumber")]
        CompanyPartNumber,
        [Description("CycleCount")]
        CycleCount,
        [Description("PurchaseOrderNumber")]
        PurchaseOrderNumber,
        [Description("Qty")]
        Qty,
        [Description("StartDate")]
        StartDate,
        [Description("DueDate")]
        DueDate,
        [Description("PartName")]
        PartName,
        [Description("ProductName")]
        ProductName,
        [Description("ProcedureName")]
        ProcedureName,
        [Description("Status")]
        Status
    }
}
