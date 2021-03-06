using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Hub;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using MSR.Infrastructure.Resources.Queries;

namespace MSR.Infrastructure.Resources.Services.WorkOrder
{
    public class WorkOrderService : IWorkOrderService
    {
        public const int PROCEDURE_STEP_TYPE_NC = 3;
        public const int PROCEDURE_TYPE_NC = 6;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly EmailInformation _emailInformation;
        private readonly GeneralInformation _generalInformation;
        private readonly IMessageHubClient _messageHub;

        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IEmailService emailService,
            EmailInformation emailInformation, GeneralInformation generalInformation, IMessageHubClient messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _emailService = emailService;
            _emailInformation = emailInformation;
            _generalInformation = generalInformation;
            _messageHub = messageHub;
        }

        public async Task<ICollection<WorkOrderModel>> GetWorkOrderAsync(GetWorkOrder command)
        {
            IQueryable<EntityFramework.Entities.WorkOrder> query = _unitOfWork.WorkOrders.Query();

            if (command.Id.HasValue && command.Id > 0)
            {
                query = query.Where(x => x.Id == command.Id.Value);
            }

            if (command.completedOnly.HasValue)
            {
                query = query.Where(x => x.ActualEndDate.HasValue == command.completedOnly.Value);
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

            if (command.FromDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= command.FromDate);
            }

            if (command.ToDate.HasValue)
            {
                query = query.Where(x => x.CreatedOn <= command.ToDate);
            }

            // There is a 1-to-1 purchase to work order mapping.  This
            // should be a safe include with minimal impact, and it is
            // required to filter on customer ID.
            query = query.Include(x => x.Purchase).ThenInclude(y => y.PurchaseOrder);
            if (command.CustomerId.HasValue)
            {
                query = query.Where(i => i.Purchase.PurchaseOrder.CustomerId == command.CustomerId.Value);
            }

            List<EntityFramework.Entities.WorkOrder> workOrderEntities = await query.Include(s => s.WorkOrderParts).ToListAsync();
            List<int> workOrderIds = workOrderEntities.Select(m => m.Id).ToList();
            _ = await _unitOfWork.WorkOrderParts.Query().Include(m => m.Part).Where(s => workOrderIds.Contains(s.WorkOrderId)).ToListAsync();
            List<WorkOrderTask> workOrderTaskEntities = await _unitOfWork.WorkOrderTasks.Query().Include(u => u.ReferenceFiles).Where(s => workOrderIds.Contains(s.WorkOrderId)).ToListAsync();
            List<ProcedureStep> procedureStepEntities = await _unitOfWork.ProcedureSteps.Query()
                .Include(y => y.ProcedureStepRoles)
                .ThenInclude(y => y.Role)
                .Where(s => workOrderTaskEntities.Select(m => m.ProcedureStepId).Contains(s.Id)).ToListAsync();

            _ = await _unitOfWork.WorkOrderMessages.Query().Where(x => workOrderIds.Contains(x.WorkOrderId)).ToListAsync();
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

            var workOrderModels = new List<WorkOrderModel>();

            _ = await _unitOfWork.MonitorTypes.Query().ToListAsync();
            _ = await _unitOfWork.MonitorInputTypes.Query().ToListAsync();

            foreach (var workOrderEntity in workOrderEntities)
            {
                var workOrderModel = _mapper.Map<WorkOrderModel>(workOrderEntity);

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
                workOrderModels.Add(DetachBackPointers(workOrderModel));
            };

            if (command.completedOnly.HasValue && command.completedOnly.Value)
            {
                workOrderModels = workOrderModels.Where(x => x.Status == EnumUtils.GetDescription(EnumStatusSteps.Cancelled) || x.Status == EnumUtils.GetDescription(EnumStatusSteps.Complete)).ToList();
            }

            if (command.openOnly.HasValue && command.openOnly.Value)
            {
                workOrderModels = workOrderModels.Where(x => x.Status != EnumUtils.GetDescription(EnumStatusSteps.Cancelled) && x.Status != EnumUtils.GetDescription(EnumStatusSteps.Complete)).ToList();
            }

            return workOrderModels.OrderBy(x => x.Id).ToList();
        }

