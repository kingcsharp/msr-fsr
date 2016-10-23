namespace Msr.Models.Tasks
{
    public class MonitorResult
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string MonitorType { get; set; }
        public string Description { get; set; }
        public string ShouldBe { get; set; }
        public int? Opinion { get; set; }
        public int? HideTarget { get; set; }
        public int? UseResult { get; set; }
        public int? FailStop { get; set; }
        public int? YesNoAnswer { get; set; }
        public string TextVal { get; set; }
        public string Comment { get; set; }
        public int? IsPassing { get; set; }
        public string MyAnswer { get; set; }
        public string FailAction { get; set; }
        public decimal? PrintOrder { get; set; }
        public int? CantChange { get; set; }
        public int? AlwaysPass { get; set; }
    }
}
