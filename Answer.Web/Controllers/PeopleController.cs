using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.People;
using Msr.Services.Orders;

namespace Answer.Web.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "People";

            return View(viewModel);
        }      

        public ActionResult PeopleData(JqGridParam param)
        {
            var peopleService = new PeopleService();

            var totalRows = peopleService.GetPeople();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PeopleObjectView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PeopleObjectView.FirstName))
                    {
                        totalRows = totalRows.Where(x => x.FirstName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.LastName))
                    {
                        totalRows = totalRows.Where(x => x.LastName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.PicRecord))
                    {
                        totalRows = totalRows.Where(x => x.PicRecord.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.PositionName))
                    {
                        totalRows = totalRows.Where(x => x.PositionName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.BossName))
                    {
                        totalRows = totalRows.Where(x => x.BossName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.RootCoName))
                    {
                        totalRows = totalRows.Where(x => x.RootCoName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.CompanyName))
                    {
                        totalRows = totalRows.Where(x => x.CompanyName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.LocationName))
                    {
                        totalRows = totalRows.Where(x => x.LocationName.ToLower().Contains(rule.data.ToLower()));
                    }                    
                    else if (rule.field == nameof(PeopleObjectView.PrimaryPhoneNumber))
                    {
                        totalRows = totalRows.Where(x => x.PrimaryPhoneNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.SecondaryPhoneNumber))
                    {
                        totalRows = totalRows.Where(x => x.SecondaryPhoneNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.WorkEmailAddress))
                    {
                        totalRows = totalRows.Where(x => x.WorkEmailAddress.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.DateHired))
                    {
                        totalRows = totalRows.Where(x => x.DateHired.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.SystemStatus))
                    {
                        totalRows = totalRows.Where(x => x.SystemStatus.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PeopleObjectView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(PeopleObjectView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }                  

                }
            }
            var orderBy = nameof(PeopleObjectView.FirstName);
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
        public ActionResult Add()
        {
            return View();
        }
       
    }
}