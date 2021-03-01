using MSR.Domain.Models;
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
        Task<(ICollection<WorkOrderGridSummary> data, int totalRows)> GetWorkOrderMenuAsync(int skip = 0, int take = 0);
        Task<(ICollection<PortalWorkOrderView> data, int totalRows)> GetPortalWorkOrderMenuAsync(int customerId, string partName, int? partId, DateTime fromDate, DateTime toDate, int skip = 0, int take = 0);
    }
}
