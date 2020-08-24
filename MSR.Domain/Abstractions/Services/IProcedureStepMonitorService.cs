using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProcedureStepMonitorService
    {
        Task<ICollection<ProcedureStepMonitor>> GetProcedureStepMonitorAsync(GetProcedureStepMonitor command);
        Task<MonitorModel> GetMonitorModelAsync(GetMonitorModel command);
        Task<ProcedureStepMonitor> CreateProcedureStepMonitorAsync(CreateProcedureStepMonitor command);
        Task<ProcedureStepMonitor> UpdateProcedureStepMonitorAsync(UpdateProcedureStepMonitor command);
        Task<ProcedureStepMonitorDefinition> GetProcedureStepMonitorDefinitionAsync(GetProcedureStepMonitorDefinition command);
        Task<bool> DeleteMonitorModelAsync(DeleteProcedureStepMonitor command);
    }
}
