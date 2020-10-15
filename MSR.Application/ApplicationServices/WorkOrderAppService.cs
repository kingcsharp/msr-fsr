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
using MSR.Infrastructure.Resources.Services.Part;

namespace MSR.Application.ApplicationServices
{

    public class WorkOrderAppService :
        ICommandHandler<GetWorkOrder>,
        ICommandHandler<GetWorkOrderHistory>,
        ICommandHandler<GetWorkOrderMenu>,
        ICommandHandler<DeleteWorkOrder>,
        ICommandHandler<GetWorkOrderStatus>,
        ICommandHandler<UpdateWorkOrderPart>,
        ICommandHandler<CreateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTaskMonitor>,
        ICommandHandler<UpdateWorkOrder>,
        ICommandHandler<GetPortalWorkOrder>,
        ICommandHandler<CreateWorkOrderMessage>
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IMapper _mapper;

        public WorkOrderAppService(IWorkOrderService procedureService, IMapper mapper)
        {
            _workOrderService = procedureService;
            _mapper = mapper;
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

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderHistory command, CancellationToken cancellationToken = default)
        {
            ICollection<WorkOrderGridSummary> ret = await _workOrderService.GetWorkOrderGridSummaryAsync(command);
            return new CommandResponse<ICollection<WorkOrderGridSummary>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderMenu command, CancellationToken cancellationToken = default)
        {
            ICollection<WorkOrderGridSummary> ret = await _workOrderService.GetWorkOrderGridSummaryAsync(command);
            return new CommandResponse<ICollection<WorkOrderGridSummary>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderStatus command, CancellationToken cancellationToken = default)
        {
            ICollection<WorkOrderStatus> ret = await _workOrderService.GetWorkOrderStatusAsync(command);
            return new CommandResponse<ICollection<WorkOrderStatus>>(ret);
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
            if (!command.ProcedureStepTypeId.HasValue)
            {
                command.ProcedureStepTypeId = 1;
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
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrderMessage command, CancellationToken cancellationToken = default)
        {
            await _workOrderService.CreateWorkOrderMessageAsync(command);
            return CommandResponse.SuccessCommand;
        }
    }
}
