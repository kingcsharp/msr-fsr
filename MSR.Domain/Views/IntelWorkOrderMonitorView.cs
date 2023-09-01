using System;
namespace MSR.Domain.Views
{
	public class IntelWorkOrderMonitorView
	{
        public int WorkOrderId { get; set; }
        public int WorkOrderPartId { get; set; }
        public string ShortName { get; set; }
        public string UnitOfMeasure { get; set; }
        public string MeasurementType { get; set; }
        public string MeasurementValue { get; set; }
    }
};
