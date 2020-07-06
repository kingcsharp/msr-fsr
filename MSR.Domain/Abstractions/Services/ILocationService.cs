using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ILocationService
    {
        Task<ICollection<Location>> GetLocationsAsync(GetLocations command);
        Task<Location> CreateLocationAsync(CreateLocation command);
        Task<Location> UpdateLocationAsync(UpdateLocation command);
        Task DeactivateLocationAsync(DeactivateLocation command);
    }
}
