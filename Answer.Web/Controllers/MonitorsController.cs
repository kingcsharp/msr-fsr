using Msr.Models.Monitor;
using Msr.Services.jqGrid;
using Msr.Services.monitor;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class MonitorsController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Monitors";

            return View(viewModel);
        }

        public ActionResult MonitorsData(JqGridParam param)
        {
            var monitorService = new MonitorService();

            var totalRows = monitorService.GetLocationsQueryable();

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