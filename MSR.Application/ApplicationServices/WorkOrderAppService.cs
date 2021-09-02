using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        ICommandHandler<AddNCRWorkOrderTask>,
        ICommandHandler<CreateWorkOrder>,
        ICommandHandler<GetAssignedWorkOrders>,
        ICommandHandler<GetInvoiceableWorkOrders>
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IDocumentService _documentService;

        public WorkOrderAppService(IWorkOrderService procedureService, IMapper mapper, IFileService fileService, IDocumentService documentService)
        {
            _workOrderService = procedureService;
            _mapper = mapper;
            _fileService = fileService;
            _documentService = documentService;
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
            var ret = await _workOrderService.GetWorkOrderHistoryView(command);
            int totalRows = await _workOrderService.GetTotalWorkOrderHistoryViewRows(command);
            return new PagingCommandResponse<ICollection<Domain.Views.WorkOrderHistoryView>>(ret, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
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

        public async Task<ICommandResponse> HandleAsync(AddNCRWorkOrderTask command, CancellationToken cancellationToken = default)
        {
            var workOrderTaskModels = await _workOrderService.AddNCRWorkOrderTasksAsync(command);
            
            return new CommandResponse<ICollection<WorkOrderTaskModel>>(workOrderTaskModels);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrder command,
            CancellationToken cancellationToken = default)
        {
            var workOrderDto = CreateWorkOrderDTO.FromCommand(command);
            var tasks = await _workOrderService.GetWorkOrderTasksAsync(command);
            workOrderDto.WorkOrderTasks = tasks;

            var parts = (await _workOrderService.GetWorkOrderPartsAsync(command)).ToList();
            var serialNumberList = workOrderDto.SerialNumbers.ToList();
            var customerLineNumbers = workOrderDto.CustomerLineNumbers.ToList();

            // Copy in the serial numbers entered at purchase time, if any.
            for (var workOrderPartIndex = 0;
                workOrderPartIndex < serialNumberList.Count && workOrderPartIndex < parts.Count;
                workOrderPartIndex += 1)
            {
                if (serialNumberList[workOrderPartIndex] != null)
                {
                    parts[workOrderPartIndex].SerialNumber = serialNumberList[workOrderPartIndex];
                }

                if (customerLineNumbers[workOrderPartIndex] != null)
                {
                    parts[workOrderPartIndex].CustomerLineNumber = customerLineNumbers[workOrderPartIndex];
                }
            }

            workOrderDto.WorkOrderParts = parts;

            var workOrderModelNumber = await _workOrderService.CreateWorkOrderAsync(workOrderDto);
            return new CommandResponse<string>(workOrderModelNumber);
        }
    }
}
