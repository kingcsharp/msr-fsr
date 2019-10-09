using Msr.Models.Tasks;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;
using Msr.Services.Orders.Procedures;
using Msr.Services.Tasks.Messaging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Orders;

namespace Msr.Services.Orders
{
    public class TaskService
    {
        private readonly MsrDbContext _dbContext;

        public TaskService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<MonitorResult> GetTaskWithMonitors()
        {
            return _dbContext.MonitorResults;
        }

        public int? CheckHasMonitors(string fillId)
        {
            var fileIdParm = new SqlParameter("@fillID", fillId);

            var tasks = _dbContext.Database.SqlQuery<GetTaskWithMonitorsResult>("Portal_GetTaskWithMonitors @fillID", fileIdParm).ToList();

            if (tasks.Any())
            {
                return 1;
            }

            return null;
        }

        public int CheckHasNcr(string partId)
        {
            var fileIdParm = new SqlParameter("@partId", partId);

            var result = _dbContext.Database.SqlQuery<int>("PortalHasNcr @partId", fileIdParm).Single();

            return result;
        }

        public MonitorHistoryResponse GetTaskWithMonitors(string fileId, string ntlogin)
        {
            MonitorHistoryResponse monitorHistoryResponse = new MonitorHistoryResponse();

            List<TaskStepResult> parentAndNCRTaskStepResults = GetParentandNCRTasks(Convert.ToInt32(fileId), ntlogin);

            FileSearchView fileSearch = _dbContext.FileSearchView.Single(x => x.Id == fileId);

            monitorHistoryResponse.SupName = fileSearch.SupName;
            monitorHistoryResponse.FillObjDesc = fileSearch.FillObjDesc;
            monitorHistoryResponse.PurchItemId = fileSearch.PurchItemId;

            var fileIdParm = new SqlParameter("@fillID", fileId);

            var tasks = _dbContext.Database.SqlQuery<GetTaskWithMonitorsResult>("Portal_GetTaskWithMonitors @fillID", fileIdParm).ToList();

            if (parentAndNCRTaskStepResults.Any())
            {
                foreach (TaskStepResult task in parentAndNCRTaskStepResults)
                {
                    var monitorItem = new MonitorItem();

                    monitorItem.TaskId = task.TaskId;
                    monitorItem.TaskTitle = task.Title;
                    monitorItem.Description = task.Description;
                    monitorItem.IsNCRTask = task.IsNCRTask;

                    if (String.IsNullOrWhiteSpace(monitorItem.TaskId) == false)
                    {
                        var monitors = _dbContext.MonitorResults.Where(x => x.TaskId == task.TaskId).ToList();

                        monitorItem.MonitorResults.AddRange(monitors);
                    }
                    else
                    {
                        monitorItem.MonitorResults = new List<MonitorResult>();
                    }

                    monitorHistoryResponse.MonitorItem.Add(monitorItem);
                }
            }

            //if (tasks.Any())
            //{
            //    foreach (GetTaskWithMonitorsResult task in tasks)
            //    {
            //        var item = new MonitorItem();

            //        item.TaskId = task.TaskId;
            //        item.TaskTitle = task.TaskTitle;
            //        item.Description = task.Description;

            //        var monitors = _dbContext.MonitorResults.Where(x => x.TaskId == task.TaskId).ToList();

            //        item.MonitorResults.AddRange(monitors);

            //        monitorHistoryResponse.MonitorItem.Add(item);
            //    }
            //}

            return monitorHistoryResponse;
        }

        private List<TaskStepResult> GetParentandNCRTasks(int fillID, string ntLogin)
        {
            List<TaskStepResult> parentAndNCRTaskStepResults = new List<TaskStepResult>();

            OrderService orderService = new OrderService();

            WorkOrderDetailsResponse workOrderDetailsResponse = orderService.GetPurchaseItemDetails(fillID, ntLogin);

            List<TaskStepResult> taskStepResults = workOrderDetailsResponse.TaskStepResults;

            TaskStepResult previousTaskStepResult = null;

            foreach (TaskStepResult taskStepResult in taskStepResults)
            {

                if (taskStepResult.IsNCRTask == true)
                {
                    if (previousTaskStepResult.IsNCRTask == false)
                    {
                        parentAndNCRTaskStepResults.Add(previousTaskStepResult);
                    }

                    parentAndNCRTaskStepResults.Add(taskStepResult);
                }

                previousTaskStepResult = taskStepResult;

            }

            return parentAndNCRTaskStepResults;
        }

        public List<SelectListItem> GetMonitors(string input, int reportTypeId, int reportType)
        {
            var fillIds = new List<string>();
            var tasks = new List<GetTaskWithMonitorsResult>();
            var monitorList = new List<SelectListItem>();

            if (reportTypeId == 1)
            {
                var workItem = _dbContext.WorkOrders.FirstOrDefault(x => x.ActualPartId == input && x.ActualStopDate.HasValue);

                if (workItem != null)
                {
                    fillIds.Add(workItem.FillId);
                }
            }
            else if (reportTypeId == 2)
            {
                var workItems = _dbContext.WorkOrders.Where(x => x.Serial == input).Distinct();

                if (workItems != null)
                {
                    fillIds = workItems.Select(x => x.FillId).ToList();
                }
            }

            foreach (var fillId in fillIds)
            {
                var fileIdParm = new SqlParameter("@fillID", fillId);
                var fillIdtasks = _dbContext.Database.SqlQuery<GetTaskWithMonitorsResult>("Portal_GetTaskWithMonitors @fillID", fileIdParm).ToList();

                if (reportType == ReportTypeConstants.Graph)
                {
                    tasks.AddRange(fillIdtasks.Where(x => x.MonitorType == "NUMBER").ToList());
                }
                else
                {
                    tasks.AddRange(fillIdtasks.Where(x => x.MonitorType != "NUMBER").ToList());
                }
            }

            var groupedTasks = tasks.GroupBy(x => x.Description).ToList();

            foreach (var groupedTask in groupedTasks)
            {
                monitorList.Add(new SelectListItem
                {
                    Text = groupedTask.Key,
                    Value = String.Join(",", groupedTask.Select(t => t.TaskId))
                });
            }

            return monitorList;
        }

        public List<MonitorsWithTaskAndResult> GetReports(string serial, DateTime fromDate, DateTime toDate, int reportTypeId, string taskIds)
        {
            var taskList = taskIds.Split(',');

            var query = _dbContext.MonitorsWithTaskAndResults.AsQueryable();

            query = query.Where(x => taskList.Contains(x.TaskId) && x.TaskStopDate >= fromDate && x.TaskStopDate <= toDate);

            if (reportTypeId == ReportTypeConstants.Graph)
            {
                query = query.Where(x => x.MonitorType == "NUMBER");
            }
            else
            {
                query = query.Where(x => x.MonitorType != "NUMBER");
            }

            return query.ToList();
        }

    }
}
