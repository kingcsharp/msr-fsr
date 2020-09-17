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
        Task<WorkOrderPartModel> UpdateWorkOrderPartAsync(UpdateWorkOrderPart command);
        Task<bool> DeleteWorkOrderAsync(DeleteWorkOrder command);
        ICollection<StatusModel> GetActiveStatusList();
        Task<WorkOrderTaskModel> CreateWorkOrderTaskAsync(CreateWorkOrderTask command);
        Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command);
        Task<WorkOrderTaskMonitorModel> UpdateWorkOrderTaskMonitorAsync(UpdateWorkOrderTaskMonitor command);
    }
}
