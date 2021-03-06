using MSR.Domain.Models;
using MSR.Domain.Models.Query;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.Abstractions
{
    public interface IWorkOrderViewService
    {
        Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrdersAsync();
        Task<ICollection<WorkOrderGridSummary>> GetWorkOrderHistoryAsync();
        Task<ICollection<WorkOrderStatus>> GetWorkOrderStatusAsync();
        Task<(ICollection<WorkOrderGridSummary> data, int totalRows)> GetWorkOrderMenuAsync(GetWorkOrderMenuQueryModel filters);
        Task<(ICollection<PortalWorkOrderView> data, int totalRows)> GetPortalWorkOrderMenuAsync(GetPortalWorkOrderQueryModel portalWorkOrderQueryModel);
    }
}
