using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProcedureService
    {
        Task<ICollection<Procedure>> GetProcedureAsync(GetProcedure command);
        Task<Procedure> CreateProcedureAsync(CreateProcedure command);
        Task<Procedure> UpdateProcedureAsync(UpdateProcedure command);
    }
}
