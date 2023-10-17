using System;
using System.Collections.Generic;
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
using IronPdf;
using System.Net.Mail;
using MSR.Domain.DTOs;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions;
using System.IO;
using Barcoder.DataMatrix;
using Barcoder.Renderer.Image;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using Renci.SshNet;
using Microsoft.Extensions.Configuration;

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
        private readonly IDownloadFiles _fileDownloader;
        private readonly ILogger _logger;
        private readonly IUploadFiles _fileUploader;
        private readonly IConfiguration _config;

        public WorkOrderService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IEmailService emailService, IConfiguration config,
            EmailInformation emailInformation, GeneralInformation generalInformation, IMessageHubClient messageHub, IFileHandlerFactory fileHanderFactory, ILogger<WorkOrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _emailService = emailService;
            _emailInformation = emailInformation;
            _generalInformation = generalInformation;
            _messageHub = messageHub;
            _fileDownloader = fileHanderFactory.CreateDownloader(FileProvider.S3);
            _logger = logger;
            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _config = config;
        }

        // TODO: WorkOrder Status needs to be calculated in a timely matter
        public async Task<ICollection<WorkOrderSelectItem>> GetWorkOrderSelectItems(GetAssignedWorkOrders command)
        {

            var workOrderSelectItems = await _unitOfWork.WorkOrders.Query()
                            .Include(s => s.Purchase)
                            .Include(s => s.Product).ThenInclude(s => s.Procedure)
                            .Include(s => s.WorkOrderTasks)
                            .Where(m => m.WorkOrderTasks.Any(u => u.AssignedTo == command.AssignedUserId)).Select(s => new WorkOrderSelectItem
                            {
                                WorkOrderId = s.Id,
                                CustomerPurchaseNumber = s.Purchase.CustomerPurchaseNumber,
                                ProcedureName = s.Product.Procedure.Name,
                                PurchaseSerialNumber = s.Purchase.SerialNumber
                            }).ToListAsync();


            return workOrderSelectItems;
        }

        public async Task<ICollection<WorkOrderModel>> GetWorkOrderById(int id)
        {

            var workOrderEntity = await _unitOfWork.WorkOrders.Query()
                .Include(s => s.WorkOrderParts)
                .FirstOrDefaultAsync(s => s.Id == id);

            _ = await _unitOfWork.WorkOrderMessages.Query().Where(s => s.WorkOrderId == workOrderEntity.Id).ToListAsync();
            _ = await _unitOfWork.WorkOrderTasks.Query().Where(s => s.WorkOrderId == workOrderEntity.Id).ToListAsync();

            var workOrderTaskIds = workOrderEntity.WorkOrderTasks.Select(i => i.Id).ToList();
            var workOrderPartIds = workOrderEntity.WorkOrderParts.Select(s => s.PartId).ToList();
            var workOrderWorkOrderPartIds = workOrderEntity.WorkOrderParts.Select(s => s.Id).ToList();

            _ = await _unitOfWork.Parts.Query().Where(s => workOrderPartIds.Contains(s.Id)).ToListAsync();
            _ = await _unitOfWork.Products.Query().FirstOrDefaultAsync(s => workOrderEntity.ProductId == s.Id);

            if (workOrderEntity.Product != null)
            {
                _ = await _unitOfWork.Parts.Query().FirstOrDefaultAsync(s => s.Id == workOrderEntity.Product.PartId);
                _ = await _unitOfWork.Customers.Query().FirstOrDefaultAsync(s => s.Id == workOrderEntity.Product.CustomerId);
                _ = await _unitOfWork.Procedures.Query().FirstOrDefaultAsync(s => s.Id == workOrderEntity.Product.ProcedureId);
            }

            _ = await _unitOfWork.ProcedureSteps.Query()
                .Include(y => y.ProcedureStepRoles).ThenInclude(y => y.Role)
                .Where(s => s.ProcedureId == workOrderEntity.Product.ProcedureId).ToListAsync();

            _ = await _unitOfWork.WorkOrderTaskMonitors.Query()
                .Include(m => m.ProcedureStepMonitor)
                .Where(s => workOrderTaskIds.Contains(s.WorkOrderTaskId)).ToListAsync();

            _ = await _unitOfWork.FileEntityMap.Query().Include(i => i.FileObject).Where(j => j.EntityTableName == "WorkOrderTask" && workOrderTaskIds.Contains(j.EntityId)).ToListAsync();
            _ = await _unitOfWork.Purchases.Query().FirstOrDefaultAsync(s => workOrderEntity.PurchaseId == s.Id);

            if (workOrderEntity.Purchase != null)
            {
                _ = await _unitOfWork.PurchaseOrders.Query().FirstOrDefaultAsync(s => s.Id == workOrderEntity.Purchase.PurchaseOrderId);
                _ = await _unitOfWork.Locations.Query().FirstOrDefaultAsync(s => s.Id == workOrderEntity.Purchase.LocationId);
                _ = await _unitOfWork.PurchaseProductMaps.Query().Where(s => s.PurchaseId == workOrderEntity.PurchaseId).ToListAsync();
                if (workOrderEntity.Purchase.PurchaseProducts != null)
                {
                    var ids = workOrderEntity.Purchase.PurchaseProducts.Select(i => i.PurchaseOrderProductId).ToList();
                    _ = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => ids.Contains(i.Id)).ToListAsync();

                    if (workOrderEntity.Purchase.PurchaseProducts.Any(i => i.PurchaseOrderProduct != null))
                    {
                        var purchaseOrderProductIds = workOrderEntity.Purchase.PurchaseProducts.Select(i => i.PurchaseOrderProduct.ProductId).ToList();
                        _ = await _unitOfWork.Products.Query().Where(i => purchaseOrderProductIds.Contains(i.Id)).ToListAsync();
                    }
                }
            }

            if (workOrderEntity.Purchase.PurchaseOrder.Customer != null)
            {
                var userIds = new List<int>();

                if (workOrderEntity.Purchase.PurchaseOrder.Customer.PrimaryContactUserId.HasValue)
                    userIds.Add(workOrderEntity.Purchase.PurchaseOrder.Customer.PrimaryContactUserId.Value);

                if (workOrderEntity.Purchase.PurchaseOrder.Customer.SecondaryContactUserId.HasValue)
                    userIds.Add(workOrderEntity.Purchase.PurchaseOrder.Customer.SecondaryContactUserId.Value);

                _ = await _unitOfWork.Users.Query().Where(s => userIds.Contains(s.Id)).ToListAsync();

            }

            _ = await _unitOfWork.ProcedureStepTypes.Query().ToListAsync();
            _ = await _unitOfWork.Status.Query().ToListAsync();
            _ = await _unitOfWork.MonitorTypes.Query().ToListAsync();
            _ = await _unitOfWork.MonitorInputTypes.Query().ToListAsync();

            var workOrderModel = _mapper.Map<WorkOrderModel>(workOrderEntity);
            workOrderModel.WorkOrderProducts = workOrderEntity.Purchase.PurchaseProducts.Select(i => _mapper.Map<ProductModel>(i.PurchaseOrderProduct.Product)).ToList();
            workOrderModel.Status = TranslateWOStatusToViewModel(workOrderModel.WorkOrderTasks);
            var workOrderSerialNumbers = workOrderModel.WorkOrderParts.Select(i => i.SerialNumber).ToList();
            var ncrHistoryItems = await _unitOfWork.NCRHistory.Query().Where(i => workOrderPartIds.Contains(i.PartId) && workOrderSerialNumbers.Contains(i.SerialNumber)).ToListAsync();

            var mappedParts = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => workOrderWorkOrderPartIds.Contains(i.WorkOrderPartId)).ToListAsync();
            foreach (var workOrderPart in workOrderModel.WorkOrderParts)
            {
                var mostRecentPart = await _unitOfWork.WorkOrderParts.Query().OrderByDescending(i => i.Id)
                                            .FirstOrDefaultAsync(i => i.SerialNumber == workOrderPart.SerialNumber && i.PartId == workOrderPart.PartId
                                                                    && i.Id < workOrderPart.Id && i.PartData != null);
                if (mostRecentPart != null)
                {
                    workOrderPart.PartData = mostRecentPart.PartData;
                }

                var workOrderPartMappedPart = mappedParts.FirstOrDefault(i => i.WorkOrderPartId == workOrderPart.Id);
                if (workOrderPartMappedPart != null)
                {
                    workOrderPart.TagType = workOrderPartMappedPart.TagType;
                    var associatedWorkOrderTask = workOrderModel.WorkOrderTasks.FirstOrDefault(i => i.Id == workOrderPartMappedPart.WorkOrderTaskId);
                    if (associatedWorkOrderTask != null)
                    {
                        workOrderPart.NCNumber = associatedWorkOrderTask.NCNumber;
                    }
                    workOrderPart.Detail = workOrderPartMappedPart.Detail;
                }

                workOrderPart.NCRHistoryItems = ncrHistoryItems?.Where(i => i.PartId == workOrderPart.PartId
                                                                        && (i.SerialNumber != null
                                                                            && workOrderPart.SerialNumber != null
                                                                            && i.SerialNumber == workOrderPart.SerialNumber)
                                                                        && i.WorkOrderId != workOrderPart.WorkOrderId
                                                                       )?.Select(i => _mapper.Map<NCRHistoryItemModel>(i)).ToList();
            }

            bool invoiceable = true;
            foreach (var workOrderTaskModel in workOrderModel.WorkOrderTasks)
            {
                // enforce sane data by limiting the status IDs returned by the API
                workOrderTaskModel.StatusId = TranslateWOTaskStatusToViewModel(workOrderTaskModel);
                if (workOrderTaskModel.IsNCRTask == true && workOrderTaskModel.StatusId != (int)EnumStatusSteps.Complete)
                {
                    invoiceable = false;
                }
                var workOrderTaskMonitorModels = new List<WorkOrderTaskMonitorModel>();
                var i = 1; // Monitor Number starts at 1
                foreach (var workOrderTaskMonitorModel in workOrderTaskModel.WorkOrderTaskMonitors.OrderBy(x => x.Id))
                {
                    workOrderTaskMonitorModel.MonitorNumber = i;
                    workOrderTaskMonitorModel.WorkOrderTask = null; // avoid loops
                    workOrderTaskMonitorModels.Add(workOrderTaskMonitorModel);
                    i += 1;
                }
                workOrderTaskModel.WorkOrderTaskMonitors = workOrderTaskMonitorModels;

                if (workOrderTaskModel.ReferenceFiles != null && workOrderTaskModel.ReferenceFiles.Any())
                {
                    workOrderTaskModel.ReferenceFiles.ForEach(s => s.FileURL = _fileDownloader.GetURL(s.FileURL, 86400));
                }
                var workOrderMappedParts = mappedParts.Where(i => i.WorkOrderTaskId == workOrderTaskModel.Id);
                workOrderTaskModel.MappedWorkOrderParts = workOrderMappedParts != null && workOrderMappedParts.Any()
                                                                ? workOrderMappedParts.Select(i => new MappedWorkOrderPart()
                                                                {
                                                                    Id = i.WorkOrderPartId,
                                                                    TagType = i.TagType
                                                                }).ToList()
                                                                : new List<MappedWorkOrderPart>();
            }

            workOrderModel.Invoiceable = invoiceable;

            return new List<WorkOrderModel>() { DetachBackPointers(workOrderModel) };
        }

        public async Task<string> CreateWorkOrderAsync(CreateWorkOrderDTO createWorkOrderDto)
        {
            try
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

                if (createWorkOrderDto.ScheduledStartDate.Ticks == 0)
                {
                    createWorkOrderDto.ScheduledStartDate = DateTime.UtcNow;
                }

                var purchase = _unitOfWork.Purchases.FirstOrDefault(false, i => i.Id == createWorkOrderDto.PurchaseId);

                var workOrderEntity = _mapper.Map<EntityFramework.Entities.WorkOrder>(createWorkOrderDto);

                workOrderEntity.ProductId = createWorkOrderDto.WorkOrderProducts.First().ProductId;

                var procedureStepIds = workOrderEntity.WorkOrderTasks.Select(m => m.ProcedureStepId);
                var procedureSteps = await _unitOfWork.ProcedureSteps.Query().Where(s => procedureStepIds.Contains(s.Id)).ToListAsync();
                var procedureStepMonitors = await _unitOfWork.ProcedureStepMonitors.Query().Where(s => procedureStepIds.Contains(s.ProcedureStepId)).ToListAsync();
                var totalLaborTime = (decimal)0.0;
                workOrderEntity.HasMonitor = workOrderEntity.WorkOrderTasks.Any(i => i.WorkOrderTaskMonitors.Any());
                workOrderEntity.HasSubParts = createWorkOrderDto.WorkOrderParts.Any(i => i.Children.Any());
                foreach (var workOrderTask in workOrderEntity.WorkOrderTasks)
                {
                    var procedureStep = procedureSteps.FirstOrDefault(s => s.Id == workOrderTask.ProcedureStepId);
                    workOrderTask.Title = procedureStep?.Title;
                    workOrderTask.Description = procedureStep?.StepText;
                    totalLaborTime += (decimal)(procedureStep?.LaborTime ?? 0.0);

                    foreach (var workOrderTaskMonitor in workOrderTask.WorkOrderTaskMonitors)
                    {
                        var procedureStepMonitor = procedureStepMonitors.FirstOrDefault(s => s.Id == workOrderTaskMonitor.ProcedureMonitorId);

                        workOrderTaskMonitor.Description = procedureStepMonitor?.Description;
                        workOrderTaskMonitor.MonitorListId = procedureStepMonitor?.MonitorListId;
                        workOrderTaskMonitor.ShouldBe = procedureStepMonitor?.ShouldBe;
                        workOrderTaskMonitor.HighTarget = procedureStepMonitor?.HighTarget;
                        workOrderTaskMonitor.LowTarget = procedureStepMonitor?.LowTarget;
                        workOrderTaskMonitor.Target = procedureStepMonitor?.Target;
                        workOrderTaskMonitor.FailAction = procedureStepMonitor?.FailAction;
                        workOrderTaskMonitor.SensorName = procedureStepMonitor?.SensorName;
                        workOrderTaskMonitor.MonitorTypeId = procedureStepMonitor?.MonitorTypeId;
                        workOrderTaskMonitor.InputTypeId = procedureStepMonitor?.InputTypeId;
                    }
                }

                var created = await _unitOfWork.WorkOrders.AddAsync(workOrderEntity);

                var workOrderParts = workOrderEntity.WorkOrderParts.ToList();
                workOrderParts.AddRange(workOrderEntity.WorkOrderParts.SelectMany(i => i.Children));
                workOrderParts.ForEach(wop => wop.WorkOrder = workOrderEntity);

                var parentParts = workOrderEntity.WorkOrderParts.Where(s => s.ParentId == null).ToList();

                foreach (var parentPart in parentParts)
                {
                    await SetCycleCount(parentPart);
                }

                await _unitOfWork.LogApprovalTransaction(workOrderEntity, workOrderEntity.Id);

                // load required navigation fields
                await created.Context.Entry(workOrderEntity)
                    .Collection(x => x.WorkOrderParts).LoadAsync();
                await created.Context.Entry(workOrderEntity)
                    .Collection(x => x.WorkOrderTasks).LoadAsync();
                await created.Context.Entry(workOrderEntity)
                    .Reference(x => x.Location).LoadAsync();
                await created.Context.Entry(workOrderEntity)
                    .Reference(x => x.Purchase).LoadAsync();
                await created.Context.Entry(workOrderEntity)
                    .Reference(x => x.Product).LoadAsync();
                await created.Context.Entry(workOrderEntity.Product)
                    .Reference(x => x.Procedure).LoadAsync();
                await created.Context.Entry(workOrderEntity.Purchase)
                    .Reference(x => x.PurchaseOrder).LoadAsync();
                await created.Context.Entry(workOrderEntity.Purchase.PurchaseOrder)
                    .Reference(x => x.Customer).LoadAsync();

                var workOrderStatEntity = new WorkOrderStats()
                {
                    WorkOrderId = workOrderEntity.Id,
                    TotalTasks = (int)workOrderEntity.WorkOrderTasks?.Count(),
                    CompletedTasks = 0,
                    TotalTimeLogged = 0,
                    TotalTaskTime = totalLaborTime,
                    ActiveTitle = null
                };

                await _unitOfWork.WorkOrderStats.AddAsync(workOrderStatEntity);
                await _unitOfWork.SaveChangesAsync();
                // Build out minimal information so this can be
                // displayed on the WO status screen without a reload.
                await _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
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

                var purchaseOrderEntity = await _unitOfWork.PurchaseOrders.Query().FirstOrDefaultAsync(p => p.Id == createWorkOrderDto.PurchaseOrderId);
                purchaseOrderEntity.UninvoicedBalance += workOrderEntity.Price;

                await _unitOfWork.PurchaseOrders.UpdateAndSaveChangesAsync(purchaseOrderEntity);

                return $"{workOrderEntity.Purchase?.PurchaseOrder?.Customer?.Name ?? string.Empty}-{workOrderEntity.Id}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
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

            //Check to see if it's been invoiced
            var invoicedItem = await _unitOfWork.InvoiceItems.Query().Where(i => i.WorkOrderId == command.Id).ToListAsync();

            if (invoicedItem.Any())
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.WorkOrder)} with ID: {command.Id} has been invoiced and cannot be deleted", DomainError.Conflict);
            }

            var workOrderMessages = await _unitOfWork.WorkOrderMessages.Query().Where(i => i.WorkOrderId == command.Id).ToListAsync();
            if (workOrderMessages.Any())
            {
                foreach (var message in workOrderMessages)
                {
                    _unitOfWork.WorkOrderMessages.Delete(false, message);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            var workOrderParts = await _unitOfWork.WorkOrderParts.Query().Where(i => i.WorkOrderId == command.Id).ToListAsync();
            var workOrderPartIds = workOrderParts.Select(i => i.Id).ToList();

            var workOrderPartNCRMaps = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => workOrderPartIds.Contains(i.WorkOrderPartId)).ToListAsync();
            if (workOrderPartNCRMaps.Any())
            {
                foreach (var ncrMap in workOrderPartNCRMaps)
                {
                    _unitOfWork.WorkOrderPartNCRMap.Delete(false, ncrMap);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            if (workOrderParts.Any())
            {
                foreach (var part in workOrderParts)
                {
                    _unitOfWork.WorkOrderParts.Delete(false, part);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            var workOrderTasks = await _unitOfWork.WorkOrderTasks.Query().Where(i => i.WorkOrderId == command.Id).ToListAsync();
            var workOrderTaskIds = workOrderTasks.Select(i => i.Id).ToList();

            var workOrderTaskMonitors = await _unitOfWork.WorkOrderTaskMonitors.Query().Where(i => workOrderTaskIds.Contains(i.WorkOrderTaskId)).ToListAsync();
            var fileEntityMaps = await _unitOfWork.FileEntityMap.Query().Where(i => i.EntityTableName == "WorkOrderTask" && workOrderTaskIds.Contains(i.EntityId)).ToListAsync();
            var fileIds = fileEntityMaps.Select(i => i.FileId).ToList();
            var files = await _unitOfWork.Files.Query().Where(i => fileIds.Contains(i.Id)).ToListAsync();

            if (files.Any())
            {
                for (int i = 0; i < files.Count; i++)
                {
                    EntityFramework.Entities.File file = files[i];
                    _unitOfWork.Files.Delete(false, file);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            if (workOrderTaskMonitors.Any())
            {
                for (var i = 0; i < workOrderTaskMonitors.Count; i++)
                {
                    WorkOrderTaskMonitor monitor = workOrderTaskMonitors[i];
                    _unitOfWork.WorkOrderTaskMonitors.Delete(false, monitor);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            if (workOrderTasks.Any())
            {
                foreach (var task in workOrderTasks)
                {
                    _unitOfWork.WorkOrderTasks.Delete(false, task);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            var stats = await _unitOfWork.WorkOrderStats.Query().Where(i => i.WorkOrderId == command.Id).ToListAsync();
            if (stats.Any())
            {
                foreach (var stat in stats)
                {
                    _unitOfWork.WorkOrderStats.Delete(false, stat);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            var cancelledLog = _unitOfWork.CancelledWorkOrderLogs.Query().Where(i => i.WorkOrderId == workOrder.Id);
            if (cancelledLog.Any())
            {
                foreach (var log in cancelledLog)
                {
                    _unitOfWork.CancelledWorkOrderLogs.Delete(false, log);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            _unitOfWork.WorkOrders.Delete(false, workOrder.Id);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workOrder, workOrder.Id);

            _ = _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
            {
                workOrderId = workOrder.Id,
                workOrderStatus = "Deleted"
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
        public async Task<ICollection<WorkOrderTaskModel>> GetWorkOrderTasksAsync(int ProductId)
        {
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(false, x => x.Id == ProductId);
            List<ProcedureStep> steps = await _unitOfWork.ProcedureSteps.Query()
                .Where(x => x.ProcedureId == product.ProcedureId)
                .OrderBy(x => x.PrintOrder)
                .ToListAsync();

            _ = await _unitOfWork.ProcedureStepMonitors.Query()
                .Where(x => x.ProcedureStepId.HasValue &&
                            steps.Select(y => y.Id).Contains(x.ProcedureStepId.Value))
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

        public async Task<int> GetProductIdFromPurchaseOrderProduct(int purchaseOrderProductId)
        {
            var purchaseOrderProduct = await _unitOfWork.PurchaseOrderProducts.FirstOrDefaultAsync(false, i => i.Id == purchaseOrderProductId);

            if (purchaseOrderProduct == null)
            {
                throw new DomainException($"{nameof(PurchaseOrderProduct)} with ID: {purchaseOrderProductId} not found", DomainError.NotFound);
            }

            return purchaseOrderProduct.ProductId;
        }
        public async Task<ICollection<WorkOrderPartModel>> GetWorkOrderPartsAsync(CreateWorkOrder command)
        {
            var parts = new List<WorkOrderPartModel>();

            foreach (var item in command.WorkOrderProducts)
            {
                //Get a list of the ProductIds and pull the entire list
                var product = await _unitOfWork.Products
                    .Query()
                    .Include(x => x.Part)
                    .ThenInclude(y => y.Subparts)
                    .FirstAsync(x => x.Id == item.ProductId);

                var subPartIds = product.Part.Subparts.Select(s => s.PartId);
                _ = await _unitOfWork.Parts.Query().Where(s => subPartIds.Contains(s.Id)).ToListAsync();


                int createCount = 1;
                int quantity = item.Qty;

                if (item.SerializeIndividually && item.Qty > 1)
                {
                    createCount = item.Qty;
                    quantity = 1;
                }

                var partList = new List<WorkOrderPartModel>();
                for (; createCount > 0; createCount -= 1)
                {
                    var subParts = new List<WorkOrderPartModel>();
                    if (product.Part.Subparts != null && product.Part.Subparts.Any())
                    {
                        foreach (var partSubPartMapForSubPart in product.Part.Subparts)
                        {
                            var subPartQuantity = partSubPartMapForSubPart.Qty;
                            if (subPartQuantity == 0)
                            {
                                subPartQuantity = 1;
                            }

                            var partSegregationTypeValue = partSubPartMapForSubPart.Part?.SegregationType;

                            for (; subPartQuantity > 0; subPartQuantity -= 1)
                            {
                                subParts.Add(new WorkOrderPartModel()
                                {
                                    PartId = partSubPartMapForSubPart.PartId,
                                    ParentId = partSubPartMapForSubPart.ParentPartId,
                                    Qty = 1,
                                    SegregationType = partSegregationTypeValue != null ? EnumUtils.GetValueFromDescription<EnumSegregationType>(partSegregationTypeValue) : EnumSegregationType.NONCU
                                });
                            }
                        }
                    }

                    partList.Add(new WorkOrderPartModel()
                    {
                        PartId = product.PartId,
                        Children = subParts,
                        Qty = quantity
                    });
                }

                var serialNumberList = item.SerialNumbers.ToList();
                var customerLineNumberList = item.CustomerLineNumbers.ToList();
                for (var workOrderPartIndex = 0; workOrderPartIndex < item.SerialNumbers.Count && workOrderPartIndex < partList.Count; workOrderPartIndex += 1)
                {
                    if (serialNumberList[workOrderPartIndex] != null)
                    {
                        partList[workOrderPartIndex].SerialNumber = serialNumberList[workOrderPartIndex];
                    }

                    if (customerLineNumberList[workOrderPartIndex] != null)
                    {
                        partList[workOrderPartIndex].CustomerLineNumber = customerLineNumberList[workOrderPartIndex];
                    }
                }
                parts.AddRange(partList);
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
            var workOrderPartIds = workOrderParts.Select(i => i.Id).ToList();
            var ncrHistoryItems = await _unitOfWork.NCRHistory.Query().Where(i => workOrderPartIds.Contains(i.WorkOrderPartId)).ToListAsync();
            var DataMatrixItems = await _unitOfWork.WorkOrderPartDataMatrixViews.Query().Where(i => workOrderPartIds.Contains(i.WorkOrderPartId)).ToListAsync();


            // We have to null out the array of children for WorkOrderParts or else the depth of the data structure is too deep for the return object
            // TODO: Since we track if WorkOrderPart is child based on parentId and parent property, the child property is not needed and should be removed
            foreach (var workOrderPartModel in workOrderPartModels)
            {
                workOrderPartModel.NCRHistoryItems = ncrHistoryItems.Where(i => i.WorkOrderPartId == workOrderPartModel.Id
                                                                            && (i.SerialNumber != null
                                                                                && workOrderPartModel.SerialNumber != null
                                                                                && i.SerialNumber == workOrderPartModel.SerialNumber)
                                                                            && i.WorkOrderId != workOrderPartModel.WorkOrderId
                                                                            ).Select(i => _mapper.Map<NCRHistoryItemModel>(i)).ToList();
                workOrderPartModel.DataMatrix = ConvertItemToDataMatrix(DataMatrixItems.FirstOrDefault(i => i.WorkOrderPartId == workOrderPartModel.Id), command.WorkOrderId.GetValueOrDefault(0));
                workOrderPartModel.Children = null;
            }

            return workOrderPartModels;

        }

        private byte[] ConvertItemToDataMatrix(WorkOrderPartDataMatrixView workOrderPartDataMatrixView, int workOrderId)
        {
            if (workOrderPartDataMatrixView == null)
            {
                return null;
            }
            /*
             * 9S == WorkOrderId
             * P == Part Number
             * 1P == SerialNumber
             * 6 == Part.Name
             * 20P == Cycle Count
             * 16D == Date of Printing Sticker
             * 14D == Six Months from 16D
             * 12D == Date of Printing Sticker
             * 30P == N
             * Z == 1
             * V1 == We do not have currently.  Will need to add
             * S == Work Order Part SerialNumber
             * Q == Work Order Part Quantity
             * 3Q == "PCE"
             * 2T == Procedure.Id
             * K == PurchaseOrder.ReferencePO up to the /
             * 4k == PurchaseOrder.ReferencePO after the /
             * 2S == MTTN
             * 1T == PartData
             * 4L == CountryCode
             */
            var recordSeparator = ((char)30).ToString();
            var groupSeparator = ((char)29).ToString();
            var endTransmission = ((char)4).ToString();
            var data = $"[)>{recordSeparator}06{groupSeparator}" +
                        $"9S{workOrderId}{groupSeparator}" +
                        $"P{workOrderPartDataMatrixView.P}{groupSeparator}" +
                        $"1P{workOrderPartDataMatrixView.OneP}{groupSeparator}" +
                        $"2P1{groupSeparator}" +
                        $"20P{workOrderPartDataMatrixView.TwentyP}{groupSeparator}" +
                        $"16D{workOrderPartDataMatrixView.SixteenD}{groupSeparator}" +
                        $"14D{workOrderPartDataMatrixView.FourteenD}{groupSeparator}" +
                        $"12D{workOrderPartDataMatrixView.SixteenD}{groupSeparator}" +
                        $"30P{workOrderPartDataMatrixView.ThirtyP}{groupSeparator}" +
                        $"Z{workOrderPartDataMatrixView.Z}{groupSeparator}" +
                        $"V1{workOrderPartDataMatrixView.V1}{groupSeparator}" +
                        $"S{workOrderPartDataMatrixView.ThreeS}{groupSeparator}" +
                        $"Q{workOrderPartDataMatrixView.Q}{groupSeparator}" +
                        $"3Q{workOrderPartDataMatrixView.ThreeQ}{groupSeparator}" +
                        $"1T{workOrderPartDataMatrixView.OneT}{groupSeparator}" +
                        $"2T{workOrderPartDataMatrixView.TwoT}{groupSeparator}" +
                        $"K{workOrderPartDataMatrixView.K}{groupSeparator}" +
                        $"4k{workOrderPartDataMatrixView.FourK}{groupSeparator}" +
                        $"2S{workOrderPartDataMatrixView.TwoS}{groupSeparator}" +
                        $"4L{workOrderPartDataMatrixView.FourL}" +
                        $"{recordSeparator}{endTransmission}";

            var barcode = DataMatrixEncoder.Encode(data);
            var renderer = new ImageRenderer(imageFormat: ImageFormat.Png);

            using var stream = new MemoryStream();
            renderer.Render(barcode, stream);
            return stream.ToArray();
        }

        public async Task<WorkOrderPartModel> UpdateWorkOrderPartAsync(UpdateWorkOrderPart command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderPart)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var workOrderPartEntity = await _unitOfWork.WorkOrderParts.Query().Include(s => s.Part).FirstOrDefaultAsync(s => s.Id == command.WorkOrderPartId);

            if (workOrderPartEntity == null)
            {
                throw new DomainException($"{nameof(WorkOrderPart)} not found with ID: {command.WorkOrderPartId}", DomainError.NotFound);
            }

            await SetCycleCount(workOrderPartEntity, command.SerialNumber);

            _ = _mapper.Map(command, workOrderPartEntity);

            _unitOfWork.WorkOrderParts.Update(workOrderPartEntity);
            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(workOrderPartEntity, workOrderPartEntity.Id);

            _unitOfWork.WorkOrderParts.LoadReference(workOrderPartEntity, x => x.WorkOrder);
            _unitOfWork.WorkOrders.LoadReference(workOrderPartEntity.WorkOrder, x => x.Purchase);

            // On update, we don't need a pointer back to the work order
            var workOrderPartModel = _mapper.Map<WorkOrderPartModel>(workOrderPartEntity);
            workOrderPartModel.WorkOrder = null;
            var partId = workOrderPartModel.PartId;
            var NCRHistoryItems = await _unitOfWork.NCRHistory.Query().Where(i => i.PartId == partId
                                                                            && (i.SerialNumber != null
                                                                                && command.SerialNumber != null
                                                                                && i.SerialNumber == command.SerialNumber)
                                                                            && i.WorkOrderId != workOrderPartModel.WorkOrderId).ToListAsync();
            workOrderPartModel.NCRHistoryItems = NCRHistoryItems.Select(i => _mapper.Map<NCRHistoryItemModel>(i)).ToList();
            return workOrderPartModel;
        }

        public async Task<bool> BulkUpdateWorkOrderPart(BulkUpdateWorkOrderPart command)
        {
            var workOrderParts = await _unitOfWork.WorkOrderParts.Query().Include(s => s.Part).Where(i => command.WorkOrderPartIds.Contains(i.Id)).ToListAsync();

            workOrderParts.ForEach(i =>
            {
                i.PartData = command.PartData;
                _unitOfWork.WorkOrderParts.Update(i);
            });

            await _unitOfWork.SaveChangesAsync();


            return true;
        }

        public async Task<(bool Success, string DisplayString)> UpdateWorkOrderPartCycleCount(UpdateWorkOrderPartCycleCount command)
        {
            try
            {
                var workOrderPart = _unitOfWork.WorkOrderParts.Query().Include(i => i.Part).OrderByDescending(i => i.LastUpdatedOn == null ? i.CreatedOn : i.LastUpdatedOn)
                                                                      .FirstOrDefault(i => i.SerialNumber == command.SerialNumber && i.Part.PartNumber == command.PartNumber);

                if (workOrderPart == null)
                {
                    throw new DomainException(
                        $"No {nameof(WorkOrderPart)} with PartNumber: {command.PartNumber} && SerialNumber: {command.SerialNumber}",
                        DomainError.NotFound);
                }

                workOrderPart.CycleCount = command.CycleCount;

                _unitOfWork.WorkOrderParts.Update(workOrderPart);
                await _unitOfWork.SaveChangesAsync();

                return (true, $"The CycleCount for WorkOrderPart with Id {workOrderPart.Id} has been updated to {command.CycleCount}");
            }
            catch (Exception ex)
            {
                throw new DomainException($"The Cycle Count for WorkOrderPart with Part Number {command.PartNumber} and Serial Number {command.SerialNumber} could not be updated. " +
                                $"- Error Message: {ex.Message}", DomainError.NotFound);

            }

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

            var workOrderStatEntity = await _unitOfWork.WorkOrderStats.FirstOrDefaultAsync(false, i => i.WorkOrderId == workOrderTaskEntity.WorkOrderId);
            workOrderStatEntity.TotalTasks += 1;
            workOrderStatEntity.TotalTaskTime += procedureStepEntity?.LaborTime == null ? 0 : (decimal)procedureStepEntity?.LaborTime.Value;
            _unitOfWork.WorkOrderStats.Update(workOrderStatEntity);

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

        public async Task<ICollection<WorkOrderTaskModel>> TakeOverWorkOrderTasks(TakeOverWorkOrder takeOverWorkOrder)
        {

            var userEntity = _unitOfWork.Users.Query().FirstOrDefault(s => s.Id == takeOverWorkOrder.UserId);


            if (userEntity == null)
            {

                throw new DomainException(
                    $"No User exist with uid {takeOverWorkOrder.UserId}",
                    DomainError.BadRequest);
            }

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"{userEntity.GetFullName()} does not have edit privileges for Work Order with Id {takeOverWorkOrder.WorkOrderId}",
                    DomainError.BadRequest);
            }

            var workOrderTasksToTakeOverEntities = await _unitOfWork.WorkOrderTasks.Query()
                .Where(s => (s.StatusId == (int)EnumStatusSteps.InProgress || s.StatusId == (int)EnumStatusSteps.WaitingtoStart || s.StatusId == (int)EnumStatusSteps.Approved) && s.WorkOrderId == takeOverWorkOrder.WorkOrderId).ToListAsync();

            if (!workOrderTasksToTakeOverEntities.Any())
            {

                throw new DomainException(
                    $"No {nameof(WorkOrderTask)}s to take over since all are Completed or Cancelled for {nameof(WorkOrder)} with Id {takeOverWorkOrder.WorkOrderId}",
                    DomainError.BadRequest);

            }

            workOrderTasksToTakeOverEntities.ToList().ForEach(workOrderTaskEntity =>
            {

                workOrderTaskEntity.AssignedTo = userEntity.Id;
                workOrderTaskEntity.AssignedToUser = userEntity;
                _unitOfWork.WorkOrderTasks.Update(workOrderTaskEntity);

            });

            await _unitOfWork.SaveChangesAsync();

            var workOrderTaskModels = _mapper.Map<ICollection<WorkOrderTaskModel>>(workOrderTasksToTakeOverEntities);


            return workOrderTaskModels;
        }

        public async Task<ICollection<WorkOrderTaskModel>> AddNCRWorkOrderTasksAsync(AddNCRWorkOrderTask addNCRWorkOrderTask)
        {

            var userEntity = _unitOfWork.Users.Query().FirstOrDefault(s => s.Id == addNCRWorkOrderTask.UserId);

            if (userEntity == null)
            {

                throw new DomainException(
                    $"No User exist with uid {addNCRWorkOrderTask.UserId}",
                    DomainError.BadRequest);
            }

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"user does not have edit privileges for Work Order with Id {addNCRWorkOrderTask.WorkOrderId}",
                    DomainError.BadRequest);
            }

            var workOrderEntity = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == addNCRWorkOrderTask.WorkOrderId);

            if (workOrderEntity == null)
            {
                throw new DomainException(
                    $"No Work Order with Id {addNCRWorkOrderTask.WorkOrderId}",
                    DomainError.BadRequest);
            }

            var procedureEntity = await _unitOfWork.Procedures.Query()
                .Include(x => x.ProcedureSteps)
                .ThenInclude(p => p.ProcedureStepMonitors)
                .Include(x => x.ProcedureSteps)
                .ThenInclude(y => y.ProcedureStepRoles)
                .ThenInclude(z => z.Role)
                .Where(x => x.Id == addNCRWorkOrderTask.ProcedureId)
                .FirstOrDefaultAsync();

            if (procedureEntity == null)
            {
                throw new DomainException(
                    $"No Procedure with Id {addNCRWorkOrderTask.ProcedureId}",
                    DomainError.BadRequest);
            }

            var workOrderTaskIsInProgress = await _unitOfWork.WorkOrderTasks.Query()
            .Where(s => s.WorkOrderId == addNCRWorkOrderTask.WorkOrderId && s.StatusId == (int)EnumStatusSteps.InProgress).FirstOrDefaultAsync();

            var seedStepOrderNumber = workOrderTaskIsInProgress != null ? workOrderTaskIsInProgress.TaskStepOrder : 0;

            var workOrderStatEntity = await _unitOfWork.WorkOrderStats.FirstOrDefaultAsync(false, i => i.WorkOrderId == addNCRWorkOrderTask.WorkOrderId);

            var workOrderTaskEntities = new List<WorkOrderTask>();
            var waitingToStartStatusEntity = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)EnumStatusSteps.WaitingtoStart);
            var ncNumber = $"{workOrderEntity.Id}-{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}";

            foreach (var procedureStepEntity in procedureEntity.ProcedureSteps)
            {
                if (!workOrderEntity.HasNCR)
                {
                    workOrderEntity.HasNCR = true;
                    _unitOfWork.WorkOrders.Update(workOrderEntity);
                    await _unitOfWork.SaveChangesAsync();
                }

                seedStepOrderNumber += 1;

                var workOrderTaskEntity = new WorkOrderTask()
                {
                    WorkOrderId = workOrderEntity.Id,
                    ProcedureStepId = procedureStepEntity.Id,
                    TaskStepOrder = seedStepOrderNumber,
                    AssignedTo = addNCRWorkOrderTask.UserId,
                    StatusId = (int)EnumStatusSteps.WaitingtoStart,
                    TaskIsRunning = false,
                    TaskRunningSince = null,
                    TotalTaskTime = 0,
                    NCNumber = ncNumber,
                    Title = procedureStepEntity.Title,
                    Description = procedureStepEntity.StepText
                };

                if (procedureStepEntity.ProcedureStepTypeId.HasValue)
                {
                    workOrderTaskEntity.ProcedureStepTypeId = procedureStepEntity.ProcedureStepTypeId.Value;
                }

                workOrderTaskEntity.WorkOrderTaskMonitors = _mapper.Map<List<WorkOrderTaskMonitor>>(procedureStepEntity.ProcedureStepMonitors);

                workOrderTaskEntity.Description = procedureStepEntity.StepText;
                workOrderTaskEntity.Title = procedureStepEntity.Title;
                workOrderTaskEntity.IsNCRTask = true;

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

                if (workOrderStatEntity != null)
                {
                    workOrderStatEntity.TotalTasks += 1;
                    workOrderStatEntity.TotalTaskTime += procedureStepEntity?.LaborTime == null ? 0 : (decimal)procedureStepEntity?.LaborTime.Value;
                    _unitOfWork.WorkOrderStats.Update(workOrderStatEntity);

                    await _unitOfWork.WorkOrderTasks.AddAsync(workOrderTaskEntity);
                }
                await _unitOfWork.SaveChangesAsync();

                workOrderTaskEntities.Add(workOrderTaskEntity);
            }

            foreach (var workOrderTaskEntity in workOrderTaskEntities)
            {
                foreach (var workOrderTaskMonitorEntity in workOrderTaskEntity.WorkOrderTaskMonitors)
                {
                    workOrderTaskMonitorEntity.WorkOrderTask = null;
                }

                workOrderTaskEntity.WorkOrder = null;
                workOrderTaskEntity.Status = waitingToStartStatusEntity;
            }

            var workOrderTaskModels = _mapper.Map<ICollection<WorkOrderTaskModel>>(workOrderTaskEntities);

            return workOrderTaskModels;
        }

        public async Task<ICollection<WorkOrderTaskModel>> CancelWorkOrderTasksAsync(CancelWorkOrder cancelWorkOrder)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"user does not have edit privileges for Work Order with Id {cancelWorkOrder.WorkOrderId}",
                    DomainError.BadRequest);
            }

            if (!cancelWorkOrder.Invoiceable)
            {
                await DeleteWorkOrderAsync(new DeleteWorkOrder(cancelWorkOrder.WorkOrderId));
                return new List<WorkOrderTaskModel>();
            }

            var workOrderEntity = await _unitOfWork.WorkOrders.Query()
                .Include(x => x.WorkOrderTasks)
                .Where(x => x.Id == cancelWorkOrder.WorkOrderId)
                .FirstOrDefaultAsync();

            if (workOrderEntity == null)
            {
                throw new DomainException(
                    $"No Work Order with Id {cancelWorkOrder.WorkOrderId}",
                    DomainError.BadRequest);
            }

            var workOrderTasksEntitiesToCancel = workOrderEntity.WorkOrderTasks
                .Where(s => s.StatusId == (int)EnumStatusSteps.InProgress || s.StatusId == (int)EnumStatusSteps.WaitingtoStart || s.StatusId == (int)EnumStatusSteps.Approved).ToList();

            var workOrderTaskIds = workOrderTasksEntitiesToCancel.Select(i => i.Id).ToList();

            if (!workOrderTasksEntitiesToCancel.Any())
            {

                throw new DomainException(
                    $"No {nameof(WorkOrderTask)}s to cancel since all are Completed or Cancelled for {nameof(WorkOrder)} with Id {cancelWorkOrder.WorkOrderId}",
                    DomainError.BadRequest);

            }

            //Get all of the NCRPartMaps and update them
            var workOrderPartNCRMapEntities = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => workOrderTaskIds.Contains(i.WorkOrderTaskId)).ToListAsync();
            if (workOrderPartNCRMapEntities.Any())
            {
                foreach (var ncrMap in workOrderPartNCRMapEntities)
                {
                    ncrMap.TagType = "Cancelled";
                    ncrMap.ClosedOn = DateTime.UtcNow;
                    _unitOfWork.WorkOrderPartNCRMap.Update(ncrMap);
                }

                await _unitOfWork.SaveChangesAsync();
            }

            var workOrderStatEntity = await _unitOfWork.WorkOrderStats.FirstOrDefaultAsync(false, i => i.WorkOrderId == cancelWorkOrder.WorkOrderId);

            _unitOfWork.CancelledWorkOrderLogs.Add(
                new CancelledWorkOrderLog()
                {
                    WasInvoiced = cancelWorkOrder.Invoiceable,
                    WorkOrderId = workOrderEntity.Id
                }
            );

            foreach (var workOrderTaskEntity in workOrderTasksEntitiesToCancel)
            {
                workOrderTaskEntity.StatusId = cancelWorkOrder.Invoiceable ? (int)EnumStatusSteps.Complete : (int)EnumStatusSteps.Cancelled;

                if (cancelWorkOrder.Invoiceable)
                {
                    workOrderStatEntity.CompletedTasks += 1;
                    workOrderStatEntity.TotalTimeLogged += workOrderTaskEntity.TotalTaskTime;
                }
                _unitOfWork.WorkOrderTasks.Update(workOrderTaskEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            _unitOfWork.WorkOrderStats.Update(workOrderStatEntity);

            workOrderEntity.ActualEndDate = DateTime.Now;
            _unitOfWork.WorkOrders.Update(workOrderEntity);

            await _unitOfWork.SaveChangesAsync();

            var workOrderTaskEntities = await _unitOfWork.WorkOrderTasks.Query()
                .Include(x => x.ProcedureStep)
                .Include(x => x.WorkOrderTaskMonitors)
                .Include(x => x.Status)
                .Where(s => s.WorkOrderId == cancelWorkOrder.WorkOrderId).ToListAsync();

            foreach (var workOrderTaskEntity in workOrderTaskEntities)
            {
                foreach (var workOrderTaskMonitorEntity in workOrderTaskEntity.WorkOrderTaskMonitors)
                {
                    workOrderTaskMonitorEntity.WorkOrderTask = null;
                }

                workOrderTaskEntity.WorkOrder = null;
            }

            var workOrderTaskModels = _mapper.Map<ICollection<WorkOrderTaskModel>>(workOrderTasksEntitiesToCancel);

            return workOrderTaskModels;
        }

        public async Task<WorkOrderTaskModel> UpdateWorkOrderTaskAsync(UpdateWorkOrderTask command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WipStatus, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderTask)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var currentMappedParts = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => i.WorkOrderTaskId == command.Id).Select(i => i.WorkOrderPartId).ToListAsync();
            var incomingMappedParts = command.MappedWorkOrderParts == null ? new List<int>() : command.MappedWorkOrderParts.Select(i => i.Id).ToList();
            var PartIdsToAdd = command.MappedWorkOrderParts == null ? new List<int>() : incomingMappedParts.Where(i => !currentMappedParts.Contains(i)).ToList();
            var partIdsToRemove = command.MappedWorkOrderParts == null || !incomingMappedParts.Any() ? currentMappedParts : currentMappedParts.Where(i => !incomingMappedParts.Contains(i)).ToList();
            var partsToRemove = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => partIdsToRemove.Contains(i.WorkOrderPartId)).ToListAsync();
            var partsToUpdate = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => incomingMappedParts.Contains(i.WorkOrderPartId) && !partIdsToRemove.Contains(i.Id)).ToListAsync();

            if (PartIdsToAdd.Any())
            {
                foreach (var partId in PartIdsToAdd)
                {
                    var mappedItem = new WorkOrderPartNCRMapItem()
                    {
                        CreatedBy = CurrentUser.GetId(),
                        WorkOrderPartId = partId,
                        WorkOrderTaskId = command.Id,
                        TagType = command?.MappedWorkOrderParts?.FirstOrDefault(i => i.Id == partId)?.TagType,
                        Detail = command?.MappedWorkOrderParts?.FirstOrDefault(i => i.Id == partId)?.Detail
                    };

                    await _unitOfWork.WorkOrderPartNCRMap.AddAsync(mappedItem);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            if (partsToRemove.Any())
            {
                foreach (var part in partsToRemove)
                {
                    _unitOfWork.WorkOrderPartNCRMap.Delete(false, part);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            if (partsToUpdate.Any())
            {
                foreach (var part in partsToUpdate)
                {
                    var incomingPart = command.MappedWorkOrderParts.FirstOrDefault(i => part.WorkOrderPartId == i.Id && (part.TagType != i.TagType || part.Detail != i.Detail));
                    if (incomingPart == null)
                    {
                        continue;
                    }

                    part.TagType = incomingPart.TagType;
                    part.Detail = incomingPart.Detail;
                    _unitOfWork.WorkOrderPartNCRMap.Update(part);
                }
                await _unitOfWork.SaveChangesAsync();
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

            if (statEntity != null)
            {
                if (isTaskStarted)
                {
                    statEntity.ActiveTitle = workOrderTaskEntity.Title;
                }
                else if (isTaskCompleted)
                {
                    statEntity.ActiveTitle = null;
                    statEntity.CompletedTasks += 1;
                    statEntity.TotalTimeLogged += workOrderTaskEntity.TotalTaskTime;
                }

                _unitOfWork.WorkOrderStats.Update(statEntity);
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

            //Get a list of mapped work order parts
            var mappedWorkOrderNCRMaps = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => i.WorkOrderTaskId == workOrderTaskModel.Id).ToListAsync();

            workOrderTaskModel.MappedWorkOrderParts = mappedWorkOrderNCRMaps.Select(i => new MappedWorkOrderPart()
            {
                Id = i.WorkOrderPartId,
                TagType = i.TagType,
                Detail = i.Detail
            }).ToList();
            // Update the work order datetimes, if needed
            // IMPORTANT: the return object cannot be remapped after this
            // point because it will cause a backreference and break things.
            _unitOfWork.WorkOrderTasks.LoadReference(workOrderTaskEntity, x => x.Status);
            _unitOfWork.WorkOrderTasks.LoadReference(workOrderTaskEntity, x => x.WorkOrder);

            var workOrderEntity = workOrderTaskEntity.WorkOrder;
            _unitOfWork.WorkOrders.LoadCollection(workOrderEntity, "WorkOrderTasks");

            var workOrderTaskIds = workOrderEntity.WorkOrderTasks.Select(i => i.Id).ToList();
            var fileMaps = await _unitOfWork.FileEntityMap.Query().Include(i => i.FileObject).Where(j => j.EntityTableName == "WorkOrderTask" && workOrderTaskIds.Contains(j.EntityId)).ToListAsync();
            var workOrderFileEntities = fileMaps.Where(i => i.FileObject != null).Select(j => j.FileObject).ToList();

            workOrderEntity.HasFile = workOrderFileEntities.Any()
                                            ? workOrderFileEntities.Any(i => !i.ContentType.StartsWith("image"))
                                            : false;
            workOrderEntity.HasPhoto = workOrderFileEntities.Any()
                                            ? workOrderFileEntities.Any(i => i.ContentType.StartsWith("image"))
                                            : false;


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
                }
            }

            _unitOfWork.WorkOrders.Update(workOrderEntity);
            await _unitOfWork.SaveChangesAsync();

            await _messageHub.SendWorkOrderUpdate(new WorkOrderStatusUpdate()
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

            current.WorkOrderTask = await _unitOfWork.WorkOrderTasks.FirstOrDefaultAsync(false, i => i.Id == current.WorkOrderTaskId);
            if (current.ProcedureStepMonitor.MonitorTypeId == 6)
            {
                var ncNumber = current.WorkOrderTask.NCNumber;
                var multival = int.Parse(command.MultiVal);
                var monitorListItem = await _unitOfWork.MonitorListItems.Query().Where(x => x.Id == multival).FirstOrDefaultAsync();
                command.TextVal = monitorListItem.Name;

                if (current.Description.Contains("Customer Disposition"))
                {
                    var allWorkOrderTaskIds = await _unitOfWork.WorkOrderTasks.Query().Where(i => i.WorkOrderId == current.WorkOrderTask.WorkOrderId
                                                                                            && i.NCNumber == ncNumber).Select(i => i.Id).ToListAsync();

                    if (allWorkOrderTaskIds.Any())
                    {
                        //Get all of the ncr maps and set them to closed
                        var ncrMaps = await _unitOfWork.WorkOrderPartNCRMap.Query().Where(i => allWorkOrderTaskIds.Contains(i.WorkOrderTaskId)).ToListAsync();

                        foreach (var ncrMap in ncrMaps)
                        {
                            ncrMap.ClosedOn = DateTime.UtcNow;
                            _unitOfWork.WorkOrderPartNCRMap.Update(ncrMap);
                        }
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
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
            workOrderTaskMonitorModel.WorkOrderTask.WorkOrderTaskMonitors = null;
            return workOrderTaskMonitorModel;
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

            foreach (var wop in model.WorkOrderProducts)
            {
                wop.WorkOrders = null;
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
                .Include(s => s.WorkOrderTasks)
                .ThenInclude(s => s.WorkOrderTaskMonitors)
                .ThenInclude(s => s.ProcedureStepMonitor)
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
            var secondaryContactUserModel = await _unitOfWork.Users.FirstOrDefaultAsync(false, s => s.Id == customerEntity.SecondaryContactUserId);
            var workOrderTaskAssignedUserModel = workOrderTaskEntity.AssignedToUser;

            if ((primaryContactUserModel != null && !primaryContactUserModel.IsAnswerUser.Value) || !string.IsNullOrWhiteSpace(ncrEmail))
            {
                if (workOrderTaskAssignedUserModel == null)
                {
                    throw new DomainException("The Work Order Task must have an assigned user",
                        DomainError.InternalServerError);
                }

                var to = string.IsNullOrWhiteSpace(ncrEmail) ? primaryContactUserModel.Email : ncrEmail;
                var from = workOrderTaskAssignedUserModel.Email;
                var carbonCopyList = new List<string>() { workOrderTaskAssignedUserModel.Email };

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

                var attachments = new List<Attachment>();

                License.LicenseKey = _generalInformation.IronPDFLicense;
                var workOrderPartEntity = workOrderEntity.WorkOrderParts.First();
                var ncrReport = $@"
                <style>
                    .text-right {{
                        text-align: right;
                    }}
                    .mb-10 {{
                        margin-bottom: 10px;
                    }}
                    .border {{
                        border: 1px solid #000;
                        border-collapse: collapse;
                    }}
                    .bg-dark {{
                        background-color: #495057;
                    }}
                    .bg-secondary {{
                        background-color: #868e96;
                    }}
                    .p-2 {{
                        padding: .5rem;
                    }}
                    .w-100 {{
                        width: 100%;
                    }}
                    .font-weight-bold {{
                        font-weight: 700;
                    }}
                    .container {{
                        width: 100%;
                        font-family: arial, sans-serif;
                    }}
                </style>
                <div class=""container"">
                    <div class=""w-100 mb-10 p-2"">Part Non Conformance Report - Work Order {workOrderEntity.Id}</div>
                    <table class=""w-100 border mb-10"">
                        <tr>
                            <td width=""20%"" class=""p-2"">MSR-FSR</td>
                            <td width=""10%""></td>
                            <td class=""p-2"">Customer Part # {workOrderPartEntity?.Part?.PartNumber}, (Serial #: {workOrderPartEntity?.SerialNumber}), {workOrderPartEntity?.Part?.Name}</td>
                        </tr>
                    </table>
                    <table class=""w-100 border mb-10"">
                        <tr>
                            <td class=""p-2"" width=""30%"" style=""text-align: right;"">Date Report Prepared: </td>
                            <td width=""10%""></td>
                            <td class=""p-2"">{workOrderTaskMonitorEntity.LastUpdatedOn}</td>
                        </tr>
                        <tr>
                            <td class=""p-2"" width=""30%"" style=""text-align: right;"">Work Order #: </td>
                            <td width=""10%""></td>
                            <td class=""p-2"">{workOrderEntity.Id}</td>
                        </tr>
                        <tr>
                            <td class=""p-2"" width=""30%"" style=""text-align: right;"">Customer: </td>
                            <td width=""10%""></td>
                            <td class=""p-2"">{customerEntity.Name}</td>
                        </tr>
                        <tr>
                            <td class=""p-2"" width=""30%"" style=""text-align: right;"">Technician: </td>
                            <td width=""10%""></td>
                            <td class=""p-2"">{workOrderTaskAssignedUserModel.FirstName} {workOrderTaskAssignedUserModel.LastName}</td>
                        </tr>
                    </table>
                    <table class=""w-100 border mb-10"">
                        <tr>
                            <td class=""font-weight-bold p-2"" width=""30%"">Part #</td>
                            <td class=""font-weight-bold p-2"" width=""10%""></td>
                            <td class=""font-weight-bold p-2"" width=""30%"">Part Name</td>
                            <td class=""font-weight-bold p-2"" width=""10%""></td>
                            <td class=""font-weight-bold p-2"" width=""30%"">Serial Number</td>
                        </tr>
                        <tr>
                            <td class=""p-2"" width=""30%"">{workOrderPartEntity.Part?.PartNumber}</td>
                            <td width=""10%""></td>
                            <td class=""p-2"" width=""30%"">{workOrderPartEntity.Part?.Name}</td>
                            <td width=""10%""></td>
                            <td class=""p-2"" width=""30%"">{workOrderPartEntity.SerialNumber}</td>
                        </tr>
                    </table>
                ";

                List<FileModel> referenceFiles = new List<FileModel>();
                var taskMonitorReport = "";

                foreach (var woTaskEntity in workOrderEntity.WorkOrderTasks)
                {
                    var isNCRTask = woTaskEntity.IsNCRTask.HasValue ? woTaskEntity.IsNCRTask.Value : false;
                    if ((woTaskEntity.ProcedureStep != null && woTaskEntity.ProcedureStep.ProcedureStepTypeId == 6) || isNCRTask)
                    {
                        referenceFiles.AddRange(_fileService.ListFiles(nameof(WorkOrderTask), woTaskEntity.Id).ToList());
                        var procedureStepEntity = await _unitOfWork.ProcedureSteps.Query().FirstOrDefaultAsync(s => s.Id == woTaskEntity.ProcedureStepId);
                        var taskName = woTaskEntity.ProcedureStepId == null ? woTaskEntity.Title : procedureStepEntity.Title;
                        taskMonitorReport += $@"
                    <tr>
                        <td class=""border bg-dark p-2"" colspan=""3"">{taskName}</td>
                    </tr>
                    <tr>
                        <td class=""border bg-secondary p-2"" width=""50%"">Monitor {woTaskEntity.Id}</ div>
                        <td class=""border bg-secondary p-2"" width=""25%"">Result</td>
                        <td class=""border bg-secondary p-2"" width=""25%"">Comment</td>
                    </tr>
                    ";

                        foreach (var woTaskMonitorEntity in woTaskEntity.WorkOrderTaskMonitors)
                        {
                            var desc = woTaskMonitorEntity.ProcedureMonitorId != null ? woTaskMonitorEntity.ProcedureStepMonitor?.Description : woTaskMonitorEntity.Description;
                            taskMonitorReport += $@"
                            <tr>
                                <td class=""border p-2"" width=""50%"">
                                    {desc}
                                </td>
                                <td class=""border p-2"" width=""25%"">{GetResultFromMonitor(woTaskMonitorEntity)}</td>
                                <td class=""border p-2"" width=""25%"">{woTaskMonitorEntity.Comment}</td>
                            </tr>
                        ";
                        };
                    }
                }

                var associatedPictures = "";
                var associatedDocuments = "";
                foreach (var referenceFileModel in referenceFiles)
                {
                    if (referenceFileModel.ContentType.Contains("image"))
                    {
                        associatedPictures += $@"
                    <a href=""{referenceFileModel.FileURL}"" target=""_blank"">
                        <img style=""width: 33%"" src=""{referenceFileModel.FileURL}"" alt=""{referenceFileModel.Name}"">
                    </a>
                ";
                    }
                    else
                    {
                        associatedDocuments += $@"{referenceFileModel.Name} ";
                    }
                }

                ncrReport += $@"
                <table class=""w-100 border mb-10 p-2"">
                    <tr>
                        <td class=""font-weight-bold text-right p-2"" width=""30%"">Associated Documents:</td>
                        <td class=""p-2"">{associatedDocuments}</td>
                    </tr>
                    <tr>
                        <td class=""font-weight-bold text-right p-2"" width=""30%"">Associated Digital Pictures:</td>
                        <td class=""p-2"">
                            <div style=""display: flex; align-item: flex-start; justify-content: flex-start; flex-wrap: wrap"">{associatedPictures}</div>
                        </td>
                    <tr>
                        <td class=""font-weight-bold text-right p-2"" width=""30%"">Comments:</td>
                        <td class=""p-2""></td>
                    </tr>
                </table>
                <table class=""w-100 border mb-10"">
                {taskMonitorReport}
                </table></div>
                ";

                var renderer = new IronPdf.HtmlToPdf();
                var pdf = renderer.RenderHtmlAsPdf(ncrReport);
                attachments.Add(new Attachment(pdf.Stream, "ncr-report.pdf", "application/pdf"));

                await _emailService.SendEmailAsync(from, to, subject, body.ToString(), carbonCopyList, true, attachments);
            }
        }

        private string GetResultFromMonitor(WorkOrderTaskMonitor workOrderTaskMonitor)
        {
            var monitorType = workOrderTaskMonitor?.ProcedureStepMonitor?.MonitorTypeId;

            switch (monitorType)
            {
                case 1: // Equipment
                    return workOrderTaskMonitor.TextVal;
                case 2: // Number
                    {
                        if (workOrderTaskMonitor.ProcedureStepMonitor?.InputTypeId == 6)
                        {
                            return workOrderTaskMonitor.TextVal;
                        }
                        else
                        {
                            return workOrderTaskMonitor.NumVal.HasValue ? (workOrderTaskMonitor.NumVal.Value == 1 ? "1" : "0") : null;
                        }
                    }
                case 3: // YesOrNo
                    {
                        if (!workOrderTaskMonitor.NumVal.HasValue)
                        {
                            return null;
                        }
                        else
                        {
                            return workOrderTaskMonitor.NumVal.Value == 1 ? "Yes" : "No";
                        }
                    }
                case 4: // Text
                    return workOrderTaskMonitor.TextVal;
                case 5: // PassOrFail
                    {
                        if (!workOrderTaskMonitor.NumVal.HasValue)
                        {
                            return null;
                        }
                        else
                        {
                            return workOrderTaskMonitor.NumVal == 1 ? "Pass" : "Fail";
                        }
                    }
                case 6: // Select
                    return workOrderTaskMonitor.TextVal;
                default:
                    return null;
            }
        }

        /// <summary>
        /// This is a string join of the text values of
        /// all procedure steps with a type of "NC Disposition"
        /// and optionally the messages for the work order.
        /// </summary>
        /// <param name="workOrderModel">Work Order Model</param>
        /// <returns>The formatted disposition string</returns>
        private string GetWorkOrderDisposition(WorkOrderModel workOrderModel, bool includeMessages = false)
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

        public async Task<ICollection<Domain.Views.WorkOrderHistoryView>> GetWorkOrderHistoryView(GetWorkOrderHistory command)
        {

            var workOrderHistoryViewEntities = await _unitOfWork.WorkOrderHistoryViews.Query().CreateWorkOrderHistoryViewQuery(command).ToListAsync();
            var workOrderHistoryViewModels = _mapper.Map<ICollection<Domain.Views.WorkOrderHistoryView>>(workOrderHistoryViewEntities);
            var workOrderIds = workOrderHistoryViewModels.Select(i => i.WorkOrderId).ToList();

            var workOrderMessages = await _unitOfWork.WorkOrderMessages.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();

            foreach (var workOrderHistoryViewModel in workOrderHistoryViewModels)
            {
                workOrderHistoryViewModel.WorkOrderMessages = workOrderMessages.Where(i => i.WorkOrderId == workOrderHistoryViewModel.WorkOrderId).Select(i => _mapper.Map<WorkOrderMessageModel>(i)).ToList();
            }
            return workOrderHistoryViewModels;
        }

        public async Task<int> GetTotalWorkOrderHistoryViewRows(GetWorkOrderHistory command)
        {
            var totalRows = await _unitOfWork.WorkOrderHistoryViews.Query().CreateWorkOrderHistoryViewQuery(command, true).CountAsync();

            return totalRows;
        }

        public async Task<ICollection<MSR.Domain.Views.InvoiceableWorkOrderView>> GetInvoiceableWorkOrdersView()
        {

            var invoiceableWorkOrdersViewEntities = await _unitOfWork.InvoiceableWorkOrdersViews.Query().ToListAsync();

            var invoiceableWorkOrdersViewMdoels = _mapper.Map<ICollection<Domain.Views.InvoiceableWorkOrderView>>(invoiceableWorkOrdersViewEntities);

            return invoiceableWorkOrdersViewMdoels;
        }

        public async Task<ICollection<SubPartModel>> GetWorkOrderSubParts(List<int> workOrderIds)
        {
            if (workOrderIds == null || !workOrderIds.Any())
            {
                return null;
            }
            var subPartsList = await _unitOfWork.SubParts.Query().Where(i => workOrderIds.Contains(i.WorkOrderId)).ToListAsync();
            return subPartsList.Select(i => _mapper.Map<SubPartModel>(i)).ToList();
        }

        public async Task<WorkOrderModel> UpdateWorkOrderPriceAsync(UpdateWorkOrderPrice command)
        {
            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanApprove))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, i => i.Id == command.WorkOrderId);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrder)} not found with ID: {command.WorkOrderId}", DomainError.NotFound);
            }

            WorkOrderModel ret;
            current.Price = command.Price;
            _unitOfWork.WorkOrders.Update(current);
            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(current, current.Id);

            ret = _mapper.Map<WorkOrderModel>(current);

            return ret;
        }

        public async Task<WorkOrderModel> UpdateWorkOrderEndDateAsync(UpdateWorkOrderEndDate command)
        {

            if (!CurrentUser.HasPrivilege(EnumMenuItem.WIPMenu, EnumPrivilege.CanEdit))
            {
                throw new DomainException(
                    $"Permission denied for {nameof(WorkOrderModel)} uid {CurrentUser.GetId()}",
                    DomainError.BadRequest);
            }

            var current = await _unitOfWork.WorkOrders.FirstOrDefaultAsync(false, wo => wo.Id == command.WorkOrderId, wo => wo.Purchase.PurchaseOrder.Customer);

            if (current is null)
            {
                throw new DomainException($"{nameof(WorkOrder)} not found with ID: {command.WorkOrderId}", DomainError.NotFound);
            }

            WorkOrderModel ret;
            current.ScheduledEndDate = command.ScheduledEndDate;
            current.ScheduledEndDateChangeReason = command.ScheduledEndDateChangeReason;
            _unitOfWork.WorkOrders.Update(current);

            // This will call SaveChangesAsync
            await _unitOfWork.LogApprovalTransaction(current, current.Id);



            ret = _mapper.Map<WorkOrderModel>(current);

            return ret;

        }

        public async Task<XmlTransmissionLogModel> RetransmitXmlFile(TransmitXmlFile command)
        {

            var sftpInfo = _config.GetSection(nameof(TransmissionInformation)).Get<TransmissionInformation>();
            var log = _unitOfWork.XmlTransmissionLogs.Query().FirstOrDefault(log => log.Id == command.TransmissionId);
            var fileName= $"{log.XmlLink.Split("/").Last().Split(".xml").First()}.xml";
            var stream = await _fileDownloader.DowloadFile(fileName, sftpInfo.S3Bucket);

            try
            {
                using (SftpClient sftp = new SftpClient(sftpInfo.Host, sftpInfo.Username, sftpInfo.Password))
                {
                    sftp.Connect();
                    sftp.UploadFile(stream, sftpInfo.RemoteDirectory + fileName);
                    sftp.Disconnect();

                    log.Result = "Success";
                    log.SubmittedOn = DateTime.Now;
                    log.TransmissionDetail = "File transmitted successfully";
                }
            }
            catch (Exception ex)
            {
                log.Result = "Failure";
                log.SubmittedOn = DateTime.Now;
                log.TransmissionDetail = $"Failed to send file to SFTP Server. Error: {ex.Message}";
            }

            _unitOfWork.XmlTransmissionLogs.Update(log);

            return new XmlTransmissionLogModel
            {
                Result = log.Result,
                SubmittedOn = log.SubmittedOn,
                TransmissionDetail = log.TransmissionDetail,
                WorkOrderId = log.WorkOrderId,
                XmlLink = log.XmlLink,
                Id = log.Id

            };

        }

        public async Task<ICollection<XmlTransmissionLogModel>> GenerateAndTransmitXmlFiles(TransmitIntelXmlDataByWorkOrder command)
        {
            var monitors = command.Data.WorkOrderMonitors;
            var xmlFiles = command.Data.WorkOrderParts.Select(wop => this.generateXmlDocument(
                wop,
                monitors.ToList())
            );

            var logs = await sendIntelXmlDocuments(command.Data.WorkOrderParts.FirstOrDefault().WorkOrderId, xmlFiles.ToList());

            return logs;
        }

        private async Task<ICollection<XmlTransmissionLogModel>> sendIntelXmlDocuments(int workOrderId, ICollection<XDocument> files)
        {

            var sftpInfo = _config.GetSection(nameof(TransmissionInformation)).Get<TransmissionInformation>();


            using (SftpClient sftp = new SftpClient(sftpInfo.Host, sftpInfo.Username, sftpInfo.Password))
            {
                var logs = new List<XmlTransmissionLogModel>();



                sftp.Connect();

                if (!sftp.Exists(sftpInfo.RemoteDirectory))
                {
                    sftp.Disconnect(); // Disconnect if directory doesn't exist
                    throw new Exception($"The SFTP path: {sftpInfo.RemoteDirectory} cannot be found on the remote server.");
                }

                var uploadTasks = files.Select(async xDoc =>
                {
                    // Log defaults
                    var log = new XmlTransmissionLog
                    {
                        Result = "Success",
                        SubmittedOn = DateTime.Now,
                        XmlLink = "",
                        TransmissionDetail = "Successful SFTP Transmission",
                        WorkOrderId = workOrderId
                    };
                    var fileName = $"intel-{DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss")}.xml";
                    try
                    {
                        try
                        {
                            using (MemoryStream stream = new MemoryStream())
                            {
                                xDoc.Save(stream);
                                stream.Seek(0, SeekOrigin.Begin);
                                var fileModel = new FileModel() { Name = fileName, ContentType = "application/xml" };
                                log.XmlLink = await _fileUploader.UploadFile(stream, fileModel, sftpInfo.S3Bucket);
                            }

                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Failed to send file to S3 bucket. Error: {ex.Message}");
                        }

                        try
                        {
                            using (MemoryStream stream = new MemoryStream())
                            {
                                xDoc.Save(stream);
                                stream.Seek(0, SeekOrigin.Begin);
                                sftp.UploadFile(stream, sftpInfo.RemoteDirectory + fileName);
                            }

                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Failed to send file to SFTP Server. Error: {ex.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.TransmissionDetail = ex.Message;
                        log.Result = "Failure";
                        _unitOfWork.XmlTransmissionLogs.Add(log);
                        _unitOfWork.SaveChanges();

                        logs.Add(new XmlTransmissionLogModel
                        {
                            Result = log.Result,
                            SubmittedOn = log.SubmittedOn,
                            TransmissionDetail = log.TransmissionDetail,
                            WorkOrderId = log.WorkOrderId,
                            XmlLink = log.XmlLink,
                            Id = log.Id

                        });
                    }

                });

                await Task.WhenAll(uploadTasks);

                sftp.Disconnect();

                return logs;

            }
        }

        private XDocument generateXmlDocument(IntelWorkOrderPartView woPart, ICollection<IntelWorkOrderMonitorView> monitors)
        {
            XNamespace xSchema = "x-schema:../Schema/PLTSchema2023May.xml";
            
            IEnumerable<XElement> monitorMaterialParameters = monitors.Select(m => new XElement(xSchema + "MaterialParameter",
                new XElement(xSchema + "ShortName", m.ShortName),
                    new XElement(xSchema + "UnitOfMeasure", m.UnitOfMeasure),
                    new XElement(xSchema + "Measurements",
                      new XElement(xSchema + "Measurement",
                      new XElement(xSchema + "MeasurementType", m.MeasurementType),
                      new XElement(xSchema + "MeasurementValue", m.MeasurementValue),
                      !String.IsNullOrEmpty(m.LowerControlValue) ? new XElement(xSchema + "LCL", m.LowerControlValue) : null,
                      !String.IsNullOrEmpty(m.UpperControlValue) ? new XElement(xSchema + "UCL", m.UpperControlValue) : null
                    )
                )
            ));
            
            XDocument document = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(xSchema + "QualityCertificateFile",
                    new XElement(xSchema + "FileCreationInfo",
                        new XElement(xSchema + "ResponsiblePartyEmail", woPart.ResponsiblePartyEmail)
                    ),
                    new XElement(xSchema + "BusinessSites",
                        new XElement(xSchema + "BusinessSiteDescription",
                            new XElement(xSchema + "ManufacturerNumber", woPart.ManufacturerNumber),
                            new XElement(xSchema + "ManufacturerName", woPart.ManufacturerName),
                            new XElement(xSchema + "ManufacturingPlantCode", woPart.ManufacturingPlantCode),
                            new XElement(xSchema + "QualityCertificates",
                                new XElement(xSchema + "QualityCertificate",
                                    new XAttribute("certificateType", "SingleCertificate"),
                                    new XElement(xSchema +"ThisDocumentGenerationDateTime", woPart.ThisDocumentGenerationDateTime.HasValue ? woPart.ThisDocumentGenerationDateTime?.ToString("MM-dd-yyyy hh:mm:ss") : "N/A"),
                                    new XElement(xSchema + "ProductDescription",
                                        new XElement(xSchema + "ProductName", woPart.CustomerPartName),
                                        new XElement(xSchema + "ManufacturerPartNumber", woPart.ManufacturerNumber),
                                        new XElement(xSchema + "PurchaseOrderNumber", woPart.PurchaseOrderNumber),
                                        new XElement(xSchema + "KitNumber", woPart.KitNumber),
                                        new XElement(xSchema + "KitName", woPart.KitName),
                                        new XElement(xSchema + "PartNumber", woPart.CustomerPartNumber),
                                        new XElement(xSchema + "PartRevisionNumber", "01"),
                                        new XElement(xSchema + "LotCreatedDate", woPart.LotCreatedDate),
                                        new XElement(xSchema + "UnitNumber", woPart.UnitNumber)
                                    ),
                                    new XElement(xSchema + "Shipment",
                                        new XElement(xSchema + "DeliverTo", woPart.CustomerName),
                                        new XElement(xSchema + "ScheduledShipDate", woPart.ScheduledShipDate ?? "N/A"),
                                        new XElement(xSchema + "ActualShipDate", woPart.ActualShipDate ?? "N/A")
                                    ),
                                    new XElement(xSchema + "MaterialParameters",
                                        new XElement(xSchema + "MaterialParameter",
                                            new XElement(xSchema + "ShortName", "Clean Count"),
                                            new XElement(xSchema + "UnitOfMeasure", "N/A"),
                                            new XElement(xSchema + "Measurements",
                                                new XElement(xSchema + "Measurement",
                                                    new XElement(xSchema + "MeasurementType", "Value"),
                                                    new XElement(xSchema + "MeasurementValue", woPart.CycleCount)
                                                )
                                            )
                                        ),
                                        monitorMaterialParameters
                                    )
                                )
                            )
                        )
                    )
                )
            );
            return document;
        }
    }
}
