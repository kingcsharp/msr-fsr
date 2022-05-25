using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProcedureService
    {
        Task<ICollection<Procedure>> GetProcedureAsync(GetProcedure command);
        Task<Procedure> CreateProcedureAsync(CreateProcedure command, bool isImport = false);
        Task<Procedure> CopyProcedureAsync(CopyProcedure command);
        Task<Procedure> UpdateProcedureAsync(UpdateProcedure command, bool isImport = false);
        Task<ICollection<ProcedureStepModel>> GetProcedureStepAsync(GetProcedureStep command);
        Task<ProcedureStepModel> CreateProcedureStepAsync(CreateProcedureStep command, bool isImport = false);
        Task<ProcedureStepModel> UpdateProcedureStepAsync(UpdateProcedureStep command, bool incRevision = true, bool isImport = false);
        Task<bool> DeleteProcedureAsync(DeleteProcedure command);
        Task<ProcedureStepModel> DeleteProcedureStepAsync(DeleteProcedureStep command);
        Task<ICollection<ProcedureStepTypeModel>> GetProcedureStepType(GetProcedureStepType command);
        Task<ICollection<Procedure>> ImportProcedures(byte[] xlsData);
    }
}
