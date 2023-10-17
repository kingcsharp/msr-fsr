using System;
namespace MSR.Domain.Views
{
	public class IntelWorkOrderPartView
	{
        public int WorkOrderPartId { get; set; }
        public int WorkOrderId { get; set; }
        public string UnitNumber { get; set; }
        public string ThisDocumentGenerationDateTime { get; set; }
        public string ScheduledShipDate { get; set; }
        public string ResponsiblePartyEmail { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ProductName { get; set; }
        public string PartRevisionNumber { get; set; }
        public string ManufacturingPlantCode { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int ManufacturerOrderNumber { get; set; }
        public string ManufacturerNumber { get; set; }
        public string ManufacturerName { get; set; }
        public string LotCreatedDate { get; set; }
        public string KitNumber { get; set; }
        public string KitName { get; set; }
        public int CycleCount { get; set; }
        public string CustomerPartNumber { get; set; }
        public string CustomerPartName { get; set; }
        public string CustomerName { get; set; }
        public string? ActualShipDate { get; set; }

    }
}
