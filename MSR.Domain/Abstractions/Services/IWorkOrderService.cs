using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
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
        Task<ICollection<WorkOrderTaskModel>> GetWorkOrderTasksAsync(CreateWorkOrder command);
        Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(CreateWorkOrder command);
        Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(GetWorkOrderPart command);
        ICollection<StatusModel> GetActiveStatusList();
        Task<WorkOrderTaskModel> CreateWorkOrderTaskAsync(CreateWorkOrderTask command);
        Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command);
        Task<WorkOrderTaskMonitorModel> UpdateWorkOrderTaskMonitorAsync(UpdateWorkOrderTaskMonitor command);
        Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryAsync(GetWorkOrderMenu command);
        string GetWorkOrderItemNumber(WorkOrderModel model);
        Task<ICollection<PortalWorkOrderView>> GetPortalWorkOrders(GetPortalWorkOrder command);
        Task<WorkOrderMessageModel> CreateWorkOrderMessageAsync(CreateWorkOrderMessage command);
        Task<ICollection<WorkOrderHistoryView>> GetWorkOrderHistoryView(GetWorkOrderHistory command);
        Task<int> GetTotalWorkOrderHistoryViewRows(GetWorkOrderHistory command);
        Task<ICollection<WorkOrderTaskModel>> TakeOverWorkOrderTasks(TakeOverWorkOrder takeOverWorkOrderTasks);
        Task<ICollection<WorkOrderTaskModel>> CancelWorkOrderTasksAsync(CancelWorkOrder command);
        Task<ICollection<WorkOrderTaskModel>> AddNCRWorkOrderTasksAsync(AddNCRWorkOrderTask command);
        Task<ICollection<WorkOrderModel>> GetWorkOrderById(int id);
        Task<ICollection<WorkOrderSelectItem>> GetWorkOrderSelectItems(GetAssignedWorkOrders command);
    }
}