        public async Task<WorkOrderModel> CreateWorkOrderAsync(CreateWorkOrder command)
        {
            // Note the menu permission here: Purchases.  Work orders are created by a user
            // entering a purchase against a purchase order.  The Processor then creates the
            // work order as that user.
            if (!CurrentUser.HasPrivilege(EnumMenuItem.Purchases, EnumPrivilege.CanCreate))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            if (command.ScheduledStartDate == null || command.ScheduledStartDate.Ticks == 0)
            {
                command.ScheduledStartDate = DateTime.Now;
            }

            var workOrderEntity = _mapper.Map<EntityFramework.Entities.WorkOrder>(command);
            var procedureStepIds = workOrderEntity.WorkOrderTasks.Select(m => m.ProcedureStepId);
            var procedureSteps = _unitOfWork.ProcedureSteps.Query().Where(s => procedureStepIds.Contains(s.Id));
            var procedureStepMonitors = _unitOfWork.ProcedureStepMonitors.Query().Where(s => procedureStepIds.Contains(s.ProcedureStepId));

            foreach (var workOrderTask in workOrderEntity.WorkOrderTasks)
            {
                var procedureStep = await procedureSteps.FirstOrDefaultAsync(s => s.Id == workOrderTask.ProcedureStepId);
                workOrderTask.Title = procedureStep.Title;
                workOrderTask.Description = procedureStep.StepText;

                foreach (var workOrderTaskMonitor in workOrderTask.WorkOrderTaskMonitors)
                {

                    var procedureStepMonitor =  await procedureStepMonitors.FirstOrDefaultAsync(s => s.Id == workOrderTaskMonitor.ProcedureMonitorId);

                    workOrderTaskMonitor.Description = procedureStepMonitor.Description;
                    workOrderTaskMonitor.MonitorListId = procedureStepMonitor.MonitorListId;
                    workOrderTaskMonitor.ShouldBe = procedureStepMonitor.ShouldBe;
                    workOrderTaskMonitor.HighTarget = procedureStepMonitor.HighTarget;
                    workOrderTaskMonitor.LowTarget = procedureStepMonitor.LowTarget;
                    workOrderTaskMonitor.Target = procedureStepMonitor.Target;
                    workOrderTaskMonitor.FailAction = procedureStepMonitor.FailAction;
                    workOrderTaskMonitor.SensorName = procedureStepMonitor.SensorName;
                    workOrderTaskMonitor.MonitorTypeId = procedureStepMonitor.MonitorTypeId;
                    workOrderTaskMonitor.InputTypeId = procedureStepMonitor.InputTypeId;
                }
            }

            var created = await _unitOfWork.WorkOrders.AddAsync(workOrderEntity);
            await _unitOfWork.SaveChangesAsync();

            // link work order IDs for all parts
            Stack<WorkOrderPart> parts = new Stack<WorkOrderPart>();
            foreach (var p in workOrderEntity.WorkOrderParts)
            {
                parts.Push(p);
            }
            while (parts.Count > 0)
            {
                var p = parts.Pop();
                p.WorkOrder = workOrderEntity;
                if (p.Children != null && p.Children.Count > 0)
                {
                    foreach (var sp in p.Children)
                    {
                        parts.Push(sp);
                    }
                }
            }

            var parentParts = workOrderEntity.WorkOrderParts.Where(s => s.ParentId == null).ToList();

            foreach (var parentPart in parentParts)
            {
                await SetCycleCount(parentPart);
            }

            await _unitOfWork.WorkOrderStats.AddAsync(new WorkOrderStats()
            {
                WorkOrderId = workOrderEntity.Id,
                TotalTasks = workOrderEntity.WorkOrderTasks != null ? workOrderEntity.WorkOrderTasks.Count() : 0,
                CompletedTasks = 0,
                TotalTimeLogged = 0,
                TotalTaskTime = workOrderEntity.WorkOrderTasks != null ? (decimal?)workOrderEntity.WorkOrderTasks.Where(i => i.ProcedureStep != null).Select(j => j.ProcedureStep.LaborTime).Sum() : (decimal?)0.0,
                ActiveTitle = null
            });

            await _unitOfWork.LogApprovalTransaction(workOrderEntity, workOrderEntity.Id);


            // load required navigation fields
            created.Context.Entry(workOrderEntity)
                .Collection(x => x.WorkOrderParts).Load();
            created.Context.Entry(workOrderEntity)
                .Collection(x => x.WorkOrderTasks).Load();
            created.Context.Entry(workOrderEntity)
                .Reference(x => x.Location).Load();
            created.Context.Entry(workOrderEntity)
                .Reference(x => x.Purchase).Load();
            created.Context.Entry(workOrderEntity)
                .Reference(x => x.Product).Load();
            created.Context.Entry(workOrderEntity.Product)
                .Reference(x => x.Procedure).Load();
            created.Context.Entry(workOrderEntity.Purchase)
                .Reference(x => x.PurchaseOrder).Load();
            created.Context.Entry(workOrderEntity.Purchase.PurchaseOrder)
                .Reference(x => x.Customer).Load();

            WorkOrderModel workOrderModel = DetachBackPointers(
                _mapper.Map<WorkOrderModel>(workOrderEntity)
            );

            // Build out minimal information so this can be
            // displayed on the WO status screen without a reload.
            _ = _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
            {
                workOrderId = workOrderEntity.Id,
                workOrderStatus = TranslateWOStatusToViewModel(workOrderEntity.WorkOrderTasks),
                productName = workOrderEntity.Product?.Name,
                partNumber = workOrderEntity.WorkOrderParts.First().Part.PartNumber,
                procedureName = workOrderEntity.Product?.Procedure?.Name,
                locationName = workOrderEntity.Location.Name,
                customerName = workOrderEntity.Product?.Customer?.Name,
                serialNumber = workOrderEntity.WorkOrderParts.First().SerialNumber,
            });

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

