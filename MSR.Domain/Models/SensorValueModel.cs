using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class SensorValueModel: CreatableModel
    {
        public SensorModel Sensor { get; set; }
        public string ItemCurrentValue { get; set; }
        public string AlarmDescription { get; set; }
        public bool? IsAlarm { get; set; }
    }
}
