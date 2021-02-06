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
    }
}
