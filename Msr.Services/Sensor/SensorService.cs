using Msr.Models.Sensor;
using Msr.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Msr.Services.Sensor
{
    public class SensorService
    {

        private readonly MsrDbContext _dbContext;

        public SensorService()
        {
            _dbContext = new MsrDbContext();
        }

        public SensorDataModel GetSensor(int sensorMappingID)
        {
            string sqlGetSensorCurrentValues = $"SELECT * FROM DBO.SENSOR_CURRENT_VALUES S WHERE S.SENSORMAPPINGID = {sensorMappingID}";

            SensorDataModel sensorDataModel = _dbContext.Database.SqlQuery<SensorDataModel>(sqlGetSensorCurrentValues).FirstOrDefault();

            return sensorDataModel;
        }

        public List<SensorDataModel> GetSensorCurrentValues()
        {
            string sqlGetSensorCurrentValues = "SELECT * FROM DBO.SENSOR_CURRENT_VALUES";

            List<SensorDataModel> sensorDataModels = _dbContext.Database.SqlQuery<SensorDataModel>(sqlGetSensorCurrentValues).ToList();

            sensorDataModels = sensorDataModels.OrderBy(x => x.SiteName).ThenBy(x => x.SensorName).ToList();

            return sensorDataModels;
        }

    }
}
