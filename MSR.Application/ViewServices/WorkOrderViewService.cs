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
using MSR.Domain.Helpers;
using Newtonsoft.Json;

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

        public async Task<(ICollection<WorkOrderGridSummary> data, int totalRows)> GetWorkOrderMenuAsync(GetWorkOrderMenuQueryModel filters)
        {
            return await _unitOfWork.Query<WorkOrderMenu>().GetWorkOrderMenu(WorkOrderProjections.WorkOrderMenuView,filters);
        }

        public async Task<(ICollection<PortalWorkOrderView> data, int totalRows)> GetPortalWorkOrderMenuAsync(GetPortalWorkOrderQueryModel portalWorkOrderQueryModel)
        {
            var portalSubParts = new List<PortalSubPart>();
            if (!string.IsNullOrWhiteSpace(portalWorkOrderQueryModel.SubPartName))
            {
                portalSubParts = await _unitOfWork.PortalSubParts.Query().Where(i => i.Name.Contains(portalWorkOrderQueryModel.SubPartName)).ToListAsync();
            }

            var workOrderData = await _unitOfWork.Query<PortalWorkOrderMenu>().GetPortalWorkOrderMenu(WorkOrderProjections.PortalWorkOrderMenuView, portalWorkOrderQueryModel, portalSubParts.Select(i => i.WorkOrderId).ToList());
            var workOrderIds = workOrderData.Data.Select(i => i.WorkOrderId).ToList();

            var messages = await _unitOfWork.WorkOrderMessages.Query().Include(i => i.Created).Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();

            var allPortalSubParts = await _unitOfWork.PortalSubParts.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();

            foreach(var workOrder in workOrderData.Data)
            {
                workOrder.SubParts = allPortalSubParts.Where(i => i.WorkOrderId == workOrder.WorkOrderId).Select(i => AutoMapperHelper.Mapper.Map<PortalSubPartView>(i)).ToList();
                workOrder.Messages = messages.Where(i => i.WorkOrderId == workOrder.WorkOrderId).Select(i => AutoMapperHelper.Mapper.Map<WorkOrderMessageModel>(i)).ToList();
                workOrder.Disposition = string.Join(";",messages.Select(i => $"{i.Created.FullName}, {i.CreatedOn}, {i.Message}").ToList());
            }

            return workOrderData;
        }
    }
}
