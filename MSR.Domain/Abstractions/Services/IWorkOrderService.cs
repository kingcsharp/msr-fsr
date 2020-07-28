using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IWorkOrderService
    {
        Task<ICollection<WorkOrderModel>> GetWorkOrderAsync(GetWorkOrder command);
        Task<WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command);
        Task<WorkOrderModel> UpdateWorkOrderAsync(UpdateWorkOrder command);
    }
}
