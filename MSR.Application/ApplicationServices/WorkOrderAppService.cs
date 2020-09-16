using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public const int PROCEDURE_STEP_TYPE_NC = 3;

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

                // CurrentActiveTaskName
                var curProc =
                    m.WorkOrderTasks.Where(x => x.Status.Name.ToUpper().Equals("IN PROGRESS"));
                if (curProc.Any()) {
                    var proc = curProc.First();
                    sum.CurrentActiveTaskName = proc.ProcedureStep.Title;
                }

                // CustomerName
                sum.CustomerName = m.Purchase?.PurchaseOrder?.Customer?.Name;
                if (string.IsNullOrEmpty(sum.CustomerName)) {
                    sum.CustomerName = "";
                }

                // WorkOrderItemNumber
                string customerPNum = m.Purchase?.CustomerPurchaseNumber;
                if (string.IsNullOrEmpty(customerPNum)) {
                    customerPNum = "";
                }
                sum.WorkOrderItemNumber = $"{sum.CustomerName}-{customerPNum}";

                // ProcedureName
                var firstProc =
                    m.WorkOrderTasks.FirstOrDefault();
                if (firstProc == null) {
                    sum.ProcedureName = "";
                } else {
                    sum.ProcedureName = firstProc.ProcedureStep?.Procedure?.Name;
                }

                // Status ['Waiting Start', 'In Progress', 'Cancelled', 'Completed']
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
                int[] completed = { 3, 6, 8 };
                if (m.WorkOrderTasks.Where(x => x.StatusId == 2).Any()) {
                    sum.Status = "In Progress";
                } else if (m.WorkOrderTasks.Where(x => x.StatusId == 4).Any()) {
                    sum.Status = "Cancelled";
                } else if (m.WorkOrderTasks.All(x => completed.Contains(x.StatusId))) {
                    sum.Status = "Completed";
                } else {
                    sum.Status = "Waiting Start";
                }

                // Disposition
                // This is a string join of the text values of
                // all procedure steps with a type of "NC Disposition"
                sum.Disposition = "";
                if (m.HasNCR.GetValueOrDefault()) {
                    var nc = m.WorkOrderTasks.Where(x =>
                        x.ProcedureStepTypeId == PROCEDURE_STEP_TYPE_NC);
                    if (nc.Any()) {
                        string disp = "";
                        foreach (WorkOrderTaskModel task in nc) {
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
                if (denom > 0) {
                    pctComplete = numer / denom;
                } else {
                    pctComplete = 0m;
                }
                sum.PercentageOfTasksCompleted = pctComplete;

                // PercentageOfExpectedDurationTimeLogged
                decimal denomTime = m.WorkOrderTasks.Select(x => x.ProcedureStep.LaborTime).Sum().GetValueOrDefault();
                decimal numerTime = m.WorkOrderTasks.Select(x => x.TotalTaskTime).Sum().GetValueOrDefault();
                if (denomTime > 0) {
                    sum.PercentageOfExpectedDurationTimeLogged = numerTime / denomTime;
                } else {
                    sum.PercentageOfExpectedDurationTimeLogged = 0;
                }

                ret.Add(sum);
            }
            return new CommandResponse<ICollection<WorkOrderGridSummary>>(ret);
        }
    }
}
