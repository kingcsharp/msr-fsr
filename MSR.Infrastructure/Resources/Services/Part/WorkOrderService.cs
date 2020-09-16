using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.WorkOrderModel>> GetWorkOrderAsync(GetWorkOrder command)
        {
            List<EntityFramework.Entities.WorkOrder> workorders;
            IQueryable<WorkOrder> query = _unitOfWork.WorkOrders.Query();

            if (command.Id.HasValue) {
                query = query.Where(x => x.Id == command.Id.Value);
            }
            List<int> woIds;
            if (command.statuses != null && command.statuses.Count > 0) {
                List<int> statusIds = command.statuses.Select(x => x.Id).ToList();
                woIds = _unitOfWork.WorkOrderTasks.Query()
                    .Where(x => statusIds.Contains(x.StatusId))
                    .Select(x => x.WorkOrderId)
                    .Distinct()
                    .ToList();
                // if the invertStatusSet flag is set, then include
                // the elements NOT in the set of status ids.
                if (command.invertStatusSet)
                {
                    query = query.Where(x => !woIds.Contains(x.Id));
                }
                else
                {
                    query = query.Where(x => woIds.Contains(x.Id));
                }
            }

            workorders = await query
                .Include(x => x.WorkOrderParts)
                    .ThenInclude(y => y.Part)
                .Include(x => x.WorkOrderTasks)
                    .ThenInclude(y => y.ProcedureStep)
                .Include(x => x.WorkOrderTasks)
                    .ThenInclude(y => y.ProcedureStepType)
                .Include(x => x.Product)
                    .ThenInclude(y => y.Part)
                .Include(x => x.Product)
                    .ThenInclude(y => y.Customer)
                .Include(x => x.Product)
                    .ThenInclude(y => y.Procedure)
                .Include(x => x.Purchase)
                    .ThenInclude(y => y.PurchaseOrder)
                .Include(x => x.Location)
                .ToListAsync();

            if (workorders.Count == 0 && command.Id.HasValue) {
                throw new DomainException($"Work Order ID {command.Id.GetValueOrDefault()} not found", DomainError.NotFound);
            }

            var result = workorders.Select(x => {
                var wom = _mapper.Map<Domain.Models.WorkOrderModel>(x);
                return DetachBackPointers(wom);
            }).OrderBy(x => x.Id).ToList();

            return result;
        }
        public async Task<Domain.Models.WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command)
        {
            WorkOrderModel ret;

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove))
            {
                throw new DomainException($"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}");
            }

            WorkOrder workorder = _mapper.Map<WorkOrder>(command);

            _unitOfWork.WorkOrders.Add(workorder);

            await _unitOfWork.LogApprovalTransaction(workorder, workorder.Id);

            ret = DetachBackPointers(
                _mapper.Map<Domain.Models.WorkOrderModel>(workorder)
            );

            return ret;
        }
        public async Task<Domain.Models.WorkOrderModel> UpdateWorkOrderAsync(UpdateWorkOrder command)
        {
            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove))
            {
                throw new DomainException($"Permission denied for {nameof(Domain.Models.WorkOrderModel)} uid {CurrentUser.GetId()}");
            }

            WorkOrderModel ret;
            var workorder = _mapper.Map(command, current);
            _unitOfWork.WorkOrders.Update(workorder);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workorder, workorder.Id);

            ret = _mapper.Map<Domain.Models.WorkOrderModel>(workorder);

            return ret;

        }
        public async Task<bool> DeleteWorkOrderAsync(DeleteWorkOrder command)
        {
            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove)) {
                throw new DomainException($"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}");
            }

            _unitOfWork.WorkOrders.Delete(false, current.Id);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(current, current.Id);

            return true;
        }

        /// <summary>
        /// Return a list of statuses for active work order tasks
        /// (and by extension, work order).
        /// </summary>
        /// <description>
        /// Returns statuses that are equal to those to be displayed in
        /// the Active Work Order table.  That is those that are NOT
        /// "complete", "cancelled", "rejected", or "closed" tasks.
        /// <description>
        /// <returns>The list of "active" statuses</returns>
        public ICollection<StatusModel> GetActiveStatusList()
        {
            var query = _unitOfWork.Status.Query();
            List<Status> rows = query.Where(x => !(
                x.Name.ToUpper().Equals("complete") ||
                x.Name.ToUpper().Equals("cancelled") ||
                x.Name.ToUpper().Equals("rejected") ||
                x.Name.ToUpper().Equals("closed"))
            ).ToList();
            return rows.Select(x => _mapper.Map<StatusModel>(x)).ToList();
        }

        private WorkOrderModel DetachBackPointers(WorkOrderModel wom)
        {
            var model = _mapper.Map<WorkOrderModel>(wom);
            // unlink the backpointers to the work order model,
            // which causes loops.
            if (model.Purchase != null) {
                model.Purchase.WorkOrders = null;
            }
            if (model.Product != null) {
                model.Product.WorkOrders = null;
            }
            foreach (var wop in model.WorkOrderParts) {
                wop.WorkOrder = null;
                wop.Parent = null;
                wop.Children = null;
            }
            foreach (var wot in model.WorkOrderTasks) {
                wot.WorkOrder = null;
            }
            return model;
        }
    }
}
