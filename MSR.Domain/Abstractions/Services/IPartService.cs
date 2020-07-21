using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IPartService
    {
        Task<ICollection<Part>> GetPartsAsync(GetParts command);
        Task<Part> CreatePartAsync(CreatePart command);
        Task<Part> UpdatePartAsync(UpdatePart command);
    }
}
