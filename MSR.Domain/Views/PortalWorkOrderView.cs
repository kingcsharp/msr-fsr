using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class PortalWorkOrderView
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SerialNumber { get; set; }
        public string CompanyPartNumber { get; set; }
        public int? CycleCount { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int? Qty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string PartName { get; set; }
        public int? PartId { get; set; }
        public string ProductName { get; set; }
        public string ProcedureName { get; set; }
        public string Status { get; set; }
        public int? WorkOrderTaskId { get; set; }
        public bool HasPhotos { get; set; }
        public bool HasNCRs { get; set; }
        public bool HasFiles { get; set; }
        public bool HasMonitors { get; set; }
        public string Disposition { get; set; }
        public string Supplier { get; set; }
        public decimal? Price { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceName { get; set; }
        public decimal? PercentageOfTasksCompleted { get; set; }
        public int? PercentageOfTasksCompletedNumerator { get; set; }
        public int? PercentageOfTasksCompletedDenominator { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLogged { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLoggedNumerator { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLoggedDenominator { get; set; }
        public ICollection<WorkOrderMessageModel> Messages { get; set; }
        public ICollection<PortalSubPartView> SubParts { get; set; }
        public string StepText { get; set; }
    }
}
