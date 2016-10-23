using System;

namespace Msr.Models.Tasks
{
    public class MonitorResult
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string MonitorType { get; set; }
        public string Description { get; set; }
        public string ShouldBe { get; set; }
        public short? Opinion { get; set; }
        public short? HideTarget { get; set; }
        public short? UseResult { get; set; }
        public short? FailStop { get; set; }
        public short? YesNoAnswer { get; set; }
        public string TextVal { get; set; }
        public string Comment { get; set; }
        public byte? IsPassing { get; set; }
        public string MyAnswer { get; set; }
        public string FailAction { get; set; }
        public double? PrintOrder { get; set; }
        public byte? CantChange { get; set; }
        public byte? AlwaysPass { get; set; }
    }
}
