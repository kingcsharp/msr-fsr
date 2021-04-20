using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.Services.Part;

namespace MSR.Application.ApplicationServices
{

    public class WorkOrderAppService :
        ICommandHandler<GetWorkOrder>,
        ICommandHandler<GetWorkOrderMenu>,
        ICommandHandler<DeleteWorkOrder>,
        ICommandHandler<UpdateWorkOrderPart>,
        ICommandHandler<CreateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTaskMonitor>,
        ICommandHandler<UpdateWorkOrder>,
        ICommandHandler<GetPortalWorkOrder>,
        ICommandHandler<CreateWorkOrderMessage>,
        ICommandHandler<GetWorkOrderPart>,
        ICommandHandler<GetWorkOrderHistory>
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public WorkOrderAppService(IWorkOrderService procedureService, IMapper mapper, IFileService fileService)
        {
            _workOrderService = procedureService;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.GetWorkOrderAsync(command);
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

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderMenu command, CancellationToken cancellationToken = default)
        {
            ICollection<WorkOrderGridSummary> ret = await _workOrderService.GetWorkOrderGridSummaryAsync(command);
            return new CommandResponse<ICollection<WorkOrderGridSummary>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderPart command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderPartAsync(command);
            return new CommandResponse<WorkOrderPartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrderTask command, CancellationToken cancellationToken = default)
        {
            // some additional defaults, if needed
            if (!command.StatusId.HasValue)
            {
                command.StatusId = 1;
            }

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

        public async Task<ICommandResponse> HandleAsync(GetPortalWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.GetPortalWorkOrders(command);
            return new CommandResponse<ICollection<PortalWorkOrderView>>(ret);
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
    }
}
