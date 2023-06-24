namespace MSR.Domain.Models
{
    public class ProcedureStepMonitor
    {
        public ProcedureStepMonitor() { }

        public int Id { get; set; }
        public string MonitorType { get; set; }
        public int MonitorTypeId { get; set; }
        public string InputType { get; set; }
        public int InputTypeId { get; set; }
        public string SensorName { get; set; }
        public string ShouldBe { get; set; }
        public decimal? Target { get; set; }
        public string FailAction { get; set; }
        public string Description { get; set; }
        public bool? SendNCREmail { get; set; }
        public decimal? HighTarget { get; set; }
        public decimal? LowTarget { get; set; }
        public int? MonitorListId { get; set; }
    }
}
