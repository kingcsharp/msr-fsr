using Msr.Services.jqGrid;
using Msr.Services.PrePro;
using Msr.Services.PrePro.ViewModel;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.PrePro;

namespace Answer.Web.Controllers
{
    public class PreProSearchController : BaseController
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
                    if (rule.field == nameof(PrePropSearchView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(PrePropSearchView.ObjId))
                    {
                        totalRows = totalRows.Where(x => x.ObjId.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PrePropSearchView.StepText))
                    {
                        totalRows = totalRows.Where(x => x.StepText.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PrePropSearchView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(PrePropSearchView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(PrePropSearchView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
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

        public ActionResult PreProDelete(string id,string ntlogin)
        {
            var taskService = new PreProServices();

            var response = taskService.Delete(id: id,ntlogin:ntlogin);

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
            var model = new ProcedurePreProViewModel();

            model.CreatingCo = GetCurrentUser().Company;

            model.Setup(new PreProServices(), GetCurrentUser().Id);

            return View(model);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(ProcedurePreProViewModel model)
        {
            var taskService = new PreProServices();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
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

                    model.Setup(new PreProServices(), GetCurrentUser().Id);

                    return View(model);
                }

            }

            model.Setup(new PreProServices(), GetCurrentUser().Id);

            return View(model);
        }


        public ActionResult Details(string id)
        {
            var taskService = new PreProServices();

            var model = taskService.GetById(id);

            var part = new ProcedurePreProViewModel();

            part = part.MapToDto(model);

            part.Setup(new PreProServices(), GetCurrentUser().Id);

            return View(part);
        }

        public ActionResult Edit(string id)
        {
            var preProServices = new PreProServices();

            var model = preProServices.GetById(id);

            var procedurePreProView = new ProcedurePreProViewModel();

            procedurePreProView = procedurePreProView.MapToDto(model);

            procedurePreProView.CreatingCo = GetCurrentUser().Company;

            procedurePreProView.Setup(new PreProServices(), GetCurrentUser().Id);

            procedurePreProView.Labor = "No Labour Assigned";
            procedurePreProView.ApplicationObjects = "Object Description";

            return View(procedurePreProView);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(ProcedurePreProViewModel model)
        {
            var preProServices = new PreProServices();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = preProServices.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Step has been Updated successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new PreProServices(), GetCurrentUser().Id);

                    return View(model);
                }

            }

            model.Setup(new PreProServices(), GetCurrentUser().Id);

            return View(model);
        }
    }
}