using Msr.Models.Monitor;
using Msr.Services.jqGrid;
using Msr.Services.monitor;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class MonitorsController : BaseController
    {
        private readonly MonitorService _monitorService;

        public MonitorsController()
        {
            _monitorService = new MonitorService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Monitors";

            return View(viewModel);
        }

        public ActionResult MonitorsData(JqGridParam param)
        {
            var defaultStatusList = "FINISHED,CLOSED".Split(',');

            var totalRows = _monitorService.GetMonitorsQueryable().Where(x => defaultStatusList.Contains(x.taskStatus));

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(MonitorView.RollUpId))
                    {
                        totalRows = totalRows.Where(x => x.RollUpId == rule.data);
                    }
                    else if (rule.field == nameof(MonitorView.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(MonitorView.MonitorType))
                    {
                        totalRows = totalRows.Where(x => x.MonitorType.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(MonitorView.PrintResult))
                    {
                        totalRows = totalRows.Where(x => x.PrintResult.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(MonitorView.IsPassing))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.IsPassing == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.IsPassing.ToString().ToLower() == rule.data.ToLower());
                        }
                    }
                    else if (rule.field == nameof(MonitorView.WorkerName))
                    {
                        totalRows = totalRows.Where(x => x.WorkerName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(MonitorView.TaskStopDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.TaskStopDate.HasValue && q.TaskStopDate.Value.Day == value.Day &&
                                                             q.TaskStopDate.Value.Month == value.Month && q.TaskStopDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(MonitorView.ActualPartsApprovedDataSerial))
                    {
                        totalRows = totalRows.Where(x => x.ActualPartsApprovedDataSerial.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderBy = param.sortColumn;

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }

            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(orderBy);
            }
            else
            {
                totalRows = totalRows.OrderBy(orderBy);
            }

            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}