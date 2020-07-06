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

    public class LocationAppService : 
        ICommandHandler<GetLocations>,
        ICommandHandler<CreateLocation>,
        ICommandHandler<UpdateLocation>,
        ICommandHandler<DeactivateLocation>
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

        public async Task<ICommandResponse> HandleAsync(DeactivateLocation command, CancellationToken cancellationToken = default)
        {
            await _locationService.DeactivateLocationAsync(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(UpdateLocation command, CancellationToken cancellationToken = default)
        {
            var ret = await _locationService.UpdateLocationAsync(command);
            return new CommandResponse<Location>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateLocation command, CancellationToken cancellationToken = default)
        {
            var ret = await _locationService.CreateLocationAsync(command);
            return new CommandResponse<Location>(ret);
        }
    }
}
