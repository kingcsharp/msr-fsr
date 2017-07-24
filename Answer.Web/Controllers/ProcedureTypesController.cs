using Msr.Services.jqGrid;
using Msr.Services.ProcedureTypes;
using Msr.Services.ProcedureTypes.ViewModels;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Procedure;

namespace Answer.Web.Controllers
{
    public class ProcedureTypesController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "ProcedureTypes";

            return View(viewModel);
        }

        public ActionResult ProceduresData(JqGridParam param)
        {
            var procedureTypesService = new ProcedureTypesService();

            var totalRows = procedureTypesService.GetProceduresTypesQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureTypesView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureTypesView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureTypesView.VerbTypeName))
                    {
                        totalRows = totalRows.Where(x => x.VerbTypeName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureTypesView.Revision))
                    {
                        var rev = Convert.ToInt32(rule.data);
                        totalRows = totalRows.Where(x => x.Revision == rev);
                    }
                    else if (rule.field == nameof(ProcedureTypesView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ProcedureTypesView.Id);
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

        public ActionResult Create()
        {
            var saveProcedureViewModel = new SaveProcedureTypesViewModel();

            var NTLogin = "1618";
            saveProcedureViewModel.Setup(new ProcedureTypesService(), NTLogin);

            return View(saveProcedureViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveProcedureTypesViewModel model)
        {
            var procedureService = new ProcedureTypesService();

            var ntLogin = "1618";

            if (ModelState.IsValid)
            {
                var response = procedureService.Save(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Verb Type has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProcedureTypesService(), ntLogin);

                    return View(model);
                }
            }


            model.Setup(new ProcedureTypesService(), ntLogin);

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var saveProcedureViewModel = new SaveProcedureTypesViewModel();
            var procedureService = new ProcedureTypesService();

            var model = procedureService.GetVerbTypeById(id);

            var NTLogin = "1618";

            saveProcedureViewModel = saveProcedureViewModel.MapToDto(model);
            saveProcedureViewModel.Setup(new ProcedureTypesService(), NTLogin);

            return View(saveProcedureViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveProcedureTypesViewModel model)
        {
            var procedureService = new ProcedureTypesService();

            var ntLogin = "1618";

            if (ModelState.IsValid)
            {
                var response = procedureService.Edit(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Verb Type has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProcedureTypesService(), ntLogin);

                    return View(model);
                }
            }


            model.Setup(new ProcedureTypesService(), ntLogin);

            return View(model);
        }
    }
}