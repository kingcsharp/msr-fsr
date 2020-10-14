using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class GetSensorValue: Command
    {
        public string SensorName { get; set; }
        public int SiteId { get; set; }
    }
}
