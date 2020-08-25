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
    public class TimezoneAppService :
        ICommandHandler<GetTimezone>
    {
        private readonly ITimezoneService _timezoneService;
        public TimezoneAppService(ITimezoneService timezoneService)
        {
            _timezoneService = timezoneService;
        }
        public async Task<ICommandResponse> HandleAsync(GetTimezone command, CancellationToken cancellationToken = default)
        {
            var ret = await _timezoneService.GetTimezone(command);
            return new CommandResponse<ICollection<TimeZoneModel>>(ret);
        }
    }
}