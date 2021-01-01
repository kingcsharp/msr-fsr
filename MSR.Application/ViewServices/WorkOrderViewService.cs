using AutoMapper;
using MSR.Application.Abstractions;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.Projections;
using MSR.Infrastructure.Resources.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Application.ViewServices
{
    public class WorkOrderViewService : IWorkOrderViewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkOrderViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrdersAsync() => await _unitOfWork.Query<WorkOrder>().GetInvoiceableWorkOrders(WorkOrderProjections.InvoiceableWorkOrderView);

        public async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderHistoryAsync() => await _unitOfWork.Query<WorkOrder>().GetWorkOrderHistory(WorkOrderProjections.WorkOrders);
    }
}
