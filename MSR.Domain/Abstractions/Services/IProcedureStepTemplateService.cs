using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProcedureStepTemplateService
    {
        Task<ICollection<ProcedureStepTemplate>> GetProcedureStepTemplateAsync(GetProcedureStepTemplate command);
        Task<ProcedureStepTemplate> CreateProcedureStepTemplateAsync(CreateProcedureStepTemplate command);
        Task<ProcedureStepTemplate> UpdateProcedureStepTemplateAsync(UpdateProcedureStepTemplate command);
    }
}
