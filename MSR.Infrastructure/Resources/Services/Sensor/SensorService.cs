using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<SensorItemModel>> GetSensor(GetSensor command)
        { 
            if (command.SensorId.HasValue)
            {
                var retSensors = new List<SensorItemModel>();
                var sensor = await _unitOfwork.Sensors.Query().Include(i => i.AssignedLocation).FirstOrDefaultAsync(i => i.Id == command.SensorId);

                retSensors.Add(_mapper.Map<SensorItemModel>(sensor));
                return retSensors;
            }

            return _unitOfwork.Sensors.Query().Include(i => i.AssignedLocation).Where(i => !i.LocationId.HasValue).Select(i => _mapper.Map<SensorItemModel>(i)).AsEnumerable();
        }
    }
}
