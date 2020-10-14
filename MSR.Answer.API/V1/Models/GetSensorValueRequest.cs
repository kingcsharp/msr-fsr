using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class GetSensorValueRequest
    {
        public string SensorName { get; set; }
        public int SiteId { get; set; }
    }
}
