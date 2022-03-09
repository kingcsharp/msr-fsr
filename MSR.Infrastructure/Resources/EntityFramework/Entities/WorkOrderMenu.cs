using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderMenu))]
    public class WorkOrderMenu: Entity
    {
        public int PurchaseId { get; set; }
        public string WorkOrderItemNumber { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
        public string SerialNumber { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        public string ReferencePO { get; set; }
        public int? Quantity { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string ProductName { get; set; }
        public string ProcedureName { get; set; }
        public string Status { get; set; }
        public string Disposition { get; set; }
        public string CurrentActiveTaskName { get; set; }
        public decimal? PercentageOfTasksCompleted { get; set; }
        public int? PercentageOfTasksCompletedNumerator { get; set; }
        public int? PercentageOfTasksCompletedDenominator { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLogged { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLoggedNumerator { get; set; }
        public decimal? PercentageOfExpectedDurationTimeLoggedDenominator { get; set; }
        public bool HasNcr { get; set; }
        public string SegregationType { get; set; }
        public bool? HasSubParts { get; set; }
    }
}
