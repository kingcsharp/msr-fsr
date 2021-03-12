using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class WorkOrderHistoryView
    {
        private EnumSegregationType _segregationType;

        public int? WorkOrderId { get; set; }
        public bool HasNcr { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public string SerialNumber { get; set; }
        public int PurchaseId { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public int? Qty { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Product { get; set; }
        public string Procedure { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? Disposition { get; set; }
        public EnumSegregationType SegregationType { get;set;}
        public string WorkOrderItemNumber
        {
            get => $"{Customer}-{WorkOrderId}";
        }
    }
}
