using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProcedureTypeService
    {
        Task<ICollection<ProcedureType>> GetProcedureTypeAsync(GetProcedureType command);
        Task<ProcedureType> CreateProcedureTypeAsync(CreateProcedureType command);
        Task<ProcedureType> UpdateProcedureTypeAsync(UpdateProcedureType command);
        Task<bool> DeleteProcedureTypeAsync(DeleteProcedureType command);
    }
}
