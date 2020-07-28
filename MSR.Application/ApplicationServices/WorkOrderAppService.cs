using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class WorkOrderAppService :
        ICommandHandler<GetWorkOrder>,
        ICommandHandler<CreateWorkOrder>,
        ICommandHandler<UpdateWorkOrder>
    {
        private readonly IWorkOrderService _procedureService;

        public WorkOrderAppService(IWorkOrderService procedureService)
        {
            _procedureService = procedureService;
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetWorkOrderAsync(command);
            return new CommandResponse<ICollection<WorkOrderModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.CreateWorkOrderAsync(command);
            return new CommandResponse<WorkOrderModel>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.UpdateWorkOrderAsync(command);
            return new CommandResponse<WorkOrderModel>(ret);
        }
    }
}
