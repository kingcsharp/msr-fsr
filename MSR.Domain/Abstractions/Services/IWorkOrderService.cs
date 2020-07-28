using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IWorkOrderService
    {
        Task<ICollection<WorkOrder>> GetWorkOrderAsync(GetWorkOrder command);
        Task<WorkOrder> CreateWorkOrderAsync(CreateWorkOrder command);
        Task<WorkOrder> UpdateWorkOrderAsync(UpdateWorkOrder command);
    }
}
