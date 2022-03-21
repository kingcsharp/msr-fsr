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
        private IMapper _mapper;

        public WorkOrderViewService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<WorkOrderStatus>> GetWorkOrderStatusAsync()
        {
             return await _unitOfWork.Query<WorkOrderStatusSummary>().GetWorkOrderStatus(WorkOrderProjections.WorkOrderStatusView);       
        }

        public async Task<(ICollection<WorkOrderGridSummary> data, int totalRows)> GetWorkOrderMenuAsync(GetWorkOrderMenuQueryModel filters)
        {
            var workOrders = await _unitOfWork.Query<WorkOrderMenu>().GetWorkOrderMenu(WorkOrderProjections.WorkOrderMenuView,filters);
            var workOrderIds = workOrders.data.Select(i => i.Id).ToList();
            var subPartsList = await _unitOfWork.SubParts.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();
            foreach (var summary in workOrders.data.Where(i => i.HasSubParts))
            { 
                summary.SubParts = subPartsList.Where(i => i.WorkOrderId == summary.Id).Select(i => _mapper.Map<SubPartModel>(i)).ToList();
            }
            return workOrders;
        }

        public async Task<(ICollection<PortalWorkOrderView> data, int totalRows)> GetPortalWorkOrderMenuAsync(GetPortalWorkOrderQueryModel portalWorkOrderQueryModel)
        {
            var portalSubParts = new List<SubPart>();
            if (!string.IsNullOrWhiteSpace(portalWorkOrderQueryModel.SubPartName))
            {
                portalSubParts = await _unitOfWork.SubParts.Query().Where(i => i.Name.Contains(portalWorkOrderQueryModel.SubPartName)).ToListAsync();
            }

            var workOrderData = await _unitOfWork.Query<PortalWorkOrderMenu>().GetPortalWorkOrderMenu(WorkOrderProjections.PortalWorkOrderMenuView, portalWorkOrderQueryModel, portalSubParts.Select(i => i.WorkOrderId).ToList());
            var workOrderIds = workOrderData.Data.Select(i => i.WorkOrderId).ToList();

            var messages = await _unitOfWork.WorkOrderMessages.Query().Include(i => i.Created).Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();

            var allPortalSubParts = await _unitOfWork.SubParts.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();

            foreach(var workOrder in workOrderData.Data)
            {
                workOrder.SubParts = allPortalSubParts.Where(i => i.WorkOrderId == workOrder.WorkOrderId).Select(i => AutoMapperHelper.Mapper.Map<SubPartModel>(i)).ToList();
                workOrder.Messages = messages.Where(i => i.WorkOrderId == workOrder.WorkOrderId).Select(i => AutoMapperHelper.Mapper.Map<WorkOrderMessageModel>(i)).ToList();
                if(workOrder.Disposition != null)
                {
                    var dispositionItems = workOrder.Disposition.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
                    var dispositionMessages = dispositionItems.Select(i => i.Split(';', StringSplitOptions.RemoveEmptyEntries)).ToList();
                    var itemsToAdd = dispositionMessages.Select(j => new WorkOrderMessageModel() { Date = DateTime.Parse(j[1].ToString()), Message = j[2].ToString(), Name = j[0].ToString() });

                    workOrder.Messages = workOrder.Messages.Concat(itemsToAdd).ToList();
                }
            }

            return workOrderData;
        }
    }
}
