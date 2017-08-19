using Msr.Models.Parts;
using Msr.Services.jqGrid;
using Msr.Services.Parts;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Documents;
using Msr.Services.Parts.ViewModels;
using Msr.Services.PartTypes;

namespace Answer.Web.Controllers
{
    [Authorize]
    public class PartsController : BaseController
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

            var defaultStatusList = new[] {"CREATING", "DENIED", "APPROVED", "APPROVED_BUT_REVISING", "APPROVED_BUT_DELETING"};

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status) && x.CompanyPartNumber != null);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PartsView.ObjectId))
                    {
                        totalRows = totalRows.Where(x => x.ObjectId == rule.data.ToLower());
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
                    else if (rule.field == nameof(PartsView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }                        
                    }
                    else if (rule.field == nameof(PartsView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }

            var orderBy = nameof(PartsView.CompanyPartNumber);

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
        public ActionResult Details(string id)
        {
            var taskService = new PartsService();

            var model = taskService.GetById(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

            return View(part);
        }
        public ActionResult ViewFile(string callBackitem)
        {
            var callBackUrl = "http://docs.google.com/gview?url=" + "http://infolab.stanford.edu/pub/papers/google.pdf&embedded=true";//callBackitem url need to be dynamic
            ViewBag.callBackitem = callBackUrl;

            return PartialView("_ViewFile");
        }
        public ActionResult AddPart()
        {
            var part = new AddPartViewModel();

            part.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

            return View(part);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPart(AddPartViewModel model)
        {
            var taskService = new PartsService();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
                model.SubParts = null;

                var response = taskService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Part has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

                    return View(model);
                }

            }

            model.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

            return View(model);

        }
        public ActionResult Edit(string id)
        {
            var taskService = new PartsService();

            var model = taskService.GetById(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AddPartViewModel model)
        {
            var taskService = new PartsService();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
                model.SubParts = null;

                var response = taskService.Save(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Part has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

                return View(model);
            }

            model.Setup(new DocumentFilesService(), new PartsService(), new PartTypeService());

            return View(model);
        }
        public ActionResult PartDelete(string id)
        {
            var taskService = new PartsService();

            var response = taskService.Delete(id: id);

            if (response)
            {
                TempData["SuccessMessage"] = "Part deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
      
    }
}