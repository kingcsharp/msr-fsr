using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class WorkOrderAppService :
        ICommandHandler<GetWorkOrder>,
        ICommandHandler<GetWorkOrderView>,
        ICommandHandler<CreateWorkOrder>,
        ICommandHandler<DeleteWorkOrder>,
        ICommandHandler<UpdateWorkOrder>
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
        public async Task<ICommandResponse> HandleAsync(CreateWorkOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.CreateWorkOrderAsync(command);
            return new CommandResponse<WorkOrderModel>(ret);
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

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderView command, CancellationToken cancellationToken = default)
        {
            var gwo = _mapper.Map<GetWorkOrder>(command);
            gwo.statuses = _workOrderService.GetActiveStatusList();
            gwo.invertStatusSet = command.IsHistory;
            ICollection<WorkOrderModel> models = await _workOrderService.GetWorkOrderAsync(gwo);
            List<WorkOrderGridSummary> ret = new List<WorkOrderGridSummary>();
            foreach (WorkOrderModel m in models) {
                var sum = _mapper.Map<WorkOrderGridSummary>(m);

                //
                // Map the work order database entity to the WIP grid view
                //

                var curProc =
                    m.WorkOrderTasks.Where(x => x.Status.Name.ToUpper().Equals("IN PROGRESS"));
                if (curProc.Any()) {
                    var proc = curProc.First();
                    sum.CurrentActiveTaskName = proc.ProcedureStep.Title;
                }

                sum.SerialNumber = m.Purchase.SerialNumber;

/*
DONE public int? PurchaseId { get; set; }
     public string WorkOrderItemNumber { get; set; }
     public string CustomerName { get; set; }
DONE public string LocationName { get; set; }
DONE public string SerialNumber { get; set; }
     public int? PurchaseOrderNumber { get; set; }
     public int? Quantity { get; set; }
DONE public DateTime? ScheduledStartDate { get; set; }
DONE public DateTime? ScheduledEndDate { get; set; }
DONE public DateTime? ActualStartDate { get; set; }
DONE public DateTime? ActualEndDate { get; set; }
DONE public string ProductName { get; set; }
     public string ProcedureName { get; set; }
     public string Status { get; set; }
     public string Disposition { get; set; }
DONE public string CurrentActiveTaskName { get; set; }
     public decimal? PercentageOfTasksCompleted { get; set; }
     public decimal? PercentageOfExpectedDurationTimeLogged { get; set; }
     public bool HasNcr { get; set; }
*/

                ret.Add(sum);
            }
            return new CommandResponse<ICollection<WorkOrderGridSummary>>(ret);
        }
    }
}
