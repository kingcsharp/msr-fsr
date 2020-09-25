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
        Task<ICollection<WorkOrderTaskModel>> GetWorkOrderTasksAsync(CreateWorkOrder command);
        Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(CreateWorkOrder command);
        ICollection<StatusModel> GetActiveStatusList();
        Task<WorkOrderTaskModel> CreateWorkOrderTaskAsync(CreateWorkOrderTask command);
        Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command);
        Task<WorkOrderTaskMonitorModel> UpdateWorkOrderTaskMonitorAsync(UpdateWorkOrderTaskMonitor command);
        Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryAsync(GetWorkOrderHistory command);
        Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryAsync(GetWorkOrderMenu command);
        Task<ICollection<WorkOrderStatus>> GetWorkOrderStatusAsync(GetWorkOrderStatus command);

        public static string GetWorkOrderItemNumber(WorkOrderModel model)
        {
            string customerName = model.Purchase?.PurchaseOrder?.Customer?.Name;
            if (string.IsNullOrEmpty(customerName))
            {
                customerName = "";
            }
            string customerPNum = model.Purchase?.CustomerPurchaseNumber;
            if (string.IsNullOrEmpty(customerPNum))
            {
                customerPNum = "";
            }
            return $"{customerName}-{customerPNum}";
        }
    }
}
