using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.Services.Part;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{

    public class WorkOrderAppService :
        ICommandHandler<GetWorkOrder>,
        ICommandHandler<GetWorkOrderView>,
        ICommandHandler<CreateWorkOrder>,
        ICommandHandler<DeleteWorkOrder>,
        ICommandHandler<GetWorkOrderStatusView>,
        ICommandHandler<UpdateWorkOrderPart>,
        ICommandHandler<CreateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTask>,
        ICommandHandler<UpdateWorkOrderTaskMonitor>,
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

            gwo.completedOnly = command.IsHistory;

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
                sum.WorkOrderItemNumber = WorkOrderService.GetWorkOrderItemNumber(m);

                // ProcedureName
                var firstProc =
                    m.WorkOrderTasks.FirstOrDefault();
                if (firstProc == null) {
                    sum.ProcedureName = "";
                } else {
                    sum.ProcedureName = firstProc.ProcedureStep?.Procedure?.Name;
                }

                // Status ['Waiting Start', 'In Progress', 'Cancelled', 'Completed']
                sum.Status = WorkOrderService.TranslateWOStatusToViewModel(m.WorkOrderTasks);

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

        public async Task<ICommandResponse> HandleAsync(GetWorkOrderStatusView command, CancellationToken cancellationToken = default)
        {
            var gwo = _mapper.Map<GetWorkOrder>(command);

            gwo.statuses = _workOrderService.GetActiveStatusList();
            gwo.invertStatusSet = false;
            ICollection<WorkOrderModel> models = await _workOrderService.GetWorkOrderAsync(gwo);
            List<WorkOrderStatus> ret = new List<WorkOrderStatus>();
            foreach (WorkOrderModel m in models) {
                var sum = _mapper.Map<WorkOrderStatus>(m);

                // PartNumber
                // Lists the first part in the set (this follows
                // the behavior of Answer 2).
                if (m.WorkOrderParts != null && m.WorkOrderParts.Count > 0) {
                    sum.PartNumber = m.WorkOrderParts.First().Part.PartNumber;
                } else {
                    sum.PartNumber = "";
                }

                // ProcedureName
                if (m.WorkOrderTasks != null && m.WorkOrderTasks.Count > 0) {
                    sum.ProcedureName = m.WorkOrderTasks.First().ProcedureStep?.Procedure?.Name;
                } else {
                    sum.ProcedureName = "";
                }

                WorkOrderSummary wosum = new WorkOrderSummary();

                // WorkOrderId
                wosum.WorkOrderId = m.Id.GetValueOrDefault();

                // WorkOrderItemNumber
                wosum.WorkOrderItemNumber = WorkOrderService.GetWorkOrderItemNumber(m);

                // PurchaseOrderLineNumber
                wosum.PurchaseOrderLineNumber = m.Purchase.CustomerLineNumber.ToString();

                // WorkOrderPartSerialNumber
                if (m.WorkOrderParts != null && m.WorkOrderParts.Count > 0) {
                    wosum.WorkOrderPartSerialNumber = m.WorkOrderParts.First().SerialNumber;
                } else {
                    wosum.WorkOrderPartSerialNumber = "";
                }

                // WorkOrderStatus ['Waiting Start', 'In Progress', 'Cancelled', 'Completed']
                wosum.WorkOrderStatus = WorkOrderService.TranslateWOStatusToViewModel(m.WorkOrderTasks);

                // WorkOrderAssignedTo
                var curstep = m.WorkOrderTasks.Where(x => x.Status.Name.ToUpper().Equals("IN PROGRESS"));
                if (curstep.Count() > 0) {
                    wosum.WorkOrderAssignedTo = curstep.First().AssignedToUser.FullName;
                } else if (m.WorkOrderTasks != null && m.WorkOrderTasks.Count > 0) {
                    wosum.WorkOrderAssignedTo = m.WorkOrderTasks.First().AssignedToUser.FullName;
                } else {
                    wosum.WorkOrderAssignedTo = "";
                }

                // WorkOrderHasNcr
                wosum.WorkOrderHasNcr = m.HasNCR.GetValueOrDefault();

                // WorkOrderScheduledEndDate
                wosum.WorkOrderScheduledEndDate = m.ScheduledEndDate;

                sum.WorkOrderSummary = wosum;

                ret.Add(sum);
            }
            return new CommandResponse<ICollection<WorkOrderStatus>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkOrderPart command, CancellationToken cancellationToken = default)
        {
            var ret = await _workOrderService.UpdateWorkOrderPartAsync(command);
            return new CommandResponse<WorkOrderPartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkOrderTask command, CancellationToken cancellationToken = default)
        {
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
    }
}
