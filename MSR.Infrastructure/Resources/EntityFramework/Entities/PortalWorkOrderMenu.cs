using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PortalWorkOrderMenu))]
    public class PortalWorkOrderMenu: Entity
    {
        public int CustomerId { get; set; }
        public string CompanyPartNumber { get; set; }
        public int? CycleCount { get; set; }
        public DateTime? DueDate { get; set; }
        [Column("HasFile")]
        public bool HasFiles { get; set; }
        [Column("HasMonitor")]
        public bool HasMonitors { get; set; }
        public bool HasNCRs { get; set; }
        [Column("HasPhoto")]
        public bool HasPhotos { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceName { get; set; }
        public string PartName { get; set; }
        public int PartId { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLogged { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLoggedDenominator { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLoggedNumerator { get; set; }
        public decimal? PercentageOfTasksCompleted { get; set; }
        public int? PercentageOfTasksCompletedDenominator { get; set; }
        public int? PercentageOfTasksCompletedNumerator { get; set; }
        public decimal? Price { get; set; }
        public string ProcedureName { get; set; }
        public string ProductName { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int? Qty { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public string Status { get; set; }
        public int WorkOrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Disposition { get; set; }
    }
}
