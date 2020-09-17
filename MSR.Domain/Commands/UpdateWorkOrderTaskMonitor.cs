using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderTaskMonitor : Command
    {
        public int Id { get; set; }
        public int? NumVal { get; set; }
        public string TextVal { get; set; }
        public string MultiVal { get; set; }
        public string SensorValue { get; set; }
        public string Comment { get; set; }
    }
}
