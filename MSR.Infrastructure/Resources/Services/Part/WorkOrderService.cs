using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Helpers.Abstractions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class WorkOrderService : IWorkOrderService
    {
        public const int PROCEDURE_STEP_TYPE_NC = 3;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStatusHelper _statusHelper;

        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper, IStatusHelper statusHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _statusHelper = statusHelper;
        }

        public async Task<ICollection<Domain.Models.WorkOrderModel>> GetWorkOrderAsync(GetWorkOrder command)
        {
            List<EntityFramework.Entities.WorkOrder> workorders;
            IQueryable<WorkOrder> query = _unitOfWork.WorkOrders.Query();

            if (command.Id.HasValue)
            {
                query = query.Where(x => x.Id == command.Id.Value);
            }
            List<int> woIds;
            if (command.statuses != null && command.statuses.Count > 0)
            {
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

            if (command.completedOnly.HasValue)
            {
                if (command.completedOnly.Value)
                {
                    query = query.Where(x => x.ActualEndDate.HasValue);
                }
                else
                {
                    query = query.Where(x => !x.ActualEndDate.HasValue);
                }
            }

            if (command.assignedToId.HasValue) {
                query = query.Where(x =>
                    x.WorkOrderTasks.Any(y =>
                        y.AssignedTo == command.assignedToId));
            }

            workorders = await query
                .Include(x => x.WorkOrderParts)
                .ThenInclude(y => y.Part)

                // Work Order Tasks etc.
                .Include(x => x.WorkOrderTasks)
                .ThenInclude(y => y.ProcedureStep)
                .ThenInclude(y => y.Procedure)
                .Include(x => x.WorkOrderTasks)
                .ThenInclude(y => y.ProcedureStep)
                .ThenInclude(y => y.ProcedureStepRoles)
                .ThenInclude(y => y.Role)
                .Include(x => x.WorkOrderTasks)
                .ThenInclude(x => x.WorkOrderTaskMonitors)
                .ThenInclude(x => x.ProcedureStepMonitor)
                .Include(x => x.WorkOrderTasks)
                .ThenInclude(y => y.ProcedureStepType)
                .Include(x => x.WorkOrderTasks)
                .ThenInclude(y => y.Status)

                // Product etc.
                .Include(x => x.Product)
                .ThenInclude(y => y.Part)
                .Include(x => x.Product)
                .ThenInclude(y => y.Customer)
                .Include(x => x.Product)
                .ThenInclude(y => y.Procedure)

                .Include(x => x.Purchase)
                .ThenInclude(y => y.PurchaseOrder)
                .ThenInclude(y => y.Customer)
                .Include(x => x.Location)
                .ToListAsync();

            if (workorders.Count == 0 && command.Id.HasValue)
            {
                throw new DomainException($"Work Order ID {command.Id.GetValueOrDefault()} not found", DomainError.NotFound);
            }

            var result = new List<WorkOrderModel>();

            foreach (var wo in workorders)
            {
                var wom = _mapper.Map<WorkOrderModel>(wo);

                // Status ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
                wom.Status = TranslateWOStatusToViewModel(wom.WorkOrderTasks);

                foreach (var wot in wom.WorkOrderTasks) {

                    // enforce sane data by limiting the status IDs returned by the API
                    wot.StatusId = TranslateWOTaskStatusToViewModel(wot);

                    var wotmList = new List<WorkOrderTaskMonitorModel>();
                    int i = 1; // Monitor Number starts at 1
                    foreach (var wotm in wot.WorkOrderTaskMonitors.OrderBy(x => x.Id)) {
                        wotm.MonitorNumber = i;
                        wotm.WorkOrderTask = null; // avoid loops
                        wotmList.Add(wotm);
                        i += 1;
                    }
                    wot.WorkOrderTaskMonitors = wotmList;
                }
                result.Add(DetachBackPointers(wom));
            };

            return result.OrderBy(x => x.Id).ToList();
        }
        public async Task<Domain.Models.WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command)
        {
            WorkOrderModel ret = null;

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanCreate))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
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
            created.Context.Entry(workorder)
                .Reference(x => x.Purchase).Load();
            created.Context.Entry(workorder.Purchase)
                .Reference(x => x.PurchaseOrder).Load();
            created.Context.Entry(workorder.Purchase.PurchaseOrder)
                .Reference(x => x.Customer).Load();

            ret = DetachBackPointers(
                _mapper.Map<Domain.Models.WorkOrderModel>(workorder)
            );

            return ret;
        }

        public async Task<Domain.Models.WorkOrderModel> UpdateWorkOrderAsync(UpdateWorkOrder command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(Domain.Models.WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
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

            if (current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
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
                .OrderBy(x => x.PrintOrder)
                .ToListAsync();

            List<WorkOrderTask> tasks = new List<WorkOrderTask>();
            int taskStepOrder = 10;
            foreach (var step in steps)
            {
                var wot = _mapper.Map<WorkOrderTask>(step);
                wot.WorkOrderTaskMonitors = step.ProcedureStepMonitors
                    .Select(x => _mapper.Map<WorkOrderTaskMonitor>(x))
                    .ToList();
                wot.TaskStepOrder = taskStepOrder;
                tasks.Add(wot);
                taskStepOrder += 10;
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

        public async Task<WorkOrderPartModel> UpdateWorkOrderPartAsync(UpdateWorkOrderPart command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderPart)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = _unitOfWork.WorkOrderParts.Query().Where(x => x.Id == command.WorkOrderPartId);

            if (current == null || !current.Any())
            {
                throw new DomainException($"{nameof(WorkOrderPart)} not found with ID: {command.WorkOrderPartId}", DomainError.NotFound);
            }

            WorkOrderPart updatedWOPart = current.First();
            _ = _mapper.Map(command, updatedWOPart);

            _unitOfWork.WorkOrderParts.Update(updatedWOPart);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(updatedWOPart, updatedWOPart.Id);

            return _mapper.Map<WorkOrderPartModel>(updatedWOPart);
        }

        public async Task<WorkOrderTaskModel> CreateWorkOrderTaskAsync(CreateWorkOrderTask command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderTask)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            WorkOrderTask newTask = _mapper.Map<WorkOrderTask>(command);

            var created = _unitOfWork.WorkOrderTasks.Add(newTask);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(newTask, newTask.Id);

            created.Context.Entry(newTask)
                .Reference(x => x.ProcedureStep).Load();

            return _mapper.Map<WorkOrderTaskModel>(newTask);
        }

        public async Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderTask)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = await _unitOfWork.WorkOrderTasks.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrderTask)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (!String.IsNullOrEmpty(command.Status))
            {
                var statusObj = await _unitOfWork
                    .Status
                    .Query()
                    .FirstOrDefaultAsync(x =>
                        x.Name.ToUpper().Equals(command.Status.ToUpper()));
                if (statusObj == null)
                {
                    throw new DomainException($"{nameof(Status)} not found with Name: {command.Status}", DomainError.NotFound);
                }
                current.StatusId = statusObj.Id;
            }

            WorkOrderTaskModel ret;
            WorkOrderTask workordertask = _mapper.Map(command, current);
            _unitOfWork.WorkOrderTasks.Update(workordertask);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workordertask, workordertask.Id);

            ret = _mapper.Map<Domain.Models.WorkOrderTaskModel>(workordertask);

            // Update the work order datetimes, if needed
            _unitOfWork.WorkOrderTasks.LoadReference(workordertask, x => x.Status);
            _unitOfWork.WorkOrderTasks.LoadReference(workordertask, x => x.WorkOrder);
            WorkOrder wo = workordertask.WorkOrder;
            _unitOfWork.WorkOrders.LoadCollection(wo, "WorkOrderTasks");
            // "DONE" states are:
            // 4   Cancelled
            // 8   Closed
            // 3   Complete
            // 6   Rejected
            int[] completed = _statusHelper.GetCompletedIds();
            if (workordertask.Status.Name.ToUpper().Equals("IN PROGRESS"))
            {
                if (!workordertask.WorkOrder.ActualStartDate.HasValue)
                {
                    wo.ActualStartDate = DateTime.Now;
                    _unitOfWork.WorkOrders.Update(wo);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else if (wo.WorkOrderTasks.All(x => completed.Contains(x.StatusId)))
            {
                if (!workordertask.WorkOrder.ActualEndDate.HasValue)
                {
                    wo.ActualEndDate = DateTime.Now;
                    _unitOfWork.WorkOrders.Update(wo);
                    await _unitOfWork.SaveChangesAsync();
                }
            }




            return ret;
        }

        public async Task<WorkOrderTaskMonitorModel> UpdateWorkOrderTaskMonitorAsync(UpdateWorkOrderTaskMonitor command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderTask)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = await _unitOfWork.WorkOrderTaskMonitors.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrderTaskMonitor)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            WorkOrderTaskMonitorModel ret;
            var workordertaskmonitor = _mapper.Map(command, current);
            _unitOfWork.WorkOrderTaskMonitors.Update(workordertaskmonitor);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workordertaskmonitor, workordertaskmonitor.Id);

            ret = _mapper.Map<Domain.Models.WorkOrderTaskMonitorModel>(workordertaskmonitor);

            return ret;
        }

        public async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryAsync(GetWorkOrderHistory command)
        {
            return await GetWorkOrderGridSummaryImpl(true);
        }

        public async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryAsync(GetWorkOrderMenu command)
        {
            return await GetWorkOrderGridSummaryImpl(false);
        }

        private async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryImpl(bool isHistory)
        {
            var gwo = new GetWorkOrder() {
                completedOnly = isHistory
            };

            ICollection<WorkOrderModel> models = await GetWorkOrderAsync(gwo);
            List<WorkOrderGridSummary> ret = new List<WorkOrderGridSummary>();
            foreach (WorkOrderModel m in models)
            {
                var sum = _mapper.Map<WorkOrderGridSummary>(m);

                //
                // Map the work order database entity to the WIP grid view
                //

                // CurrentActiveTaskName
                var curProc =
                    m.WorkOrderTasks.Where(x => x.Status.Name.ToUpper().Equals("IN PROGRESS"));
                if (curProc.Any())
                {
                    var proc = curProc.First();
                    sum.CurrentActiveTaskName = proc.ProcedureStep.Title;
                }

                // CustomerName
                sum.CustomerName = m.Purchase?.PurchaseOrder?.Customer?.Name;
                if (string.IsNullOrEmpty(sum.CustomerName))
                {
                    sum.CustomerName = "";
                }

                // WorkOrderItemNumber
                sum.WorkOrderItemNumber = IWorkOrderService.GetWorkOrderItemNumber(m);

                // ProcedureName
                var firstProc =
                    m.WorkOrderTasks.FirstOrDefault();
                if (firstProc == null)
                {
                    sum.ProcedureName = "";
                }
                else
                {
                    sum.ProcedureName = firstProc.ProcedureStep?.Procedure?.Name;
                }

                // Disposition
                // This is a string join of the text values of
                // all procedure steps with a type of "NC Disposition"
                sum.Disposition = "";
                if (m.HasNCR.GetValueOrDefault())
                {
                    var nc = m.WorkOrderTasks.Where(x =>
                        x.ProcedureStepTypeId == PROCEDURE_STEP_TYPE_NC);
                    if (nc.Any())
                    {
                        string disp = "";
                        foreach (WorkOrderTaskModel task in nc)
                        {
                            disp +=
                                String.Join(" ",
                                    task.WorkOrderTaskMonitors.Select(x =>
                                        x.TextVal
                                    ).ToList()
                                ) + " ";
                        }
                        sum.Disposition = disp;
                    }
                }

                // PercentageOfTasksCompleted
                int[] pctCompletedIds = { 3, 4, 6, 8 };
                int denom = m.WorkOrderTasks.Count();
                int numer = m.WorkOrderTasks.Where(x => pctCompletedIds.Contains(x.StatusId)).Count();
                decimal pctComplete;
                if (denom > 0)
                {
                    pctComplete = numer / denom;
                }
                else
                {
                    pctComplete = 0m;
                }
                sum.PercentageOfTasksCompleted = pctComplete;

                // PercentageOfExpectedDurationTimeLogged
                decimal denomTime = m.WorkOrderTasks.Select(x => x.ProcedureStep.LaborTime).Sum().GetValueOrDefault();
                decimal numerTime = m.WorkOrderTasks.Select(x => x.TotalTaskTime).Sum().GetValueOrDefault();
                if (denomTime > 0)
                {
                    sum.PercentageOfExpectedDurationTimeLogged = numerTime / denomTime;
                }
                else
                {
                    sum.PercentageOfExpectedDurationTimeLogged = 0;
                }

                ret.Add(sum);
            }

            return ret;
        }

        public async Task<ICollection<WorkOrderStatus>> GetWorkOrderStatusAsync(GetWorkOrderStatus command)
        {
            var gwo = _mapper.Map<GetWorkOrder>(command);

            gwo.completedOnly = false;

            ICollection<WorkOrderModel> models = await GetWorkOrderAsync(gwo);
            List<WorkOrderStatus> ret = new List<WorkOrderStatus>();
            foreach (WorkOrderModel m in models)
            {
                var sum = _mapper.Map<WorkOrderStatus>(m);

                // PartNumber
                // Lists the first part in the set (this follows
                // the behavior of Answer 2).
                if (m.WorkOrderParts != null && m.WorkOrderParts.Count > 0)
                {
                    sum.PartNumber = m.WorkOrderParts.First().Part.PartNumber;
                }
                else
                {
                    sum.PartNumber = "";
                }

                // ProcedureName
                if (m.WorkOrderTasks != null && m.WorkOrderTasks.Count > 0)
                {
                    sum.ProcedureName = m.WorkOrderTasks.First().ProcedureStep?.Procedure?.Name;
                }
                else
                {
                    sum.ProcedureName = "";
                }

                WorkOrderSummary wosum = new WorkOrderSummary();

                // WorkOrderId
                wosum.WorkOrderId = m.Id.GetValueOrDefault();

                // WorkOrderItemNumber
                wosum.WorkOrderItemNumber = IWorkOrderService.GetWorkOrderItemNumber(m);

                // PurchaseOrderLineNumber
                wosum.PurchaseOrderLineNumber = m.Purchase.CustomerLineNumber.ToString();

                // WorkOrderPartSerialNumber
                if (m.WorkOrderParts != null && m.WorkOrderParts.Count > 0)
                {
                    wosum.WorkOrderPartSerialNumber = m.WorkOrderParts.First().SerialNumber;
                }
                else
                {
                    wosum.WorkOrderPartSerialNumber = "";
                }

                // WorkOrderStatus ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
                wosum.WorkOrderStatus = m.Status;

                // WorkOrderAssignedTo
                var curstep = m.WorkOrderTasks.Where(x => x.Status.Name.ToUpper().Equals("IN PROGRESS"));
                if (curstep.Count() > 0)
                {
                    if (curstep.First().AssignedToUser == null)
                    {
                        wosum.WorkOrderAssignedTo = "";
                    }
                    else
                    {
                        wosum.WorkOrderAssignedTo = curstep.First().AssignedToUser.FullName;
                    }
                }
                else if (m.WorkOrderTasks != null && m.WorkOrderTasks.Count > 0)
                {
                    if (m.WorkOrderTasks.First().AssignedToUser == null)
                    {
                        wosum.WorkOrderAssignedTo = "";
                    }
                    else
                    {
                        wosum.WorkOrderAssignedTo = m.WorkOrderTasks.First().AssignedToUser.FullName;
                    }
                }
                else
                {
                    wosum.WorkOrderAssignedTo = "";
                }

                // WorkOrderHasNcr
                wosum.WorkOrderHasNcr = m.HasNCR.GetValueOrDefault();

                // WorkOrderScheduledEndDate
                wosum.WorkOrderScheduledEndDate = m.ScheduledEndDate;

                sum.WorkOrderSummary = wosum;

                ret.Add(sum);
            }
            return ret;
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
                x.Name.ToUpper().Equals("closed"))).ToList();
            return rows.Select(x => _mapper.Map<StatusModel>(x)).ToList();
        }

        private string TranslateWOStatusToViewModel(ICollection<WorkOrderTaskModel> tasks)
        {
            // Status ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
            // This field is calculated based on the summation of the statuses
            // of the steps.
            // 1   Approved
            // 2   In Progress
            // 3   Complete
            // 4   Cancelled
            // 5   Pending
            // 6   Rejected
            // 7   Open
            // 8   Closed
            // 9   Requested
            // 10  Assigned
            // 11  Waiting to Start
            int[] completed = _statusHelper.GetCompletedIds();
            string status;
            if (tasks.Where(x => x.StatusId ==
                    _statusHelper.GetInProgress().Id)
                .Any())
            {
                status = _statusHelper.GetInProgress().Name;
            }
            else if (tasks.Where(x => x.StatusId ==
                    _statusHelper.GetCancelled().Id)
                .Any())
            {
                status = _statusHelper.GetCancelled().Name;
            }
            else if (tasks.All(x => completed.Contains(x.StatusId)))
            {
                status = _statusHelper.GetCompleted().Name;
            }
            else
            {
                status = _statusHelper.GetWaitingToStart().Name;
            }
            return status;
        }

        public int TranslateWOTaskStatusToViewModel(WorkOrderTaskModel task)
        {
            // Status ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
            int[] completed = _statusHelper.GetCompletedIds();
            int status;
            if (task.StatusId ==
                    _statusHelper.GetInProgress().Id)
            {
                status = _statusHelper.GetInProgress().Id;
            }
            else if (task.StatusId ==
                    _statusHelper.GetCancelled().Id)
            {
                status = _statusHelper.GetCancelled().Id;
            }
            else if (completed.Contains(task.StatusId))
            {
                status = _statusHelper.GetCompleted().Id;
            }
            else
            {
                status = _statusHelper.GetWaitingToStart().Id;
            }
            return status;
        }

        private WorkOrderModel DetachBackPointers(WorkOrderModel wom)
        {
            var model = _mapper.Map<WorkOrderModel>(wom);
            // unlink the backpointers to the work order model,
            // which causes loops.
            if (model.Purchase != null)
            {
                model.Purchase.WorkOrders = null;
            }
            if (model.Product != null)
            {
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
