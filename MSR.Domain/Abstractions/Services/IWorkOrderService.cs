using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.DTOs;
using System.Xml.Linq;
using MSR.Domain.Models.Config;

namespace MSR.Domain.Abstractions.Services
{
    public interface IWorkOrderService
    {
        Task<string> CreateWorkOrderAsync(CreateWorkOrderDTO createWorkOrderDto);
        Task<WorkOrderModel> UpdateWorkOrderAsync(UpdateWorkOrder command);
        Task<WorkOrderPartModel> UpdateWorkOrderPartAsync(UpdateWorkOrderPart command);
        Task<bool> DeleteWorkOrderAsync(DeleteWorkOrder command);
        Task<ICollection<WorkOrderTaskModel>> GetWorkOrderTasksAsync(int ProductId);
        Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(CreateWorkOrder command);
        Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(GetWorkOrderPart command);
        ICollection<StatusModel> GetActiveStatusList();
        Task<WorkOrderTaskModel> CreateWorkOrderTaskAsync(CreateWorkOrderTask command);
        Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command);
        Task<WorkOrderTaskMonitorModel> UpdateWorkOrderTaskMonitorAsync(UpdateWorkOrderTaskMonitor command);
        Task<WorkOrderMessageModel> CreateWorkOrderMessageAsync(CreateWorkOrderMessage command);
        Task<ICollection<WorkOrderHistoryView>> GetWorkOrderHistoryView(GetWorkOrderHistory command);
        Task<int> GetTotalWorkOrderHistoryViewRows(GetWorkOrderHistory command);
        Task<ICollection<WorkOrderTaskModel>> TakeOverWorkOrderTasks(TakeOverWorkOrder takeOverWorkOrderTasks);
        Task<ICollection<WorkOrderTaskModel>> CancelWorkOrderTasksAsync(CancelWorkOrder command);
        Task<ICollection<WorkOrderTaskModel>> AddNCRWorkOrderTasksAsync(AddNCRWorkOrderTask command);
        Task<ICollection<WorkOrderModel>> GetWorkOrderById(int id);
        Task<ICollection<WorkOrderSelectItem>> GetWorkOrderSelectItems(GetAssignedWorkOrders command);
        Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrdersView();
        Task<ICollection<SubPartModel>> GetWorkOrderSubParts(List<int> workOrderIds);
        Task<WorkOrderModel> UpdateWorkOrderPriceAsync(UpdateWorkOrderPrice command);
        Task<int> GetProductIdFromPurchaseOrderProduct(int purchaseOrderProductId);
        Task<WorkOrderModel> UpdateWorkOrderEndDateAsync(UpdateWorkOrderEndDate command);
        Task<bool> BulkUpdateWorkOrderPart(BulkUpdateWorkOrderPart command);
        Task<(bool Success, string DisplayString)> UpdateWorkOrderPartCycleCount(UpdateWorkOrderPartCycleCount command);
        Task<XmlTransmissionLogModel> GenerateAndTransmitXmlFiles(TransmitIntelXmlDataByWorkOrder command);
        Task<XmlTransmissionLogModel> RetransmitXmlFile(TransmitXmlFile command);
        IntelXmlData GetIntelXmlData(int workOrderId);
        Task<XmlTransmissionLogModel> TransferFtpTransmission(int workOrderId, string XmlFileContent, string XmlLink);
    }
        
}
