using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Amazon.Runtime.Internal;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MSR.Application.Abstractions;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.DTOs;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Application.ApplicationServices
{

    public class WorkOrderAppService :
        ICommandHandler<GetWorkOrder>,
        ICommandHandler<DeleteWorkOrder>,
        ICommandHandler<UpdateWorkOrderPart>,
        ICommandHandler<CreateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTaskMonitor>,
        ICommandHandler<UpdateWorkOrder>,
        ICommandHandler<CreateWorkOrderMessage>,
        ICommandHandler<GetWorkOrderPart>,
        ICommandHandler<GetWorkOrderHistory>,
        ICommandHandler<TakeOverWorkOrder>,
        ICommandHandler<CancelWorkOrder>,
        ICommandHandler<UpdateWorkOrderPrice>,
        ICommandHandler<UpdateWorkOrderEndDate>,
        ICommandHandler<AddNCRWorkOrderTask>,
        ICommandHandler<CreateWorkOrder>,
        ICommandHandler<GetAssignedWorkOrders>,
        ICommandHandler<GetInvoiceableWorkOrders>,
        ICommandHandler<BulkUpdateWorkOrderPart>,
        ICommandHandler<UpdateWorkOrderPartCycleCount>,
        ICommandHandler<TransmitIntelXmlDataByWorkOrder>,
        ICommandHandler<TransmitXmlFile>
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IDocumentService _documentService;
        private readonly ILogger<WorkOrderAppService> _logger;

        public WorkOrderAppService(IWorkOrderService procedureService, IMapper mapper, IFileService fileService, IDocumentService documentService, ILogger<WorkOrderAppService> logger)
        {
            _workOrderService = procedureService;
            _mapper = mapper;
            _fileService = fileService;
            _documentService = documentService;
            _logger = logger;
        }

        public async Task<ICommandResponse> HandleAsync(GetInvoiceableWorkOrders command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.GetInvoiceableWorkOrdersView();
            return new CommandResponse<ICollection<InvoiceableWorkOrderView>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetAssignedWorkOrders command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.GetWorkOrderSelectItems(command);
            return new CommandResponse<ICollection<WorkOrderSelectItem>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrder command, CancellationToken cancellationToken = default)
        {

            var ret = await _workOrderService.GetWorkOrderById(command.Id.Value);
            return new CommandResponse<ICollection<WorkOrderModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderAsync(command);
            return new CommandResponse<WorkOrderModel>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(DeleteWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.DeleteWorkOrderAsync(command);
            return new CommandResponse<bool>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderPart command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderPartAsync(command);
            return new CommandResponse<WorkOrderPartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrderTask command, CancellationToken cancellationToken = default)
        {
            // some additional defaults, if needed
            command.StatusId ??= 1;

            var ret = await _workOrderService.CreateWorkOrderTaskAsync(command);
            return new CommandResponse<WorkOrderTaskModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderTask command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderTaskAsync(command);
            var wo = (await _workOrderService.GetWorkOrderById(ret.WorkOrderId)).FirstOrDefault();

            var isIntel = wo.Product.Customer.Name.ToLower().Contains("intel");
            int[] completed = { 3, 4, 6, 8 };
            var allTasksComplete = wo.WorkOrderTasks.All(wot => completed.Contains(wot.StatusId));

            if (isIntel && allTasksComplete)
            {
                // Run this code in another thread pool so we don't block the action
                Task task = Task.Run(async () =>
                {
                    try
                    {
                        var data = _workOrderService.GetIntelXmlData(ret.WorkOrderId);
                        var xmlCommand = new TransmitIntelXmlDataByWorkOrder { Data = data };
                        await _workOrderService.GenerateAndTransmitXmlFiles(xmlCommand);
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError(ex, $"Error Transmitting Files: {ex.Message}");
                    }
                });
            }
            return new CommandResponse<WorkOrderTaskModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderTaskMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderTaskMonitorAsync(command);
            return new CommandResponse<WorkOrderTaskMonitorModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrderMessage command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.CreateWorkOrderMessageAsync(command);
            return new CommandResponse<WorkOrderMessageModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderPart command, CancellationToken cancellationToken = default)
        {
            var workOrderPartModels = await _workOrderService.GetWorkOrderPartsAsync(command);

            var files = _fileService.ListFilesForEntitySet(new Part().GetType().Name, workOrderPartModels.Select(x => x.Part.Id).ToList());

            foreach (var workOrderPartModel in workOrderPartModels)
            {
                workOrderPartModel.Part.Files = files.Where(x => x.EntityId == workOrderPartModel.Part.Id).ToList();
            }


            return new CommandResponse<ICollection<WorkOrderPartModel>>(workOrderPartModels);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderHistory command, CancellationToken cancellationToken = default)
        {
            var workOrders = await _workOrderService.GetWorkOrderHistoryView(command);
            var workOrderIds = workOrders.Select(i => i.WorkOrderId.Value).ToList();
            var subPartList = await _workOrderService.GetWorkOrderSubParts(workOrderIds);
            foreach (var summary in workOrders.Where(i => i.HasSubParts))
            {
                summary.SubParts = subPartList.Where(i => i.WorkOrderId == summary.WorkOrderId).ToList();
            }
            int totalRows = await _workOrderService.GetTotalWorkOrderHistoryViewRows(command);
            return new PagingCommandResponse<ICollection<Domain.Views.WorkOrderHistoryView>>(workOrders, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
        }

        public async Task<ICommandResponse> HandleAsync(TakeOverWorkOrder command, CancellationToken cancellationToken = default)
        {
            var workOrderTaskModels = await _workOrderService.TakeOverWorkOrderTasks(command);

            return new CommandResponse<ICollection<WorkOrderTaskModel>>(workOrderTaskModels);
        }

        public async Task<ICommandResponse> HandleAsync(CancelWorkOrder command, CancellationToken cancellationToken = default)
        {
            var workOrderTaskModels = await _workOrderService.CancelWorkOrderTasksAsync(command);

            return new CommandResponse<ICollection<WorkOrderTaskModel>>(workOrderTaskModels);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderPrice command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderPriceAsync(command);
            return new CommandResponse<WorkOrderModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderEndDate command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderEndDateAsync(command);

            return new CommandResponse<WorkOrderModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(TransmitIntelXmlDataByWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.GenerateAndTransmitXmlFiles(command);

            return new CommandResponse<ICollection<XmlTransmissionLogModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(AddNCRWorkOrderTask command, CancellationToken cancellationToken = default)
        {
            var workOrderTaskModels = await _workOrderService.AddNCRWorkOrderTasksAsync(command);

            return new CommandResponse<ICollection<WorkOrderTaskModel>>(workOrderTaskModels);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrder command, CancellationToken cancellationToken = default)
        {
            foreach (var product in command.WorkOrderProducts)
            {
                var id = await _workOrderService.GetProductIdFromPurchaseOrderProduct(product.ProductId);
                product.ProductId = id;
            }
            var workOrderDto = CreateWorkOrderDTO.FromCommand(command);
            var productId = command.WorkOrderProducts.First().ProductId;
            var tasks = await _workOrderService.GetWorkOrderTasksAsync(productId);
            workOrderDto.WorkOrderTasks = tasks;


            var parts = (await _workOrderService.GetWorkOrderPartsAsync(command)).ToList();

            workOrderDto.WorkOrderParts = parts;
            workOrderDto.Price = command.WorkOrderProducts.Sum(i => i.Price * i.Qty);
            var workOrderModelNumber = await _workOrderService.CreateWorkOrderAsync(workOrderDto);
            return new CommandResponse<string>(workOrderModelNumber);
        }

        public async Task<ICommandResponse> HandleAsync(BulkUpdateWorkOrderPart command, CancellationToken cancellationToken = default)
        {
            return new CommandResponse<bool>(await _workOrderService.BulkUpdateWorkOrderPart(command));
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderPartCycleCount command, CancellationToken cancellationToken = default)
        {
            var (Success, DisplayString) = await _workOrderService.UpdateWorkOrderPartCycleCount(command);
            var response = new CommandResponse<bool>(Success)
            {
                Success = Success,
                DisplayString = DisplayString
            };

            return response;
        }

        public async Task<ICommandResponse> HandleAsync(TransmitXmlFile command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.RetransmitXmlFile(command);
            return new CommandResponse<XmlTransmissionLogModel>(ret);
        }

    }
}
