using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Sensor
{
    public class SensorService : ISensorService
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public SensorService(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SensorModel>> GetSensor(GetSensor command)
        {
            if (command.SensorId.HasValue)
            {
                var retSensors = new List<SensorModel>();
                var sensor = await _unitOfwork.Sensors.Query().Include(i => i.AssignedLocation).FirstOrDefaultAsync(i => i.Id == command.SensorId);

                retSensors.Add(_mapper.Map<SensorModel>(sensor));
                return retSensors;
            }

            var sensors = _unitOfwork.Sensors.Query();

            if (command.SiteId.HasValue)
            {
                sensors = sensors.Where(i => i.SiteId == command.SiteId);
            }


            return sensors.Include(i => i.AssignedLocation).Include(i => i.Site).Where(i => !i.AssignedLocationId.HasValue).Select(i => _mapper.Map<SensorModel>(i)).AsEnumerable();
        }

        public async Task<IEnumerable<string>> GetSensorName(GetSensorName command)
        {

            var sensorName = await _unitOfwork.Sensors.Query().Select(s => s.SensorName).Distinct().ToListAsync();

            return sensorName;

        }

        public async Task<SensorValueModel> GetSensorValue(GetSensorValue command)
        {
            var sensorValueEntity = await _unitOfwork.SensorValues.Query().FirstOrDefaultAsync(s => s.Sensor.SensorName == command.SensorName && s.Sensor.SiteId == command.SiteId);
            return _mapper.Map<SensorValueModel>(sensorValueEntity);
        }
    }
}
