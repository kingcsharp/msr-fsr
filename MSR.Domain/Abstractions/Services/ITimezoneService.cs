using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Domain.Abstractions.Services
{
    public interface ITimezoneService
    {
        Task<ICollection<TimeZoneModel>> GetTimezone(GetTimezone command);
    }
}