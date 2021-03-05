using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumWorkOrderHistoryViewSortFields
    {
        [Description("PurchaseId")]
        PurchaseId,
        [Description("WorkOrderItemNumber")]
        WorkOrderItemNumber,
        [Description("Customer")]
        Customer,
        [Description("Location")]
        Location,
        [Description("SerialNumber")]
        SerialNumber,
        [Description("PurchaseOrderNumber")]
        PurchaseOrderNumber,
        [Description("Qty")]
        Qty,
        [Description("ScheduledStartDate")]
        ScheduledStartDate,
        [Description("ScheduledEndDate")]
        ScheduledEndDate,
        [Description("ActualStartDate")]
        ActualStartDate,
        [Description("ActualEndDate")]
        ActualEndDate,
        [Description("Product")]
        Product,
        [Description("Procedure")]
        Procedure,
        [Description("Status")]
        Status,
        [Description("Dispostion")]
        Dispostion
    }
}
