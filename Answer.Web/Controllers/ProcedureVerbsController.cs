using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;
using Msr.Models.ProcedureVerbs;
using Msr.Services.ProcedureVerbs;
using Msr.Services.ProcedureVerbs.ViewModels;
using Msr.Services.Workflows;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Procedures)]
    public class ProcedureVerbsController : BaseController
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "ProcedureVerbs";

            return View(viewModel);
        }

        public ActionResult ProcedureVerbsData(JqGridParam param)
        {
            var procedureTypesService = new ProcedureVerbsService();

            var totalRows = procedureTypesService.GetProceduresVerbs();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureVerbsView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureVerbsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureVerbsView.VerbTypeName))
                    {
                        totalRows = totalRows.Where(x => x.VerbTypeName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureVerbsView.Revision))
                    {
                        var val = -1;

                        int.TryParse(rule.data.ToLower(), out val);

                        totalRows = totalRows.Where(x => x.Revision == val);
                    }
                    else if (rule.field == nameof(ProcedureVerbsView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }

            var orderBy = nameof(ProcedureVerbsView.Id);

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
            var saveProcedureVerbsViewModel = new SaveProcedureVerbsViewModel();

            saveProcedureVerbsViewModel.Setup(new ProcedureVerbsService());

            return View(saveProcedureVerbsViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveProcedureVerbsViewModel model)
        {
            var procedureVerbsService = new ProcedureVerbsService();

            model.NTLogin = GetCurrentUser().Id;

            if (ModelState.IsValid)
            {
                var response = procedureVerbsService.Save(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Type has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProcedureVerbsService());

                    return View(model);
                }
            }


            model.Setup(new ProcedureVerbsService());

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var currrentUser = GetCurrentUser();
            var workflowService = new WorkflowService();
            var checkoutEntity = workflowService.CheckOutObject(id, currrentUser.Id);

            var saveProcedureVerbsViewModel = new SaveProcedureVerbsViewModel();
            var procedureVerbsService = new ProcedureVerbsService();

            var model = procedureVerbsService.GetVerbTypeById(checkoutEntity.Entity);

            saveProcedureVerbsViewModel = saveProcedureVerbsViewModel.MapToDto(model);
            saveProcedureVerbsViewModel.Setup(new ProcedureVerbsService());

            return View(saveProcedureVerbsViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveProcedureVerbsViewModel model)
        {
            var procedureService = new ProcedureVerbsService();

            model.NTLogin = GetCurrentUser().Id;

            if (ModelState.IsValid)
            {
                var response = procedureService.Edit(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Type has been updated successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProcedureVerbsService());

                    return View(model);
                }
            }


            model.Setup(new ProcedureVerbsService());

            return View(model);
        }
        public ActionResult Delete(string id,string ntlogin)
        {
            var procedureVerbsService = new ProcedureVerbsService();

            var response = procedureVerbsService.Delete(id: id,ntlogin:ntlogin);

            if (response)
            {
                TempData["SuccessMessage"] = "Procedure Type deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
    }
}