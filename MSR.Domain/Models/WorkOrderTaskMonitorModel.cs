using MSR.Domain.Models.BaseModels;
using System.Runtime.Serialization;

namespace MSR.Domain.Models
{
    public class WorkOrderTaskMonitorModel : TrackableModel
    {
        public int WorkOrderTaskId { get; set; }
        public virtual WorkOrderTaskModel WorkOrderTask { get; set; }

        public virtual ProcedureStepMonitor ProcedureStepMonitor { get; set; }

        /// <summary>
        /// Procedure Step Monitor Id
        /// </summary>
        public int? ProcedureMonitorId { get; set; }

        public decimal? NumVal { get; set; }
        public string TextVal { get; set; }
        public string MultiVal { get; set; }
        public int SensorMappingId { get; set; }
        public string Comment { get; set; }

        /// <summary>
        /// This needs to be added
        /// </summary>
        /// <value>This needs to be added</value>
        [DataMember(Name = "sensorValue")]
        public string SensorValue { get; set; }

        /// <summary>
        /// This needs to be added
        /// </summary>
        /// <value>This needs to be added</value>
        [DataMember(Name = "sensorName")]
        public string SensorName { get; set; }

        /// <summary>
        /// Integer index of this monitor in a list, can be 0.
        /// </summary>
        public int MonitorNumber { get; set; }

        public string Description { get; set; }
        public int? MonitorListId { get; set; }
        public string ShouldBe { get; set; }
        public decimal? HighTarget { get; set; }
        public decimal? LowTarget { get; set; }
        public decimal? Target { get; set; }
        public string FailAction { get; set; }
        public int? MonitorTypeId { get; set; }
        public int? InputTypeId { get; set; }
        public decimal? LowNumVal { get; set; }
        public decimal? HighNumVal { get; set; }
    }
}
