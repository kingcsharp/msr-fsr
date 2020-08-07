using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IPartService
    {
        Task<ICollection<PartModel>> GetPartsAsync(GetParts command);
        Task<PartModel> CreatePartAsync(CreatePart command);
        Task<PartModel> UpdatePartAsync(UpdatePart command);
        Task<PartModel> DeletePartAsync(DeletePart command);
        int ImportPartsAsync(ImportParts command);
    }
}
