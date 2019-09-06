using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Sensor
{
    public class SensorDataModel
    {

        public int SensorMappingID { get; set; }
        public string SiteName { get; set; }
        public string SensorName { get; set; }
        public string SensorCurrentValue { get; set; }

        public string SiteSensorName
        {
            get
            {
                return $"{SiteName} - {SensorName}";
            }
        }

        public string SiteSensorNameValue
        {
            get
            {
                return $"{SiteName} - {SensorName} (Current Value: {SensorCurrentValue})";
            }
        }

    }
}
