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
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MSR.Domain.Abstractions.Services;
using System;
using MSR.Domain.Models.Query;

namespace MSR.Application.ViewServices
{
    public class WorkOrderViewService : IWorkOrderViewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkOrderViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrdersAsync()
        {
            List<int> invoicedWorkOrderIds = await _unitOfWork.InvoiceItems.Query().Select(i => i.WorkOrderId).Distinct().ToListAsync();

            return await _unitOfWork.Query<WorkOrder>().GetInvoiceableWorkOrders(WorkOrderProjections.InvoiceableWorkOrderView, invoicedWorkOrderIds);
        }

        public async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderHistoryAsync() => await _unitOfWork.Query<WorkOrder>().GetWorkOrderHistory(WorkOrderProjections.WorkOrderGridSummaryView);

        public async Task<ICollection<WorkOrderStatus>> GetWorkOrderStatusAsync()
        {
             return await _unitOfWork.Query<WorkOrderStatusSummary>().GetWorkOrderStatus(WorkOrderProjections.WorkOrderStatusView);       
        }

        public async Task<(ICollection<WorkOrderGridSummary> data, int totalRows)> GetWorkOrderMenuAsync(QueryBase filters)
        {
            return await _unitOfWork.Query<WorkOrderMenu>().GetWorkOrderMenu(WorkOrderProjections.WorkOrderMenuView,filters);
        }

        public async Task<(ICollection<PortalWorkOrderView> data, int totalRows)> GetPortalWorkOrderMenuAsync(GetPortalWorkOrderQueryModel portalWorkOrderQueryModel)
        {
            return await _unitOfWork.Query<PortalWorkOrderMenu>().GetPortalWorkOrderMenu(WorkOrderProjections.PortalWorkOrderMenuView, portalWorkOrderQueryModel);
        }
    }
}
