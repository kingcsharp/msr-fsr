using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class SensorAppService :
        ICommandHandler<GetSensor>
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
    }
}
