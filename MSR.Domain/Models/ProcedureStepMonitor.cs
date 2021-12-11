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
        /// <description>
        /// "Should be" is the relation between hi/low or target,
        /// e.g. BETWEEN, EQUAL, and other strings
        /// </description>
        /// <example>BETWEEN</example>
        public string ShouldBe { get; set; }

        /// <summary>
        /// Gets or Sets Target
        /// </summary>
        public decimal? Target { get; set; }

        /// <summary>
        /// Gets or Sets FailAction
        /// </summary>
        public string FailAction { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets SendNCREmail
        /// </summary>
        public bool? SendNCREmail { get; set; }

        /// <summary>
        /// HighTarget
        /// </summary>
        public decimal? HighTarget { get; set; }

        /// <summary>
        /// LowTarget
        /// </summary>
        public decimal? LowTarget { get; set; }

        /// <summary>
        /// MonitorListId
        /// </summary>
        public int? MonitorListId { get; set; }
    }
}
