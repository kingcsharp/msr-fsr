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
using System.Text;
using Newtonsoft.Json;
using MSR.Domain.Models.Query;

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
                        Disposition = string.Join(" | ", workOrderHistoryViewDTO.WorkOrderMessages.Select(x =>x.Message == null ? "" : x.Message)),
                        ProcedureName = workOrderHistoryViewDTO.WorkOrderTasks != null && workOrderHistoryViewDTO.WorkOrderTasks.Any() ? workOrderHistoryViewDTO.WorkOrderTasks.First().ProcedureName : string.Empty,
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
                        HasNcr = workOrderHistoryViewDTO.HasNcr
                    });
                }
            });

            return workOrderGridSummaryViews.ToList();
        }

        public static async Task<ICollection<WorkOrderStatus>> GetWorkOrderStatus(this DbSet<WorkOrderStatusSummary> dbSet, Expression<Func<WorkOrderStatusSummary, dynamic>> projection)
        {
            var workOrderStatusViews = (await QueryHelper.GetViewDataFor<WorkOrderStatusSummary, ICollection<WorkOrderStatus>>(dbSet, projection)).ToList();
            return workOrderStatusViews;
        }

        public static async Task<(ICollection<WorkOrderGridSummary> data, int totalRows)> GetWorkOrderMenu(this DbSet<WorkOrderMenu> dbSet, Expression<Func<WorkOrderMenu,dynamic>> projection, QueryBase filters)
        {
            var pagedData = dbSet.AsQueryable().ToFilterView(filters);
            var pagedList = await pagedData.data.ToListAsync();
            
            return (pagedList.Select(i => AutoMapperHelper.Mapper.Map<WorkOrderGridSummary>(i)).ToList(), pagedData.totalRows);
        }

        public static async Task<(ICollection<PortalWorkOrderView> data, int totalRows)> GetPortalWorkOrderMenu(this DbSet<PortalWorkOrderMenu> dbSet, Expression<Func<PortalWorkOrderMenu, dynamic>> projection, GetPortalWorkOrderQueryModel portalWorkOrderQueryModel)
        {
            var portalWorkOrderViews = new List<PortalWorkOrderView>();
            var pagedData = dbSet.AsQueryable().Where(i => i.CustomerId == portalWorkOrderQueryModel.CustomerId && i.CreatedOn >= portalWorkOrderQueryModel.FromDate && i.CreatedOn <= portalWorkOrderQueryModel.ToDate)
                .ToFilterView(portalWorkOrderQueryModel);

            var pagedList = await pagedData.data.ToListAsync();
            foreach (var view in pagedList)
            {
                var portalWorkOrderView = new PortalWorkOrderView()
                {
                    Id = view.WorkOrderId,
                    WorkOrderId = view.WorkOrderId,
                    SubParts = new List<PortalSubPartView>(),
                    CompanyPartNumber = view.CompanyPartNumber,
                    CustomerId = view.CustomerId,
                    CycleCount = view.CycleCount,
                    Disposition = view.Disposition,
                    DueDate = view.DueDate,
                    HasFiles = view.HasFiles,
                    HasMonitors = view.HasMonitors,
                    HasNCRs = view.HasNCRs,
                    HasPhotos = view.HasPhotos,
                    InvoiceAmount = view.InvoiceAmount,
                    InvoiceDate = view.InvoiceDate,
                    InvoiceName = view.InvoiceName,
                    Messages = new List<WorkOrderMessageModel>(),
                    PartId = view.PartId,
                    PartName = view.PartName,
                    PercentageOfExpectedDurationTimeLogged = view.PercentageOfExpectedDurationTimeLogged,
                    PercentageOfExpectedDurationTimeLoggedDenominator = view.PercentageOfExpectedDurationTimeLoggedDenominator,
                    PercentageOfExpectedDurationTimeLoggedNumerator = view.PercentageOfExpectedDurationTimeLoggedNumerator,
                    PercentageOfTasksCompleted = view.PercentageOfTasksCompleted,
                    PercentageOfTasksCompletedDenominator = view.PercentageOfTasksCompletedDenominator,
                    PercentageOfTasksCompletedNumerator = view.PercentageOfTasksCompletedNumerator,
                    Price = view.Price,
                    ProcedureName = view.ProcedureName,
                    ProductName = view.ProductName,
                    PurchaseOrderNumber = view.PurchaseOrderNumber,
                    Qty = view.Qty,
                    SerialNumber = view.SerialNumber,
                    StartDate = view.StartDate,
                    Status = view.Status
                };

                if (!string.IsNullOrWhiteSpace(view.Disposition))
                {
                    var messages = (from messageItem in view.Disposition.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                    let eachMessage = messageItem.Split(',')
                                    select new WorkOrderMessageModel()
                                    {
                                        Date = DateTime.Parse(eachMessage[1]),
                                        Name = eachMessage[0],
                                        Message = eachMessage[2]
                                    }).ToList();

                    portalWorkOrderView.Messages = messages;
                }
                var subParts = pagedList.First(i => i.WorkOrderId == view.WorkOrderId).SubParts;
                if (!string.IsNullOrWhiteSpace(subParts))
                {
                    var subPartModels = JsonConvert.DeserializeObject<List<PortalSubPartView>>(subParts);
                    portalWorkOrderView.SubParts = subPartModels;
                }
                portalWorkOrderViews.Add(portalWorkOrderView);
            }

            return (portalWorkOrderViews.ToList(), pagedData.totalRows);
        }

        private static (int PercentageOfTasksCompletedDenominator, int PercentageOfTasksCompletedNumerator, decimal PercentageOfTasksCompleted,decimal PercentageOfExpectedDurationTimeLoggedNumerator, 
                        decimal PercentageOfExpectedDurationTimeLoggedDenominator, decimal PercentageOfExpectedDurationTimeLogged) CalculateWorkOrderProgress(WorkOrderHistoryViewDTO workOrderEntity)
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

        private static string GetCurrentActiveTaskName(ICollection<WorkOrderTaskDTO> workOrderTasks)
        {
            var workOrderTaskEntitiesInProgress = workOrderTasks.Where(x => x.StatusId == (int)EnumStatusSteps.InProgress).OrderBy(s => s.TaskStepOrder).ToList();
            return workOrderTaskEntitiesInProgress.Any() ? workOrderTaskEntitiesInProgress.First().Title : string.Empty;
        }

        private static EnumStatusSteps GetWorkOrderStatusFromTasks(ICollection<WorkOrderTaskDTO> tasks)
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
            StringBuilder dispositionMessage = new StringBuilder();

            if (workOrderModel.HasNcr)
            {
                var workOrderTaskModels = workOrderModel.WorkOrderTasks.Where(x =>
                    x.ProcedureStepTypeId == Constants.PROCEDURESTEPTYPENC).ToList();
                if (workOrderTaskModels.Any())
                {
                    dispositionMessage.Append(string.Join(
                        " | ",
                        workOrderTaskModels.SelectMany(x => x.WorkOrderTaskMonitors)))
                        .Append(" | ");
                }
            }

            if (workOrderModel.WorkOrderMessages != null &&
                workOrderModel.WorkOrderMessages.Count > 0)
            {
                dispositionMessage.Append(
                    string.Join(
                        " | ",
                        workOrderModel.WorkOrderMessages.Select(x =>
                            x.Message == null ? "" : x.Message)
                    ));
            }
            return dispositionMessage.ToString().Trim().Trim('|').Trim();
        }
    }
}