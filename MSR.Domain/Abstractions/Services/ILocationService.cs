using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ILocationService
    {
        Task<ICollection<LocationModel>> GetLocationsAsync(GetLocations command);
        Task<LocationModel> CreateLocationAsync(CreateLocation command);
        Task<LocationModel> UpdateLocationAsync(UpdateLocation command);
        Task DeactivateLocationAsync(DeactivateLocation command);
    }
}
