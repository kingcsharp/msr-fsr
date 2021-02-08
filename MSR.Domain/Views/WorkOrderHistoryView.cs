using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class WorkOrderHistoryView
    {
        private string _segregationType;

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
        public string? Dispostion { get; set; }
        public string SegregationType { 
            get  {


                if (String.IsNullOrWhiteSpace(_segregationType)) {
                    return EnumUtils.GetDescription<EnumSegregationType>(EnumSegregationType.NONCU);
                }
                else { 
                    return _segregationType;    
                }
            } 

            set { 
               _segregationType = value;
            } 
        }
        public string WorkOrderItemNumber
        {
            get => $"{Customer}-{WorkOrderId}";
        }
    }
}
