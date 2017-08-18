using Msr.Models;
using Msr.Services.Documents;
using Msr.Services.jqGrid;
using Msr.Services.PrePro;
using Msr.Services.PrePro.ViewModel;
using Msr.Services.ProcedureTypes;
using Msr.Services.TheoryParagraph;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.PrePro;
using Msr.Services.ProcedureVerbs;

namespace Answer.Web.Controllers
{
    public class PreProSearchController : Controller
    {
        // GET: PreProSearch
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "PreProSearch";

            return View(viewModel);
        }

        public ActionResult PreProData(JqGridParam param)
        {
            var preproService = new PreProServices();

            var totalRows = preproService.GetPreProQueryable().Where(x=>x.Status!="DELETED");

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PrePropSearchView.ObjId))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(PrePropSearchView.ObjId))
                    {
                        totalRows = totalRows.Where(x => x.ObjId.ToLower().Contains(rule.data.ToLower()));
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

        public ActionResult PreProDelete(string id)
        {
            var taskService = new PreProServices();

            var response = taskService.Delete(id: id);

            if (response)
            {
                TempData["SuccessMessage"] = "PrePro deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var part = new ProcedurePreProViewModel();

            part.Setup(new PreProServices());

            return View(part);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ProcedurePreProViewModel model)
        {
            var taskService = new PreProServices();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";
                //  model.SubParts = null;

                var response = taskService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Step has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new PreProServices());

                    return View(model);
                }

            }

            model.Setup(new PreProServices());

            return View(model);
        }


        public ActionResult Details(string id)
        {
            var taskService = new PreProServices();

            var model = taskService.GetById(id);

            var part = new ProcedurePreProViewModel();

            part = part.MapToDto(model);

            part.Setup(new PreProServices());

            return View(part);
        }

        public ActionResult Edit(string id)
        {
            var preProServices = new PreProServices();

            var model = preProServices.GetById(id);

            var procedurePreProView = new ProcedurePreProViewModel();

            procedurePreProView = procedurePreProView.MapToDto(model);

            procedurePreProView.Setup(new PreProServices());

            procedurePreProView.Labor = "No Labour Assigned";
            procedurePreProView.ApplicationObjects = "Object Description";

            return View(procedurePreProView);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ProcedurePreProViewModel model)
        {
            var preProServices = new PreProServices();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = preProServices.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Step has been Updated successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new PreProServices());

                    return View(model);
                }

            }

            model.Setup(new PreProServices());

            return View(model);
        }
    }
}