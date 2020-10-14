using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class SensorAppService :
        ICommandHandler<GetSensor>,
        ICommandHandler<GetSensorName>,
        ICommandHandler<GetSensorValue>
    {
        private readonly ISensorService _sensorService;

        public SensorAppService(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        public async Task<ICommandResponse> HandleAsync(GetSensor command, CancellationToken cancellationToken = default)
        {
            var ret = await _sensorService.GetSensor(command);
            return new CommandResponse<IEnumerable<SensorModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(GetSensorName command, CancellationToken cancellationToken = default)
        {
            var ret = await _sensorService.GetSensorName(command);
            return new CommandResponse<IEnumerable<string>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetSensorValue command, CancellationToken cancellationToken = default)
        {
            var ret = await _sensorService.GetSensorValue(command);
            return new CommandResponse<SensorValueModel>(ret);
        }
    }
}