            _ = _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
            {
                workOrderId = workorder.Id,
                workOrderStatus = TranslateWOStatusToViewModel(workorder.WorkOrderTasks)
            });

            return ret;
        }
        public async Task<bool> DeleteWorkOrderAsync(DeleteWorkOrder command)
        {
            var workOrder = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (workOrder is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.WorkOrder)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            _unitOfWork.WorkOrders.Delete(false, workOrder.Id);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workOrder, workOrder.Id);

            _ = _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
            {
                workOrderId = workOrder.Id,
                workOrderStatus = TranslateWOStatusToViewModel(workOrder.WorkOrderTasks)
            });

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

            var subPartIds = product.Part.Subparts.Select(s => s.PartId);
            _ = await _unitOfWork.Parts.Query().Where(s => subPartIds.Contains(s.Id)).ToListAsync();


            int createCount = 1;
            int quantity = command.Qty;

            if (command.SerializeIndividually && command.Qty > 1)
            {
                createCount = command.Qty;
                quantity = 1;
            }

            List<WorkOrderPartModel> parts = new List<WorkOrderPartModel>();
            for ( ; createCount > 0; createCount -= 1)
            {
                List<WorkOrderPartModel> subs = new List<WorkOrderPartModel>();
                if (product.Part.Subparts != null && product.Part.Subparts.Count > 0)
                {
                    foreach (PartSubPartMap partSubPartMapForSubPart in product.Part.Subparts)
                    {
                        int subPartQuantity = partSubPartMapForSubPart.Qty;
                        if (subPartQuantity == 0)
                        {
                            subPartQuantity = 1;
                        }

                        var partSegregationTypeValue = partSubPartMapForSubPart.Part?.SegregationType;

                        for ( ; subPartQuantity > 0; subPartQuantity -= 1)
                        {
                            subs.Add(new WorkOrderPartModel()
                            {
                                PartId = partSubPartMapForSubPart.PartId,
                                ParentId = partSubPartMapForSubPart.ParentPartId,
                                Qty = partSubPartMapForSubPart.Qty,
                                SegregationType = partSegregationTypeValue != null ? EnumUtils.GetValueFromDescription<EnumSegregationType>(partSegregationTypeValue) : EnumSegregationType.NONCU
                            });
                        }
                    }
                }
                parts.Add(new WorkOrderPartModel()
                    {
                        PartId = product.PartId,
                        Children = subs,
                        Qty = quantity
                    });
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

            await SetCycleCount(workOrderPartEntity, command.SerialNumber);

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

            ProcedureStep procedureStepEntity = _unitOfWork.ProcedureSteps
                .Query()
                .Include(x => x.ProcedureStepMonitors)
                .FirstOrDefault(x => x.Id == command.ProcedureStepId);

            _ = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, s => s.Id == procedureStepEntity.ProcedureId);
            _ = await _unitOfWork.MonitorTypes.Query().ToListAsync();
            _ = await _unitOfWork.MonitorInputTypes.Query().ToListAsync();

            bool isNcrTask = false;
            if (procedureStepEntity != null && procedureStepEntity.Procedure?.ProcedureTypeId == PROCEDURE_TYPE_NC)
            {
                var workOrderEntity = await _unitOfWork.WorkOrders
                    .FirstOrDefaultAsync(false, s => s.Id == command.WorkOrderId);

                if (!workOrderEntity.HasNCR)
                {
                    workOrderEntity.HasNCR = true;
                    _unitOfWork.WorkOrders.Update(workOrderEntity);
                    await _unitOfWork.SaveChangesAsync();
                }

                isNcrTask = true;

            }


            if (procedureStepEntity == null)
            {
                throw new DomainException(
                    $"No {nameof(ProcedureStep)} with ID {command.ProcedureStepId}",
                    DomainError.NotFound);
            }

            if (!command.ProcedureStepTypeId.HasValue)
            {
                command.ProcedureStepTypeId = procedureStepEntity.ProcedureStepTypeId;
            }

            WorkOrderTask workOrderTaskEntity = _mapper.Map<WorkOrderTask>(command);

            workOrderTaskEntity.WorkOrderTaskMonitors =
                _mapper.Map<List<WorkOrderTaskMonitor>>(procedureStepEntity.ProcedureStepMonitors);

            workOrderTaskEntity.Description = procedureStepEntity.StepText;
            workOrderTaskEntity.Title = procedureStepEntity.Title;
            workOrderTaskEntity.IsNCRTask = isNcrTask;
            foreach (var workOrderTaskMonitor in workOrderTaskEntity.WorkOrderTaskMonitors)
            {
                var procedureStepMonitor =
                    procedureStepEntity.ProcedureStepMonitors.FirstOrDefault(s =>
                        s.Id == workOrderTaskMonitor.ProcedureMonitorId);

                if (procedureStepMonitor != null)
                {
                    workOrderTaskMonitor.FailAction = procedureStepMonitor.FailAction;
                    workOrderTaskMonitor.Description = procedureStepMonitor.Description;
                    workOrderTaskMonitor.MonitorListId = procedureStepMonitor.MonitorListId;
                    workOrderTaskMonitor.ShouldBe = procedureStepMonitor.ShouldBe;
                    workOrderTaskMonitor.HighTarget = procedureStepMonitor.HighTarget;
                    workOrderTaskMonitor.LowTarget = procedureStepMonitor.LowTarget;
                    workOrderTaskMonitor.Target = procedureStepMonitor.Target;
                    workOrderTaskMonitor.FailAction = procedureStepMonitor.FailAction;
                    workOrderTaskMonitor.SensorName = procedureStepMonitor.SensorName;
                    workOrderTaskMonitor.MonitorTypeId = procedureStepMonitor.MonitorTypeId;
                    workOrderTaskMonitor.InputTypeId = procedureStepMonitor.InputTypeId;
                }
            }


            var created = await _unitOfWork.WorkOrderTasks.AddAsync(workOrderTaskEntity);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workOrderTaskEntity, workOrderTaskEntity.Id);

            await created.Context.Entry(workOrderTaskEntity)
                .Reference(x => x.ProcedureStep).LoadAsync();
            await created.Context.Entry(workOrderTaskEntity.ProcedureStep)
                .Reference(x => x.StepType).LoadAsync();

            var workOrderTaskModel = _mapper.Map<WorkOrderTaskModel>(workOrderTaskEntity);

            // detach backpointer to self
            foreach (WorkOrderTaskMonitorModel workOrderTaskMonitorModel in workOrderTaskModel.WorkOrderTaskMonitors)
            {
                workOrderTaskMonitorModel.WorkOrderTask = null;
            }

            workOrderTaskModel.WorkOrder = null;

            return workOrderTaskModel;
        }

        public async Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderTask)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            int[] completed = { 3, 4, 6, 8 };
            var isTaskStarted = false;
            var isTaskCompleted = false;
            var current = await _unitOfWork.WorkOrderTasks.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrderTask)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (!string.IsNullOrEmpty(command.Status))
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
                //Figure out if this is a Completed workOrder Or Started one
                isTaskStarted = statusEntity.Id == (int)EnumStatusSteps.InProgress;
                isTaskCompleted = completed.Contains(statusEntity.Id);
                current.StatusId = statusEntity.Id;
            }

            WorkOrderTask workOrderTaskEntity = _mapper.Map(command, current);

            if (command.TaskRunningSince == null)
            {
                workOrderTaskEntity.TaskRunningSince = null;
            }

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
            var statEntity = await _unitOfWork.WorkOrderStats.FirstOrDefaultAsync(false, i => i.WorkOrderId == workOrderTaskEntity.WorkOrderId);

            if (isTaskStarted)
            {
                statEntity.ActiveTitle = workOrderTaskEntity.Title;
            }
            else if (isTaskCompleted)
            {
                statEntity.CompletedTasks += 1;
                statEntity.TotalTimeLogged += workOrderTaskEntity.TotalTaskTime;
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

            var workOrderEntity = workOrderTaskEntity.WorkOrder;
            _unitOfWork.WorkOrders.LoadCollection(workOrderEntity, "WorkOrderTasks");
            // "DONE" states are:
            // 4   Cancelled
            // 8   Closed
            // 3   Complete
            // 6   Rejected
            if (workOrderTaskEntity.Status.Name.ToUpper().Equals("IN PROGRESS"))
            {
                if (!workOrderTaskEntity.WorkOrder.ActualStartDate.HasValue)
                {
                    workOrderEntity.ActualStartDate = DateTime.Now;
                    _unitOfWork.WorkOrders.Update(workOrderEntity);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            else if (workOrderEntity.WorkOrderTasks.All(x => completed.Contains(x.StatusId)))
            {
                if (!workOrderTaskEntity.WorkOrder.ActualEndDate.HasValue)
                {
                    workOrderEntity.ActualEndDate = DateTime.Now;
                    _unitOfWork.WorkOrders.Update(workOrderEntity);
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            _ = _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
            {
                workOrderId = workOrderEntity.Id,
                workOrderStatus = TranslateWOStatusToViewModel(workOrderEntity.WorkOrderTasks)
            });

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

            var current = await _unitOfWork.WorkOrderTaskMonitors.Query().Include(x => x.ProcedureStepMonitor).FirstOrDefaultAsync(i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrderTaskMonitor)} not found with ID: {command.Id}", DomainError.NotFound);
            }



            if (current.ProcedureStepMonitor.MonitorTypeId == 6)
            {
                var multival = int.Parse(command.MultiVal);
                var monitorListItem = await _unitOfWork.MonitorListItems.Query().Where(x => x.Id == multival).FirstOrDefaultAsync();
                command.TextVal = monitorListItem.Name;
            }

            var workOrderTaskMonitor = _mapper.Map(command, current);
            _unitOfWork.WorkOrderTaskMonitors.Update(workOrderTaskMonitor);

            await _unitOfWork.LogApprovalTransaction(workOrderTaskMonitor, workOrderTaskMonitor.Id);

            WorkOrderTaskMonitorModel workOrderTaskMonitorModel = _mapper.Map<WorkOrderTaskMonitorModel>(workOrderTaskMonitor);

            var workOrderTaskMonitorEntity = await _unitOfWork.WorkOrderTaskMonitors.Query().FirstOrDefaultAsync(s => s.Id == command.Id);

            if (workOrderTaskMonitorEntity.ProcedureStepMonitor.SendNCREmail.HasValue &&
                workOrderTaskMonitorEntity.ProcedureStepMonitor.SendNCREmail.Value)
            {
                await SendNcrEmailNotification(workOrderTaskMonitorModel.Id, command.SendNCREmail);
            }

            return workOrderTaskMonitorModel;
        }

        public async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryAsync(GetWorkOrderMenu command)
        {
            return await GetWorkOrderGridSummaryImpl();
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
            var workOrders = await GetWorkOrderAsync(_mapper.Map<GetWorkOrder>(command));

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
            var invoiceItems = await _unitOfWork.InvoiceItems.Query().Include(i => i.Invoice).Where(i => workOrderIds.Contains(i.WorkOrderId)).Select(i => i).ToListAsync();
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
                    portalView.SubParts = new List<PortalSubPartView>();//associatedWorkOrder.WorkOrderParts.Where(i => i.ParentId != null).ToList();
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
                    portalView.Disposition = getWorkOrderDisposition(associatedWorkOrder);

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

                var invoiceItem = invoiceItems.FirstOrDefault(i => i.WorkOrderId == portalView.WorkOrderId);

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

        private string TranslateWOStatusToViewModel(ICollection<WorkOrderTask> tasks)
        {
            return TranslateWOStatusToViewModel(
                _mapper.Map<ICollection<WorkOrderTaskModel>>(tasks));
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
            int[] completed = {
                (int)EnumStatusSteps.Complete,
                (int)EnumStatusSteps.Rejected,
                (int)EnumStatusSteps.Closed
            };
            int[] cancelled = {
                (int)EnumStatusSteps.Closed,
                (int)EnumStatusSteps.Cancelled,
            };
            int[] waiting = {
                (int)EnumStatusSteps.Approved,
                (int)EnumStatusSteps.WaitingtoStart
            };
            int[] inProgress = {
                (int)EnumStatusSteps.InProgress,
                (int)EnumStatusSteps.Approved,
                (int)EnumStatusSteps.WaitingtoStart,
                (int)EnumStatusSteps.Complete
            };
            string status;
            if (tasks.All(x => completed.Contains(x.StatusId)))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.Complete);
            }
            else if (tasks.All(x => waiting.Contains(x.StatusId)))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.WaitingtoStart);
            }
            else if (tasks.All(x => inProgress.Contains(x.StatusId)))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.InProgress);
            }
            else if (tasks.Any(x => cancelled.Contains(x.StatusId)))
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.Cancelled);
            }
            else
            {
                status = EnumUtils.GetDescription(EnumStatusSteps.WaitingtoStart);
            }
            return status;
        }

        private async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderGridSummaryImpl()
        {
            var getWorkOrderCommand = new GetWorkOrder()
            {
                completedOnly = false
            };

            ICollection<WorkOrderModel> workOrderModels = await GetWorkOrderAsync(getWorkOrderCommand);
            List<WorkOrderGridSummary> workOrderGridSummaries = new List<WorkOrderGridSummary>();
            foreach (WorkOrderModel workOrderModel in workOrderModels)
            {
                var workOrderGridSummary = _mapper.Map<WorkOrderGridSummary>(workOrderModel);

                var workOrderTaskEntitiesInProgress = workOrderModel.WorkOrderTasks
                    .Where(x => x.StatusId == (int)EnumStatusSteps.InProgress)
                    .OrderBy(s => s.TaskStepOrder).ToList();

                if (workOrderTaskEntitiesInProgress.Any())
                {
                    var workOrderTaskEntityInProgress = workOrderTaskEntitiesInProgress.First();
                    workOrderGridSummary.CurrentActiveTaskName = workOrderTaskEntityInProgress.ProcedureStep?.Title;
                }

                workOrderGridSummary.CustomerName = workOrderModel.Purchase?.PurchaseOrder?.Customer?.Name ?? string.Empty;
                workOrderGridSummary.WorkOrderItemNumber = GetWorkOrderItemNumber(workOrderModel);
                workOrderGridSummary.ReferencePO = workOrderModel.Purchase?.PurchaseOrder?.ReferencePO;

                var firstWorkOrderTask = workOrderModel.WorkOrderTasks.OrderBy(s => s.TaskStepOrder).FirstOrDefault();
                workOrderGridSummary.ProcedureName = firstWorkOrderTask == null ? string.Empty : firstWorkOrderTask.ProcedureStep?.Procedure?.Name;

                workOrderGridSummary.Disposition = getWorkOrderDisposition(workOrderModel, true);

                var statusValues = GetStatusValues(workOrderModel);
                workOrderGridSummary.PercentageOfTasksCompleted = statusValues.percentComplete;
                workOrderGridSummary.PercentageOfTasksCompletedDenominator = statusValues.completedDenominator;
                workOrderGridSummary.PercentageOfTasksCompletedNumerator = statusValues.completedNumerator;
                workOrderGridSummary.PercentageOfExpectedDurationTimeLogged = statusValues.percentExpectedDuration;
                workOrderGridSummary.PercentageOfExpectedDurationTimeLoggedDenominator = (double)statusValues.expectedDurationDenominator;
                workOrderGridSummary.PercentageOfExpectedDurationTimeLoggedNumerator = statusValues.expectedDurationNumerator;


                PartModel parentPart = workOrderModel.Product?.Part;
                workOrderGridSummary.SegregationType = parentPart == null ? EnumSegregationType.NONCU : parentPart.SegregationType;

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

        private async Task SetCycleCount(WorkOrderPart workOrderPart, string newSerialNumber = null)
        {
            if (workOrderPart == null)
            {
                return;
            }

            string serialNumber = newSerialNumber == null ? workOrderPart.SerialNumber : newSerialNumber;
            if (serialNumber == null)
            {
                return;
            }

            workOrderPart.SerialNumber = serialNumber;

            List<WorkOrderPart> workOrderParts = await _unitOfWork.WorkOrderParts.Query().Where(s => s.SerialNumber == workOrderPart.SerialNumber).ToListAsync();
            var workOrderPartIds = workOrderParts.Select(m => m.PartId);
            List<EntityFramework.Entities.Part> partEntities = await _unitOfWork.Parts.Query().Where(s => workOrderPartIds.Contains(s.Id)).ToListAsync();
            
            workOrderParts = workOrderParts.Where(s => s.SerialNumber == workOrderPart.SerialNumber && s.Part.PartNumber == workOrderPart.Part.PartNumber).ToList();


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

        private async Task SendNcrEmailNotification(int? workOrderTaskMonitorId, string ncrEmail)
        {
            var workOrderTaskMonitorEntity = await _unitOfWork.WorkOrderTaskMonitors.Query()
                .FirstOrDefaultAsync(s => s.Id == workOrderTaskMonitorId);
            var workOrderTaskEntity = await _unitOfWork.WorkOrderTasks.Query()
                .FirstOrDefaultAsync(s => s.Id == workOrderTaskMonitorEntity.WorkOrderTaskId);
            var workOrderEntity = await _unitOfWork.WorkOrders.Query()
                .FirstOrDefaultAsync(s => s.Id == workOrderTaskEntity.WorkOrderId);
            var purchaseEntity = await _unitOfWork.Purchases.Query()
                .FirstOrDefaultAsync(s => s.Id == workOrderEntity.PurchaseId);
            var purchaseOrderEntity = await _unitOfWork.PurchaseOrders.Query()
                .FirstOrDefaultAsync(s => s.Id == purchaseEntity.PurchaseOrderId);
            var customerEntity = await _unitOfWork.Customers.Query()
                .FirstOrDefaultAsync(s => s.Id == purchaseOrderEntity.CustomerId);
            var parentPartEntity = await _unitOfWork.WorkOrderParts.Query().Include(s => s.Part)
                .FirstOrDefaultAsync(m => m.WorkOrderId == workOrderEntity.Id && m.ParentId.HasValue == false);
            var primaryContactUserModel = await _unitOfWork.Users.Query()
                .FirstOrDefaultAsync(s => s.Id == customerEntity.PrimaryContactUserId);
            var secondaryContactUserModel = await _unitOfWork.Users.FirstOrDefaultAsync(false,s => s.Id == customerEntity.SecondaryContactUserId);
            var workOrderTaskAssignedUserModel = workOrderTaskEntity.AssignedToUser;

            if ((primaryContactUserModel == null || primaryContactUserModel.IsAnswerUser == true) && string.IsNullOrWhiteSpace(ncrEmail))
            {
                throw new DomainException(
                    "There is no Portal User set as Primary Contact associated with this Work Order",
                    DomainError.NotFound);
            }

            if (workOrderTaskAssignedUserModel == null)
            {
                throw new DomainException("The Work Order Task must have an assigned user",
                    DomainError.InternalServerError);
            }

            var to = string.IsNullOrWhiteSpace(ncrEmail) ? primaryContactUserModel.Email : ncrEmail;
            var from = workOrderTaskAssignedUserModel.Email;
            var carbonCopyList = new List<string>() {workOrderTaskAssignedUserModel.Email};

            if (secondaryContactUserModel != null && secondaryContactUserModel.IsAnswerUser == false)
            {
                carbonCopyList.Add(secondaryContactUserModel?.Email);
            }

            var serialNumber = string.IsNullOrWhiteSpace(parentPartEntity?.SerialNumber) ? "N/A" : parentPartEntity?.SerialNumber;

            StringBuilder body = new StringBuilder($@"
                        Dear MSR-FSR Customer,<br>
                        <br>
                        A product non-conformance has been reported on a part for which you are listed as the NC contact.<br>
                        <br>
                        Date Reported: {workOrderTaskMonitorEntity.LastUpdatedOn}<br>
                        Technician: {workOrderTaskAssignedUserModel.FirstName} {workOrderTaskAssignedUserModel.LastName}<br>
                        Part Name: {parentPartEntity?.Part?.Name}<br>
                        Part Number: {parentPartEntity?.Part?.PartNumber}<br>
                        Serial Number: {serialNumber}<br>
                        WO Number: {workOrderEntity.Id}<br>
                        Description of NC: {workOrderTaskMonitorEntity.TextVal}<br>
                        <br>
                        Please log into the MSR-FSR Portal at <a href=""{_generalInformation.PortalWebsiteUrl}"">portal.msr-fsr.com</a> for additional detail, to view photographs, and to enter a disposition.<br>
                        <br>
                        Alternatively you can email your local MSR-FSR Production Manager or call MSR-FSR at the numbers below:<br>");

            var parentLocationEntities = _unitOfWork.Locations.Query().Where(s => s.ParentId.HasValue == false && s.IsActive == true);

            await parentLocationEntities.ForEachAsync(parentLocationEntity =>
            {
                body.Append($"{parentLocationEntity.Name}, {parentLocationEntity.State} {parentLocationEntity.Country} {parentLocationEntity.Phone}<br>");
            });

            var subject = $"Non-Conformity Reported on {workOrderEntity.Id}";

            await _emailService.SendEmailAsync(from, to, subject, body.ToString(), carbonCopyList, true);

        }

        /// <summary>
        /// This is a string join of the text values of
        /// all procedure steps with a type of "NC Disposition"
        /// and optionally the messages for the work order.
        /// </summary>
        /// <param name="workOrderModel">Work Order Model</param>
        /// <returns>The formatted disposition string</returns>
        private string getWorkOrderDisposition(WorkOrderModel workOrderModel, bool includeMessages = false)
        {
            string dispositionMessage = string.Empty;

            if (workOrderModel.HasNCR.GetValueOrDefault())
            {
                var workOrderTaskModels = workOrderModel.WorkOrderTasks.Where(x =>
                    x.ProcedureStepTypeId == PROCEDURE_STEP_TYPE_NC).ToList();
                if (workOrderTaskModels.Any())
                {
                    foreach (WorkOrderTaskModel workOrderTaskModel in workOrderTaskModels)
                    {
                        dispositionMessage +=
                            String.Join(string.Empty,
                                workOrderTaskModel.WorkOrderTaskMonitors.Select(x =>
                                    x.TextVal == null ? "" : x.TextVal + " | "
                                ).ToList()
                            );
                    }
                }
            }

            if (includeMessages && workOrderModel.WorkOrderMessages != null)
            {
                dispositionMessage += String.Join(string.Empty,
                    workOrderModel.WorkOrderMessages.Select(x =>
                        x.Message == null ? "" : x.Message + " | "
                    ).ToList()
                );
            }
            return dispositionMessage.Trim().Trim('|').Trim();
        }

        public async Task<ICollection<MSR.Domain.Views.WorkOrderHistoryView>> GetWorkOrderHistoryView(GetWorkOrderHistory command) {

            var workOrderHistoryViewEntities = await _unitOfWork.WorkOrderHistoryViews.Query().CreateWorkOrderHistoryViewQuery(command).ToListAsync();

            var workOrderHistoryViewModels = _mapper.Map<ICollection<MSR.Domain.Views.WorkOrderHistoryView>>(workOrderHistoryViewEntities);

            return workOrderHistoryViewModels;

        }

        public async Task<int> GetTotalWorkOrderHistoryViewRows(GetWorkOrderHistory command)
        {
            var totalRows = await _unitOfWork.WorkOrderHistoryViews.Query().CreateWorkOrderHistoryViewQuery(command).CountAsync();

            return totalRows;
        }
    }
}
