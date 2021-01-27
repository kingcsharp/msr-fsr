using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MSR.Domain.Helpers;
using MSR.Domain.DTOs;
using System.Collections.Concurrent;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class WorkOrderQueries
    {
        public static async Task<ICollection<InvoiceableWorkOrderView>> GetInvoiceableWorkOrders(this DbSet<WorkOrder> dbSet, Expression<Func<WorkOrder, dynamic>> projection, List<int> invoicedWorkOrderIds)
        {
            var workOrderViews = await QueryHelper.GetViewDataFor<WorkOrder, ICollection<InvoiceableWorkOrderView>>(dbSet,projection);
            var invoiceableWorkOrderViews = workOrderViews.Where(i => !invoicedWorkOrderIds.Contains(i.Id) && i.Status == EnumStatusSteps.Complete).ToList();
            return invoiceableWorkOrderViews;
        }

        public static async Task<ICollection<WorkOrderGridSummary>> GetWorkOrderHistory(this DbSet<WorkOrder> dbSet, Expression<Func<WorkOrder, dynamic>> projection)
        {
            var workOrderHistoryViewDTOs = await QueryHelper.GetViewDataFor<WorkOrder,ICollection<WorkOrderHistoryViewDTO>>(dbSet, projection);
            var workOrderGridSummaryViews = new ConcurrentBag<WorkOrderGridSummary>();
            var workOrderHistoryBag = new ConcurrentBag<WorkOrderHistoryViewDTO>(workOrderHistoryViewDTOs);
            var taskList = new List<Task>();

            Parallel.ForEach(workOrderHistoryViewDTOs, workOrderHistoryViewDTO => 
            {
                var status = GetWorkOrderStatusFromTasks(workOrderHistoryViewDTO.WorkOrderTasks);

                if (status == EnumStatusSteps.Cancelled || status == EnumStatusSteps.Complete)
                {
                    workOrderGridSummaryViews.Add(new WorkOrderGridSummary()
                    {
                        PercentageOfTasksCompleted = CalculateWorkOrderProgress(workOrderHistoryViewDTO).PercentageOfTasksCompleted,
                        PercentageOfTasksCompletedNumerator = CalculateWorkOrderProgress(workOrderHistoryViewDTO).PercentageOfTasksCompletedNumerator,
                        PercentageOfTasksCompletedDenominator = CalculateWorkOrderProgress(workOrderHistoryViewDTO).PercentageOfTasksCompletedDenominator,
                        PercentageOfExpectedDurationTimeLogged = CalculateWorkOrderProgress(workOrderHistoryViewDTO).PercentageOfExpectedDurationTimeLogged,
                        PercentageOfExpectedDurationTimeLoggedNumerator = CalculateWorkOrderProgress(workOrderHistoryViewDTO).PercentageOfExpectedDurationTimeLoggedNumerator,
                        PercentageOfExpectedDurationTimeLoggedDenominator = (double)CalculateWorkOrderProgress(workOrderHistoryViewDTO).PercentageOfExpectedDurationTimeLoggedDenominator,
                        Disposition = GetWorkOrderDisposition(workOrderHistoryViewDTO),
                        ProcedureName = workOrderHistoryViewDTO.WorkOrderTasks != null && workOrderHistoryViewDTO.WorkOrderTasks.Any() ? workOrderHistoryViewDTO.WorkOrderTasks.First().ProcedureName : string.Empty,
                        CurrentActiveTaskName = GetCurrentActiveTaskName(workOrderHistoryViewDTO.WorkOrderTasks),
                        Quantity = workOrderHistoryViewDTO.WorkOrderPart?.Qty,
                        Status = status.ToString(),
                        Id = workOrderHistoryViewDTO.Id,
                        PurchaseId = workOrderHistoryViewDTO.PurchaseId,
                        WorkOrderItemNumber = workOrderHistoryViewDTO.WorkOrderItemNumber,
                        CustomerName = workOrderHistoryViewDTO.CustomerName,
                        LocationName = workOrderHistoryViewDTO.LocationName,
                        SerialNumber = workOrderHistoryViewDTO.WorkOrderPart?.SerialNumber,
                        PurchaseOrderNumber = workOrderHistoryViewDTO.PurchaseOrderNumber,
                        ReferencePO = workOrderHistoryViewDTO.ReferencePO,
                        ScheduledStartDate = workOrderHistoryViewDTO.ScheduledStartDate,
                        ScheduledEndDate = workOrderHistoryViewDTO.ScheduledEndDate,
                        ActualStartDate = workOrderHistoryViewDTO.ActualStartDate,
                        ActualEndDate = workOrderHistoryViewDTO.ActualEndDate,
                        ProductName = workOrderHistoryViewDTO.ProductName,
                        HasNcr = workOrderHistoryViewDTO.HasNcr,
                    });;
                }
            });

            return workOrderGridSummaryViews.ToList();
        }

        private static (int PercentageOfTasksCompletedDenominator, int PercentageOfTasksCompletedNumerator, decimal PercentageOfTasksCompleted,
                       decimal PercentageOfExpectedDurationTimeLoggedNumerator, decimal PercentageOfExpectedDurationTimeLoggedDenominator,
                       decimal PercentageOfExpectedDurationTimeLogged) CalculateWorkOrderProgress(WorkOrderHistoryViewDTO workOrderEntity)
        {
            int[] pctCompletedIds = { 3, 4, 6, 8 };
            int completedDenominator = workOrderEntity.WorkOrderTasks.Count();
            int completedNumerator = workOrderEntity.WorkOrderTasks.Where(x => pctCompletedIds.Contains(x.StatusId)).Count();
            decimal pctComplete = completedDenominator == 0 ? 0 : completedNumerator / (decimal)completedDenominator;
            decimal expectedDurationDenominator = (decimal)workOrderEntity.WorkOrderTasks.Select(x => x.LaborTime).Sum().GetValueOrDefault();
            decimal expectedDurationNumerator = workOrderEntity.WorkOrderTasks.Select(x => x.TotalTaskTime).Sum().GetValueOrDefault();
            decimal percentExpectedDuration = expectedDurationDenominator == 0 ? 0 : expectedDurationNumerator / expectedDurationDenominator;

            return (completedDenominator, completedNumerator, pctComplete, expectedDurationNumerator, expectedDurationDenominator, percentExpectedDuration);
        }

        private static string GetCurrentActiveTaskName(ICollection<WorkOrderHistoryTaskDTO> workOrderTasks)
        {
            var workOrderTaskEntitiesInProgress = workOrderTasks.Where(x => x.StatusId == (int)EnumStatusSteps.InProgress).OrderBy(s => s.TaskStepOrder).ToList();
            return workOrderTaskEntitiesInProgress.Any() ? workOrderTaskEntitiesInProgress.First().Title : string.Empty;
        }

        private static EnumStatusSteps GetWorkOrderStatusFromTasks(ICollection<WorkOrderHistoryTaskDTO> tasks)
        {
            int[] completed = { 3, 6, 8 };
            if (tasks.All(x => completed.Contains(x.StatusId)))
            {
                return EnumStatusSteps.Complete;
            }

            if (tasks.Any(x => (x.StatusId == (int)EnumStatusSteps.InProgress || x.StatusId == (int)EnumStatusSteps.Complete)))
            {
                return EnumStatusSteps.InProgress;
            }

            if (tasks.Any(x => x.StatusId == (int)EnumStatusSteps.Cancelled))
            {
                return EnumStatusSteps.Cancelled;
            }

            return EnumStatusSteps.WaitingtoStart;
        }

        private static string GetWorkOrderDisposition(WorkOrderHistoryViewDTO workOrderModel)
        {
            string dispositionMessage = string.Empty;

            if (workOrderModel.HasNcr)
            {
                var workOrderTaskModels = workOrderModel.WorkOrderTasks.Where(x =>
                    x.ProcedureStepTypeId == Constants.PROCEDURESTEPTYPENC).ToList();
                if (workOrderTaskModels.Any())
                {
                    foreach (var workOrderTaskModel in workOrderTaskModels)
                    {
                        dispositionMessage +=
                            String.Join(
                                " | ",
                                workOrderTaskModel.WorkOrderTaskMonitors
                            ) + " | ";
                    }
                }
            }

            if (workOrderModel.WorkOrderMessages != null &&
                workOrderModel.WorkOrderMessages.Count > 0)
            {
                dispositionMessage += String.Join(string.Empty,
                    workOrderModel.WorkOrderMessages.Select(x =>
                        x.Message == null ? "" : x.Message + " | "
                    ).ToList()
                );
            }
            return dispositionMessage.Trim().Trim('|').Trim();
        }
    }
}
