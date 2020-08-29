using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProcedureStepTemplateService
    {
        Task<ICollection<ProcedureStepTemplateModel>> GetProcedureStepTemplateAsync(GetProcedureStepTemplate command);
        Task<ProcedureStepTemplateModel> CreateProcedureStepTemplateAsync(CreateProcedureStepTemplate command);
        Task<ProcedureStepTemplateModel> UpdateProcedureStepTemplateAsync(UpdateProcedureStepTemplate command);
        Task<bool> DeleteProcedureStepTemplateAsync(DeleteProcedureStepTemplate command);
    }
}
