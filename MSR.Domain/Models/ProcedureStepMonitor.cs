using Newtonsoft.Json;
using System.Text;

namespace MSR.Domain.Models
{
    public class ProcedureStepMonitor
    {
        public ProcedureStepMonitor() { }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets MonitorType
        /// </summary>
        public string MonitorType { get; set; }

        /// <summary>
        /// Gets or Sets MonitorTypeId
        /// </summary>
        public int MonitorTypeId { get; set; }

        /// <summary>
        /// Gets or Sets InputType
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// Gets or Sets InputType
        /// </summary>
        public int InputTypeId { get; set; }

        /// <summary>
        /// Sensor Name
        /// </summary>
        /// <description>
        /// Sensor Name is only valid if this monitor is
        /// of input type == sensor.
        /// </description>
        public string SensorName { get; set; }

        /// <summary>
        /// Gets or Sets ShouldBe
        /// </summary>
        public string ShouldBe { get; set; }

        /// <summary>
        /// Gets or Sets TargetValue
        /// </summary>
        public string TargetValue { get; set; }

        /// <summary>
        /// Gets or Sets FaultHandling
        /// </summary>
        public string FaultHandling { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets SendEmailNotification
        /// </summary>
        public bool? SendEmailNotification { get; set; }

        /// <summary>
        /// HighTarget
        /// </summary>
        public float? HighTarget { get; set; }

        /// <summary>
        /// LowTarget
        /// </summary>
        public float? LowTarget { get; set; }
    }
}
