using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using MSR.Domain.Commanding;
using MSR.Domain.Abstractions.Services;
using System.Collections.Generic;
using MSR.Domain.Models;

namespace MSR.Application.ApplicationServices
{

    public class LocationAppService : ICommandHandler<GetLocations>
    {
        private readonly ILocationService _locationService;
        public LocationAppService(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<ICommandResponse> HandleAsync(GetLocations command, CancellationToken cancellationToken = default)
        {
            var ret = await _locationService.GetLocationsAsync(command);
            return new CommandResponse<ICollection<Location>>(ret);
        }
    }
}
