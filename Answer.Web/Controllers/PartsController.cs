using Msr.Models.Parts;
using Msr.Models.Tasks;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Parts;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class PartsController : Controller
    {
        // GET: Parts
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Parts";

            return View(viewModel);
        }
        public ActionResult PartsData(JqGridParam param)
        {
            var taskService = new PartsService();

            var totalRows = taskService.GetPartsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PartsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PartsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.CompanyPartNumber))
                    {
                        totalRows = totalRows.Where(x => x.CompanyPartNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.CompanyName))
                    {
                        totalRows = totalRows.Where(x => x.CompanyName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartsView.Revision))
                    {
                        var rev = Convert.ToInt32(rule.data);
                        totalRows = totalRows.Where(x => x.Revision == rev);
                    }
                    else if (rule.field == nameof(PartsView.ToString))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(PartsView.CompanyPartNumber);
            var orderDirection = "asc";

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

        public ActionResult PartTypes()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Parts";

            return View(viewModel);
        }

        public ActionResult PartTypesData(JqGridParam param)
        {
            var taskService = new PartTypeService();

            var totalRows = taskService.GetPartTypesQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PartTypesView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PartTypesView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Spare))
                    {
                        totalRows = totalRows.Where(x => x.Spare.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Consumable))
                    {
                        totalRows = totalRows.Where(x => x.Consumable.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(PartTypesView.Id);
            var orderDirection = "asc";

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
        public ActionResult AddPartTypes()
        {
            var addModel = new AddPartTypesViewModel();

            addModel.Setup();

            return View(addModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPartTypes(AddPartTypesViewModel parttypes)
        {
            var partservice = new PartTypeService();

            //need to de dynamic
            parttypes.NTLogin = "1618";

            partservice.Create(parttypes);

            return RedirectToAction("PartTypes");
        }
        public ActionResult SavePartTypes(string Id)
        {
            var partservice = new PartTypeService();

            var model = partservice.GetById(Id: Id);

            var partype = new AddPartTypesViewModel();
            partype = partype.MapToDto(model);
            partype.Setup();

            return View(partype);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SavePartTypes(AddPartTypesViewModel parttypes)
        {
            var partservice = new PartTypeService();

            //need to de dynamic
            parttypes.NTLogin = "1618";

            partservice.Edit(parttypes);

            return RedirectToAction("PartTypes");
        }
    }
}