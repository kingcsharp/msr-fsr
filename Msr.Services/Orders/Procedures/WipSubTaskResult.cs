
using System;

namespace Msr.Services.Orders.Procedures
{
    public class WipSubTaskResult
    {
        public string Id { get; set; }

        public string TaskId { get; set; }

        public string MonitorType { get; set; }

        public string Description { get; set; }

        public string PrintResult { get; set; }

        public string IsPassing { get; set; }

        public string TaskStatus { get; set; }

        public string YesNoAnswer { get; set; }

        public string NumValue { get; set; }

        public string TextValue { get; set; }
    }
}
