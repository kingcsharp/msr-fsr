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
using System;
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
            workorders = await query
                .Include(x => x.WorkOrderParts)
                .Include(x => x.WorkOrderTasks)
                .Include(x => x.Product)
                .Include(x => x.Purchase)
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
            WorkOrderModel ret = null;

            if (false && !CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanCreate))
            {
                throw new DomainException($"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}");
            }

            if (command.ScheduledStartDate == null || command.ScheduledStartDate.Ticks == 0)
            {
                command.ScheduledStartDate = DateTime.Now;
            }

            WorkOrder workorder = _mapper.Map<WorkOrder>(command);

            var created = _unitOfWork.WorkOrders.Add(workorder);

            await _unitOfWork.LogApprovalTransaction(workorder, workorder.Id);

            // load required navigation fields
            created.Context.Entry(workorder)
                .Collection(x => x.WorkOrderParts).Load();
            created.Context.Entry(workorder)
                .Collection(x => x.WorkOrderTasks).Load();

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
        /// Create the EF objects based on the Procedure, ProcedureStep, and
        /// ProcedureStepMonitor definitions in the database.
        /// This does not do the insert.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<ICollection<WorkOrderTaskModel>> GetWorkOrderTasksAsync(CreateWorkOrder command)
        {
            var product = await _unitOfWork.Products.Query()
                .FirstAsync(x => x.Id == command.ProductId);
            List<ProcedureStep> steps = await _unitOfWork.ProcedureSteps.Query()
                .Where(x => x.ProcedureId == product.ProcedureId)
                .Include(x => x.ProcedureStepMonitors)
                .ToListAsync();

            List<WorkOrderTask> tasks = new List<WorkOrderTask>();
            foreach (var step in steps)
            {
                var wot = _mapper.Map<WorkOrderTask>(step);
                wot.WorkOrderTaskMonitors = step.ProcedureStepMonitors
                    .Select(x => _mapper.Map<WorkOrderTaskMonitor>(x))
                    .ToList();
                tasks.Add(wot);
            }

            return tasks.Select(x =>
                _mapper.Map<WorkOrderTaskModel>(x))
                .ToList();
        }

        public async Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(CreateWorkOrder command)
        {
            var product = await _unitOfWork.Products.Query()
                .FirstAsync(x => x.Id == command.ProductId);
            int count = 1;
            int quantity = command.Qty;

            if (command.SerializeIndividually && command.Qty > 1) {
                count = command.Qty;
                quantity = 1;
            }

            List<WorkOrderPartModel> parts = new List<WorkOrderPartModel>();
            for (int i = 0; i < count; i++) {
                var n = new WorkOrderPartModel() {
                    PartId = product.PartId
                };
                parts.Add(n);
            }

            return parts;
        }

        private WorkOrderModel DetachBackPointers(WorkOrderModel wom)
        {
            var model = _mapper.Map<WorkOrderModel>(wom);
            // unlink the backpointers to the work order model, which causes loops.
            if (model.Purchase != null) {
                model.Purchase.WorkOrders = null;
            }
            if (model.Product != null) {
                model.Product.WorkOrders = null;
            }

            foreach (var wop in model.WorkOrderParts)
            {
                wop.WorkOrder = null;
                wop.Parent = null;
                wop.Children = null;
            }

            foreach (var wot in model.WorkOrderTasks)
            {
                wot.WorkOrder = null;
            }
            return model;
        }
    }
}
