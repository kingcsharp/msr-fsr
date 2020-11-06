using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
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
        private readonly IFileService _fileService;

        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ICollection<WorkOrderModel>> GetWorkOrderAsync(GetWorkOrder command)
        {

            IQueryable<WorkOrder> query = _unitOfWork.WorkOrders.Query();

            if (command.Id.HasValue && command.Id > 0)
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

            if (command.assignedToId.HasValue)
            {
                query = query.Where(x =>
                    x.WorkOrderTasks.Any(y =>
                        y.AssignedTo == command.assignedToId));
            }

            if (command.LocationId.HasValue && command.LocationId > 0)
            {
                query = query.Where(x => x.LocationId == command.LocationId);
            }

            List<WorkOrder> workOrderEntities = await query.Include(s => s.WorkOrderParts).ToListAsync();
            List<int> workOrderIds = workOrderEntities.Select(m => m.Id).ToList();
            _ = await _unitOfWork.WorkOrderParts.Query().Include(m => m.Part).Where(s => workOrderIds.Contains(s.WorkOrderId)).ToListAsync();
            List<WorkOrderTask> workOrderTaskEntities = await _unitOfWork.WorkOrderTasks.Query().Include(u => u.ReferenceFiles).Where(s => workOrderIds.Contains(s.WorkOrderId)).ToListAsync();
            List<ProcedureStep> procedureStepEntities = await _unitOfWork.ProcedureSteps.Query()
                .Include(y => y.ProcedureStepRoles)
                .ThenInclude(y => y.Role)
                .Where(s => workOrderTaskEntities.Select(m => m.ProcedureStepId).Contains(s.Id)).ToListAsync();

            _ = await _unitOfWork.Procedures.Query().Where(s => procedureStepEntities.Select(m => m.ProcedureId).Contains(s.Id)).ToListAsync();
            _ = await _unitOfWork.WorkOrderTaskMonitors.Query().Include(m => m.ProcedureStepMonitor)
                .Where(s => workOrderTaskEntities.Select(m => m.Id).Contains(s.WorkOrderTaskId)).ToListAsync();
            _ = await _unitOfWork.ProcedureStepTypes.Query().ToListAsync();
            _ = await _unitOfWork.Status.Query().ToListAsync();

            _ = await _unitOfWork.Products.Query()
                .Include(u => u.Part)
                .Include(t => t.Customer)
                .Include(v => v.Procedure)
                .Where(s => workOrderEntities.Select(m => m.ProductId).Contains(s.Id)).ToListAsync();

            _ = await _unitOfWork.Purchases.Query()
                .Include(s => s.PurchaseOrder)
                .ThenInclude(u => u.Customer)
                .Include(m => m.Location)
                .Where(s => workOrderEntities.Select(m => m.PurchaseId).Contains(s.Id))
                .ToListAsync();


            if (workOrderEntities.Count == 0 && command.Id.HasValue)
            {
                throw new DomainException($"Work Order ID {command.Id.GetValueOrDefault()} not found", DomainError.NotFound);
            }

            if (command.CustomerId.HasValue)
            {
                workOrderEntities = workOrderEntities.Where(i => i.Purchase.PurchaseOrder.CustomerId == command.CustomerId.Value).ToList();
            }

            var result = new List<WorkOrderModel>();

            _ = await _unitOfWork.MonitorTypes.Query().ToListAsync();
            _ = await _unitOfWork.MonitorInputTypes.Query().ToListAsync();

            foreach (var workOrderEntity in workOrderEntities)
            {

                var workOrderModel = _mapper.Map<WorkOrderModel>(workOrderEntity);

                // Status ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
                workOrderModel.Status = TranslateWOStatusToViewModel(workOrderModel.WorkOrderTasks);

                foreach (var workOrderTaskModel in workOrderModel.WorkOrderTasks)
                {
                    // enforce sane data by limiting the status IDs returned by the API
                    workOrderTaskModel.StatusId = TranslateWOTaskStatusToViewModel(workOrderTaskModel);

                    var workOrderTaskMonitorModels = new List<WorkOrderTaskMonitorModel>();
                    int i = 1; // Monitor Number starts at 1
                    foreach (var workOrderTaskMonitorModel in workOrderTaskModel.WorkOrderTaskMonitors.OrderBy(x => x.Id))
                    {
                        workOrderTaskMonitorModel.MonitorNumber = i;
                        workOrderTaskMonitorModel.WorkOrderTask = null; // avoid loops
                        workOrderTaskMonitorModels.Add(workOrderTaskMonitorModel);
                        i += 1;
                    }
                    workOrderTaskModel.WorkOrderTaskMonitors = workOrderTaskMonitorModels;
                    if (workOrderTaskModel.ReferenceFiles.Any())
                    {
                        workOrderTaskModel.ReferenceFiles = _fileService.ListFiles(nameof(WorkOrderTask), workOrderTaskModel.Id).ToList();
                    }

                }
                result.Add(DetachBackPointers(workOrderModel));
            };

            return result.OrderBy(x => x.Id).ToList();
        }
        public async Task<WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command)
        {


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

            // link work order IDs for all parts
            Stack<WorkOrderPart> parts = new Stack<WorkOrderPart>();
            foreach (var p in workorder.WorkOrderParts)
            {
                parts.Push(p);
            }
            while (parts.Count > 0)
            {
                var p = parts.Pop();
                p.WorkOrder = workorder;
                if (p.Children != null && p.Children.Count > 0)
                {
                    foreach (var sp in p.Children)
                    {
                        parts.Push(sp);
                    }
                }
            }

            var purchase = await _unitOfWork.Purchases.Query().FirstOrDefaultAsync(s => s.Id == command.PurchaseId);
            var parentPart = workorder.WorkOrderParts.FirstOrDefault(s => s.ParentId == null);

            await GetCycleCount(parentPart, purchase.SerialNumber);


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

            WorkOrderModel workOrderModel = DetachBackPointers(
                _mapper.Map<WorkOrderModel>(workorder)
            );

            return workOrderModel;
        }
        public async Task<WorkOrderModel> UpdateWorkOrderAsync(UpdateWorkOrder command)
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

            ret = _mapper.Map<WorkOrderModel>(workorder);

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

                // This should probably not be a nullable field in the
                // database.  Carrying through the null causes problems
                // down the line, so for now, the code will default to 1.
                if (!step.ProcedureStepTypeId.HasValue)
                {
                    wot.ProcedureStepTypeId = 1;
                }

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
            var product = await _unitOfWork.Products
                .Query()
                .Include(x => x.Part)
                .ThenInclude(y => y.Subparts)
                .FirstAsync(x => x.Id == command.ProductId);
            int count = 1;
            int quantity = command.Qty;

            if (command.SerializeIndividually && command.Qty > 1)
            {
                count = command.Qty;
                quantity = 1;
            }

            List<WorkOrderPartModel> parts = new List<WorkOrderPartModel>();
            for (int i = 0; i < count; i++)
            {
                List<WorkOrderPartModel> subs = new List<WorkOrderPartModel>();
                if (product.Part.Subparts != null && product.Part.Subparts.Count > 0)
                {
                    foreach (PartSubPartMap p in product.Part.Subparts)
                    {
                        int spquantity = p.Qty;
                        if (spquantity == 0)
                        {
                            spquantity = 1;
                        }

                        for (int j = 0; j < spquantity; j++)
                        {
                            subs.Add(new WorkOrderPartModel()
                            {
                                PartId = p.PartId,
                                ParentId = p.ParentPartId
                            });
                        }
                    }
                }
                var n = new WorkOrderPartModel()
                {
                    PartId = product.PartId,
                    Children = subs
                };
                parts.Add(n);
            }

            return parts;
        }

        public async Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(GetWorkOrderPart command)
        {
            var query = _unitOfWork.WorkOrderParts.Query();

            if (command.Id.HasValue)
            {
                query = query.Where(s => s.Id == command.Id);

            }

            if (command.WorkOrderId.HasValue)
            {
                query = query.Where(s => s.WorkOrderId == command.WorkOrderId);
            }

            var workOrderParts = await query.ToListAsync();
            _ = await _unitOfWork.Parts.Query().Where(s => workOrderParts.Select(m => m.PartId).ToList().Contains(s.Id)).ToListAsync();
            var workOrderPartModels = _mapper.Map<ICollection<WorkOrderPartModel>>(workOrderParts);

            // We have to null out the array of children for WorkOrderParts or else the depth of the data structure is too deep for the return object
            // TODO: Since we track if WorkOrderPart is child based on parentId and parent property, the child property is not needed and should be removed
            foreach (var workOrderPartModel in workOrderPartModels)
            {
                workOrderPartModel.Children = null;
            }

            return workOrderPartModels;

        }

        public async Task<WorkOrderPartModel> UpdateWorkOrderPartAsync(UpdateWorkOrderPart command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderPart)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = _unitOfWork.WorkOrderParts.Query().Include(s => s.Part).Where(x => x.Id == command.WorkOrderPartId);

            if (current == null || !current.Any())
            {
                throw new DomainException($"{nameof(WorkOrderPart)} not found with ID: {command.WorkOrderPartId}", DomainError.NotFound);
            }

            WorkOrderPart workOrderPartEntity = await current.FirstOrDefaultAsync(s => s.Id == command.WorkOrderPartId);

            await GetCycleCount(workOrderPartEntity, command.SerialNumber);

            _ = _mapper.Map(command, workOrderPartEntity);

            _unitOfWork.WorkOrderParts.Update(workOrderPartEntity);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workOrderPartEntity, workOrderPartEntity.Id);

            _unitOfWork.WorkOrderParts.LoadReference(workOrderPartEntity, x => x.WorkOrder);
            _unitOfWork.WorkOrders.LoadReference(workOrderPartEntity.WorkOrder, x => x.Purchase);

            // On update, we don't need a pointer back to the work order
            var ret = _mapper.Map<WorkOrderPartModel>(workOrderPartEntity);
            ret.WorkOrder = null;

            return ret;
        }
        public async Task<WorkOrderTaskModel> CreateWorkOrderTaskAsync(CreateWorkOrderTask command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderTask)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            ProcedureStep step = _unitOfWork.ProcedureSteps
                .Query()
                .Include(x => x.ProcedureStepMonitors)
                .FirstOrDefault(x => x.Id == command.ProcedureStepId);

            _ = await _unitOfWork.MonitorTypes.Query().ToListAsync();
            _ = await _unitOfWork.MonitorInputTypes.Query().ToListAsync();

            if (step != null && step.ProcedureStepTypeId == PROCEDURE_STEP_TYPE_NC)
            {
                var workOrderEntity = await _unitOfWork.WorkOrders
                    .FirstOrDefaultAsync(false, s => s.Id == command.WorkOrderId);

                if (!workOrderEntity.HasNCR)
                {
                    workOrderEntity.HasNCR = true;
                    _unitOfWork.WorkOrders.Update(workOrderEntity);
                    await _unitOfWork.SaveChangesAsync();
                }

            }


            if (step == null)
            {
                throw new DomainException(
                    $"No {nameof(ProcedureStep)} with ID {command.ProcedureStepId}",
                    DomainError.NotFound);
            }

            if (!command.ProcedureStepTypeId.HasValue)
            {
                command.ProcedureStepTypeId = step.ProcedureStepTypeId;
            }

            WorkOrderTask newTask = _mapper.Map<WorkOrderTask>(command);

            newTask.WorkOrderTaskMonitors =
                _mapper.Map<List<WorkOrderTaskMonitor>>(step.ProcedureStepMonitors);

            var created = _unitOfWork.WorkOrderTasks.Add(newTask);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(newTask, newTask.Id);

            created.Context.Entry(newTask)
                .Reference(x => x.ProcedureStep).Load();
            created.Context.Entry(newTask.ProcedureStep)
                .Reference(x => x.StepType).Load();

            var ret = _mapper.Map<WorkOrderTaskModel>(newTask);

            // detach backpointer to self
            foreach (WorkOrderTaskMonitorModel wotm in ret.WorkOrderTaskMonitors)
            {
                wotm.WorkOrderTask = null;
            }

            ret.WorkOrder = null;

            return ret;
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
                var statusEntity = await _unitOfWork
                    .Status
                    .Query()
                    .FirstOrDefaultAsync(x =>
                        x.Name.ToUpper().Equals(command.Status.ToUpper()));
                if (statusEntity == null)
                {
                    throw new DomainException($"{nameof(Status)} not found with Name: {command.Status}", DomainError.NotFound);
                }
                current.StatusId = statusEntity.Id;
            }

            WorkOrderTask workOrderTaskEntity = _mapper.Map(command, current);

            _unitOfWork.WorkOrderTasks.Update(workOrderTaskEntity);

            // attach files, if any
            if (command.ReferenceFilesIds != null)
            {
                // First, blank the existing list and the attach the new one.
                List<int> origList = _unitOfWork.FileEntityMap.Query().Where(x =>
                    x.EntityId == workOrderTaskEntity.Id &&
                    x.EntityTableName.ToUpper().Equals("WORKORDERTASK")
                ).Select(x => x.Id).ToList();

                foreach (int id in origList)
                {
                    _unitOfWork.FileEntityMap.Delete(false, id);
                }

                workOrderTaskEntity.ReferenceFiles = new List<FileEntityMap>();
                foreach (int id in command.ReferenceFilesIds)
                {
                    FileEntityMap fem = new FileEntityMap()
                    {
                        EntityId = workOrderTaskEntity.Id,
                        EntityTableName = "WorkOrderTask",
                        FileId = id
                    };
                    workOrderTaskEntity.ReferenceFiles.Add(fem);
                }
            }

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workOrderTaskEntity, workOrderTaskEntity.Id);

            // upload files, if any.
            // This has to happen _after_ the reference file ID list
            // is re-created.
            if (command.ReferenceFiles != null &&
                command.ReferenceFiles?.Count > 0 &&
                command.ReferenceFiles.All(x => x != null))
            {
                foreach (FileModel fmodel in command.ReferenceFiles)
                {
                    FileModel m =
                        await _fileService.CreateFileAsync("WorkOrderTask", current.Id, fmodel);
                }
            }

            // Load any attached files
            _unitOfWork.WorkOrderTasks.LoadCollection(workOrderTaskEntity, "ReferenceFiles");
            foreach (FileEntityMap m in workOrderTaskEntity.ReferenceFiles)
            {
                _unitOfWork.FileEntityMap.LoadReference(m, x => x.FileObject);
            }

            // Build the return object model
            WorkOrderTaskModel workOrderTaskModel = _mapper.Map<Domain.Models.WorkOrderTaskModel>(workOrderTaskEntity);

            workOrderTaskModel.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.WorkOrderTask), workOrderTaskModel.Id).ToList();

            // Update the work order datetimes, if needed
            // IMPORTANT: the return object cannot be remapped after this
            // point because it will cause a backreference and break things.
            _unitOfWork.WorkOrderTasks.LoadReference(workOrderTaskEntity, x => x.Status);
            _unitOfWork.WorkOrderTasks.LoadReference(workOrderTaskEntity, x => x.WorkOrder);

            WorkOrder wo = workOrderTaskEntity.WorkOrder;
            _unitOfWork.WorkOrders.LoadCollection(wo, "WorkOrderTasks");
            // "DONE" states are:
            // 4   Cancelled
            // 8   Closed
            // 3   Complete
            // 6   Rejected
            int[] completed = { 3, 4, 6, 8 };
            if (workOrderTaskEntity.Status.Name.ToUpper().Equals("IN PROGRESS"))
            {
                if (!workOrderTaskEntity.WorkOrder.ActualStartDate.HasValue)
                {
                    wo.ActualStartDate = DateTime.Now;
                    _unitOfWork.WorkOrders.Update(wo);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else if (wo.WorkOrderTasks.All(x => completed.Contains(x.StatusId)))
            {
                if (!workOrderTaskEntity.WorkOrder.ActualEndDate.HasValue)
                {
                    wo.ActualEndDate = DateTime.Now;
                    _unitOfWork.WorkOrders.Update(wo);
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return workOrderTaskModel;
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

            ret = _mapper.Map<WorkOrderTaskMonitorModel>(workordertaskmonitor);

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
                wosum.WorkOrderItemNumber = GetWorkOrderItemNumber(m);

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
                var curstepq = m.WorkOrderTasks.Where(x => !(
                    x.Status.Name.ToUpper().Equals("COMPLETE") ||
                    x.Status.Name.ToUpper().Equals("CANCELLED"))
                );
                if (curstepq.Count() > 0)
                {
                    WorkOrderTaskModel curstep = curstepq.OrderBy(x => x.TaskStepOrder).First();
                    if (curstep.AssignedToUser == null)
                    {
                        wosum.WorkOrderAssignedTo = "";
                    }
                    else
                    {
                        wosum.WorkOrderAssignedTo = curstep.AssignedToUser.FullName;
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
        public int TranslateWOTaskStatusToViewModel(WorkOrderTaskModel task)
        {
            // Status ['Waiting to Start', 'In Progress', 'Cancelled', 'Completed']
            int[] completed = { 3, 6, 8 };
            int status;
            if (task.StatusId == (int)EnumStatusSteps.InProgress)
            {
                status = (int)EnumStatusSteps.InProgress;
            }
            else if (task.StatusId == (int)EnumStatusSteps.Cancelled)
            {
                status = (int)EnumStatusSteps.Cancelled;
            }
            else if (completed.Contains(task.StatusId))
            {
                status = (int)EnumStatusSteps.Complete;
            }
            else
            {
                status = (int)EnumStatusSteps.WaitingtoStart;
            }
            return status;
        }
        public string GetWorkOrderItemNumber(WorkOrderModel workOrderModel)
        {
            string customerName = workOrderModel.Purchase?.PurchaseOrder?.Customer?.Name;
            if (string.IsNullOrEmpty(customerName))
            {
                customerName = "";
            }

            return $"{customerName}-{workOrderModel.Id}";
        }
        public async Task<ICollection<PortalWorkOrderView>> GetPortalWorkOrders(GetPortalWorkOrder command)
        {
            var workOrders = await GetWorkOrderAsync(new GetWorkOrder() { CustomerId = command.CustomerId });

            if (command.PartId.HasValue)
            {
                workOrders = workOrders.Where(i => i.WorkOrderParts.Any(j => j.PartId == command.PartId.Value)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(command.PartName))
            {
                workOrders = workOrders.Where(i => i.WorkOrderParts.Any(j => j.Part != null && j.Part.Name == command.PartName)).ToList();
            }

            var portalViews = workOrders.Select(i => _mapper.Map<PortalWorkOrderView>(i)).ToList();

            var workOrderIds = portalViews.Select(i => i.Id).ToList();
            var workOrderTaskIds = (await _unitOfWork.WorkOrderTasks.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync()).GroupBy(j => j.WorkOrderId).ToDictionary(k => k.Key, l => l.Select(m => m.Id).ToList());

            var notes = await _unitOfWork.WorkOrderMessages.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();
            var files = _unitOfWork.FileEntityMap.Query().Include(i => i.FileObject).ToList().Where(i => workOrderTaskIds.Values.Any(j => j.Contains(i.EntityId)) && i.EntityTableName.Equals("WorkOrderTask") && i.FileObject != null).Select(i => i).ToList();
            var monitors = _unitOfWork.WorkOrderTaskMonitors.Query().ToList().Where(i => workOrderTaskIds.Values.Any(j => j.Contains(i.WorkOrderTaskId))).Select(i => i).ToList();
            var invoiceItems = await _unitOfWork.InvoiceItems.Query().Include(i => i.Invoice).Where(i => i.WorkOrderId != null && workOrderIds.Contains(i.WorkOrderId.Value)).Select(i => i).ToListAsync();
            var imageContentTypes = new List<string>() { "image/jpg", "image/jpeg", "image/gif", "image/png" };

            foreach (var portalView in portalViews)
            {
                var associatedWorkOrder = workOrders.FirstOrDefault(i => i.Id == portalView.Id);
                if (associatedWorkOrder != null)
                {
                    var (completedDenominator, completedNumerator, percentComplete, expectedDurationNumerator, expectedDurationDenominator, percentExpectedDuration) = GetStatusValues(associatedWorkOrder);
                    var parentPart = associatedWorkOrder.WorkOrderParts.Any() ? associatedWorkOrder.WorkOrderParts.FirstOrDefault(i => i.Part != null && i.ParentId == null) : (new WorkOrderPartModel());
                    var inProgressTask = associatedWorkOrder.WorkOrderTasks.FirstOrDefault(i => i.StatusId == (int)WorkOrderStatusEnum.InProgress);
                    if (inProgressTask != null)
                    {
                        portalView.StepText = inProgressTask.ProcedureStep?.Title;
                    }
                    portalView.Messages = notes.Where(i => i.WorkOrderId == portalView.Id).Select(j => _mapper.Map<WorkOrderMessageModel>(j)).ToList();
                    portalView.WorkOrderId = portalView.Id;
                    portalView.PercentageOfTasksCompleted = percentComplete;
                    portalView.PercentageOfTasksCompletedDenominator = completedDenominator;
                    portalView.PercentageOfTasksCompletedNumerator = completedNumerator;
                    portalView.PercentageOfExpectedDurationTimeLogged = percentExpectedDuration;
                    portalView.PercentageOfExpectedDurationTimeLoggedDenominator = expectedDurationDenominator;
                    portalView.PercentageOfExpectedDurationTimeLoggedNumerator = expectedDurationNumerator;
                    portalView.SubParts = associatedWorkOrder.WorkOrderParts.Where(i => i.ParentId != null).ToList();
                    portalView.PartId = parentPart.PartId;
                    portalView.PartName = parentPart.Part != null ? parentPart.Part.Name : string.Empty;
                    portalView.CustomerId = associatedWorkOrder.Purchase?.PurchaseOrder?.CustomerId;
                    portalView.CustomerName = associatedWorkOrder.Purchase?.PurchaseOrder?.Customer?.Name;
                    portalView.SerialNumber = parentPart.SerialNumber;
                    portalView.CompanyPartNumber = parentPart.Part?.PartNumber;
                    portalView.CycleCount = parentPart.CycleCount;
                    portalView.PurchaseOrderNumber = associatedWorkOrder.Purchase?.PurchaseOrder?.ReferencePO;
                    portalView.Qty = associatedWorkOrder.Purchase?.Qty;
                    portalView.StartDate = associatedWorkOrder.ActualStartDate;
                    portalView.DueDate = associatedWorkOrder.ScheduledEndDate;
                    portalView.Price = associatedWorkOrder.Price;

                }

                var workOrderTasks = workOrderTaskIds.ContainsKey(portalView.Id) ? workOrderTaskIds[portalView.Id] : new List<int>();
                if (workOrderTasks.Any())
                {
                    var firstTask = workOrderTasks.First();
                    var workOrderTask = _unitOfWork.WorkOrderTasks.Query().Include(i => i.ProcedureStep).ThenInclude(i => i.Procedure).FirstOrDefault(i => i.Id == firstTask);
                    portalView.HasMonitors = monitors.Any(i => workOrderTasks.Contains(i.WorkOrderTaskId));
                    portalView.HasFiles = files.Any(i => workOrderTasks.Contains(i.EntityId) && !imageContentTypes.Contains(i.FileObject.ContentType));
                    portalView.HasPhotos = files.Any(i => workOrderTasks.Contains(i.EntityId) && imageContentTypes.Contains(i.FileObject.ContentType));
                    portalView.WorkOrderTaskId = firstTask;
                    portalView.ProcedureName = workOrderTask.ProcedureStep?.Procedure?.Name;
                }

                var invoiceItem = invoiceItems.FirstOrDefault(i => i.WorkOrderId.Value == portalView.WorkOrderId);

                if (invoiceItem != null)
                {
                    portalView.InvoiceAmount = invoiceItem.Invoice.Total;
                    portalView.InvoiceDate = invoiceItem.Invoice.InvoiceDate;
                    portalView.InvoiceName = invoiceItem.Invoice.Description;
                }
            }

            return portalViews;
        }
        public async Task<WorkOrderMessageModel> CreateWorkOrderMessageAsync(CreateWorkOrderMessage command)
        {
            var message = new WorkOrderMessage()
            {
                Message = command.Message,
                WorkOrderId = command.WorkOrderId
            };

            await _unitOfWork.WorkOrderMessages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<WorkOrderMessageModel>(message);
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
            int[] completed = { 3, 6, 8 };
            string status;
            if (tasks.Any(x => x.StatusId == (int)EnumStatusSteps.InProgress))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.InProgress);
            }
            else if (tasks.Any(x => x.StatusId == (int)EnumStatusSteps.Cancelled))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.Cancelled);
            }
            else if (tasks.All(x => completed.Contains(x.StatusId)))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.Complete);
            }
            else
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.WaitingtoStart);
            }
            return status;
        }
        private async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryImpl(bool isHistory)
        {
            var gwo = new GetWorkOrder()
            {
                completedOnly = isHistory
            };

            ICollection<WorkOrderModel> workOrderModels = await GetWorkOrderAsync(gwo);
            List<WorkOrderGridSummary> workOrderGridSummaries = new List<WorkOrderGridSummary>();
            foreach (WorkOrderModel workOrderModel in workOrderModels)
            {
                var workOrderGridSummary = _mapper.Map<WorkOrderGridSummary>(workOrderModel);

                //
                // Map the work order database entity to the WIP grid view
                //

                // CurrentActiveTaskName
                var curProc =
                    workOrderModel.WorkOrderTasks.Where(x => x.Status.Name.ToUpper().Equals("IN PROGRESS"));
                if (curProc.Any())
                {
                    var proc = curProc.First();
                    workOrderGridSummary.CurrentActiveTaskName = proc.ProcedureStep.Title;
                }

                // CustomerName
                workOrderGridSummary.CustomerName = workOrderModel.Purchase?.PurchaseOrder?.Customer?.Name;
                if (string.IsNullOrEmpty(workOrderGridSummary.CustomerName))
                {
                    workOrderGridSummary.CustomerName = "";
                }

                // WorkOrderItemNumber
                workOrderGridSummary.WorkOrderItemNumber = GetWorkOrderItemNumber(workOrderModel);

                workOrderGridSummary.ReferencePO = workOrderModel.Purchase?.PurchaseOrder?.ReferencePO;
                // ProcedureName
                var firstProc =
                    workOrderModel.WorkOrderTasks.FirstOrDefault();
                if (firstProc == null)
                {
                    workOrderGridSummary.ProcedureName = "";
                }
                else
                {
                    workOrderGridSummary.ProcedureName = firstProc.ProcedureStep?.Procedure?.Name;
                }

                // Disposition
                // This is a string join of the text values of
                // all procedure steps with a type of "NC Disposition"
                workOrderGridSummary.Disposition = "";
                if (workOrderModel.HasNCR.GetValueOrDefault())
                {
                    var nc = workOrderModel.WorkOrderTasks.Where(x =>
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
                        workOrderGridSummary.Disposition = disp;
                    }
                }

                var statusValues = GetStatusValues(workOrderModel);
                workOrderGridSummary.PercentageOfTasksCompleted = statusValues.percentComplete;
                workOrderGridSummary.PercentageOfTasksCompletedDenominator = statusValues.completedDenominator;
                workOrderGridSummary.PercentageOfTasksCompletedNumerator = statusValues.completedNumerator;
                workOrderGridSummary.PercentageOfExpectedDurationTimeLogged = statusValues.percentExpectedDuration;
                workOrderGridSummary.PercentageOfExpectedDurationTimeLoggedDenominator = (double)statusValues.expectedDurationDenominator;
                workOrderGridSummary.PercentageOfExpectedDurationTimeLoggedNumerator = statusValues.expectedDurationNumerator;

                workOrderGridSummaries.Add(workOrderGridSummary);
            }

            return workOrderGridSummaries;
        }

        private (int completedDenominator, int completedNumerator, decimal percentComplete, decimal expectedDurationNumerator, decimal expectedDurationDenominator, decimal percentExpectedDuration) GetStatusValues(WorkOrderModel workOrderModel)
        {
            int[] pctCompletedIds = { 3, 4, 6, 8 };
            int completedDenominator = workOrderModel.WorkOrderTasks.Count();
            int completedNumerator = workOrderModel.WorkOrderTasks.Where(x => pctCompletedIds.Contains(x.StatusId)).Count();
            decimal pctComplete = completedDenominator == 0 ? 0 : completedNumerator / (decimal)completedDenominator;
            decimal expectedDurationDenominator = (decimal)workOrderModel.WorkOrderTasks.Select(x => x.ProcedureStep.LaborTime).Sum().GetValueOrDefault();
            decimal expectedDurationNumerator = workOrderModel.WorkOrderTasks.Select(x => x.TotalTaskTime).Sum().GetValueOrDefault();
            decimal percentExpectedDuration = expectedDurationDenominator == 0 ? 0 : expectedDurationNumerator / expectedDurationDenominator;
            return (completedDenominator, completedNumerator, pctComplete, expectedDurationNumerator, expectedDurationDenominator, percentExpectedDuration);
        }

        private async Task GetCycleCount(WorkOrderPart workOrderPart, string serialNumber)
        {
            if (workOrderPart != null)
            {
                workOrderPart.SerialNumber = serialNumber;
                var workOrderParts = await _unitOfWork.WorkOrderParts.Query().Include(s => s.Part)
                    .Where(s => s.SerialNumber == workOrderPart.SerialNumber && s.Part != null &&
                                s.Part.PartNumber == workOrderPart.Part.PartNumber).ToListAsync();

                if (!workOrderParts.Any())
                {
                    var workOrderPartsFromHistoryTable = await _unitOfWork.CycleCountHistory.Query().Where(s =>
                        s.SerialNumber == workOrderPart.SerialNumber &&
                        s.PartNumber == workOrderPart.Part.PartNumber).ToListAsync();

                    if (workOrderPartsFromHistoryTable.Any())
                    {
                        workOrderPart.CycleCount = workOrderPartsFromHistoryTable.OrderByDescending(s => s.CycleCount).FirstOrDefault()
                            ?.CycleCount + 1;
                    }
                    else
                    {
                        workOrderPart.CycleCount = 1;
                    }
                }
                else
                {

                    var cycleCount = workOrderParts.OrderByDescending(s => s.CycleCount)
                        .FirstOrDefault(m => m.Id != workOrderPart.Id)
                        ?.CycleCount;

                    if (cycleCount == null)
                    {
                        workOrderPart.CycleCount = 1;
                    }
                    else
                    {
                        workOrderPart.CycleCount = cycleCount + 1;
                    }

                }


            }
        }

    }
}
