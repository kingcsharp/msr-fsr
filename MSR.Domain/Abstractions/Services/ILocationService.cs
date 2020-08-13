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
        Task<LocationModel> DeactivateLocationAsync(DeactivateLocation command);
        Task<IEnumerable<LocationModel>> ImportLocations(string csvData);

        Task<IEnumerable<SensorItemModel>> GetSensorsForLocation(GetSensorsForLocation command);

        Task AddSensorToLocation(CreateLocationSensorMap command);
        Task RemoveSensorFromLocation(DeleteLocationSensorMap command);

    }
}
