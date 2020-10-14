using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ISensorService
    {
        Task<IEnumerable<SensorModel>> GetSensor(GetSensor command);
        Task<IEnumerable<string>> GetSensorName(GetSensorName command);
    }
}
