using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Training;
using Msr.Services.jqGrid;
using Msr.Services.Orders;

namespace Answer.Web.Controllers
{
    public class TrainingController : Controller
    {
        private readonly PeopleService _peopleService;

        public TrainingController()
        {
            _peopleService = new PeopleService();
        }
        // GET: Training
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TrainingData(JqGridParam param)
        {
            var totalRows = _peopleService.GetTrainingQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(TrainingView.FullName))
                    {
                        totalRows = totalRows.Where(x => x.FullName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(TrainingView.PositionName))
                    {
                        totalRows = totalRows.Where(x => x.PositionName.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(TrainingView.EndDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.EndDate.Day == value.Day &&
                                                             q.EndDate.Month == value.Month && q.EndDate.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(TrainingView.StartDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.StartDate.Day == value.Day &&
                                                             q.StartDate.Month == value.Month && q.StartDate.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(TrainingView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower() == rule.data.ToLower());
                    }

                }
            }
            var orderBy = param.sortColumn;

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