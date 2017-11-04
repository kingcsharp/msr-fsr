using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.jqGrid;
using Msr.Services.Procedures;
using Msr.Services.Procedures.ViewModels;

namespace Answer.Web.Controllers
{
    public class ProcedureMonitorsController : BaseController
    {
        public ActionResult Monitors()
        {
            return View();
        }

        public ActionResult ProcedureMonitorsData(JqGridParam param, string objectId)
        {
            var procedureService = new ProceduresService();

            var model = new GetMoniterViewModel();

            model.Related_Object_Id = objectId;

            model.StrNTLogin = GetCurrentUser().Id;

            bool reorderByLatestMonitor = false;

            var totalRows = procedureService.GetMonitors(model, reorderByLatestMonitor);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(GetMoniterViewModel.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(GetMoniterViewModel.Monitor_Type))
                    {
                        totalRows = totalRows.Where(x => x.Monitor_Type.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(GetMoniterViewModel.Should_Be))
                    {
                        totalRows = totalRows.Where(x => x.Should_Be.ToLower().Contains(rule.data.ToLower()));
                    }

                }
            }

            var orderBy = nameof(GetMoniterViewModel.MonitorNumber);

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

        public ActionResult Add(string id)
        {
            var model = new AddMonitorForProcedureViewModel();

            model.Related_Object_Id = id;

            model.Setup();

            return View(model);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Add(AddMonitorForProcedureViewModel model)
        {
            var proceduresService = new ProceduresService();
            if (ModelState.IsValid)
            {
                model.Id = null;
                model.StrNTLogin = GetCurrentUser().Id;
                var response = proceduresService.AddMonitorForProcedure(model: model);
                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Monitor has been added successfully.";

                    return RedirectToAction(actionName: "Monitors", routeValues: new { id = model.Related_Object_Id });
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return View(model);
            }

            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var addMonitorForProcedureViewModel = new AddMonitorForProcedureViewModel();

            var taskService = new ProceduresService();

            var model = taskService.GetMonitorById(id, GetCurrentUser().Id);

            addMonitorForProcedureViewModel = addMonitorForProcedureViewModel.MapToDto(model);

            addMonitorForProcedureViewModel.Setup();

            return View(addMonitorForProcedureViewModel);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Edit(AddMonitorForProcedureViewModel model)
        {
            var proceduresService = new ProceduresService();
            if (ModelState.IsValid)
            {
                model.StrNTLogin = GetCurrentUser().Id;
                var response = proceduresService.AddMonitorForProcedure(model: model);
                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Monitor has been updated successfully.";

                    return RedirectToAction(actionName: "Monitors", routeValues: new { id = model.Related_Object_Id });
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return View(model);
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            string[] ar = id.Split(',');

            id = ar[0];

            string objId = ar[1];

            var proceduresService = new ProceduresService();

            var response = proceduresService.DeleteMonitor(id);

            if (response)
            {
                TempData["SuccessMessage"] = "Monitor has been deleted successfully.";

                return RedirectToAction(actionName: "Monitors", routeValues: new { id = objId });
            }

            TempData["ErrorMessage"] = "Something went wrong.";

            return RedirectToAction(actionName: "Monitors", routeValues: new { id = objId });
        }

        public ActionResult Reorder(GetMoniterViewModel model)
        {
            var proceduresService = new ProceduresService();

            proceduresService.UpdatePrintOrder(model.Id, model.MonitorNumber.ToString());

            return RedirectToAction(actionName: "Monitors", routeValues: new { id = ViewBag.ObjectId });

        }

    }
}