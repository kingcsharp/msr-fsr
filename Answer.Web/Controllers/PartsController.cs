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
using Msr.Services.Parts.ViewModels;
using Msr.Services.Files;

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
                    if (rule.field == nameof(PartsView.ObjId))
                    {
                        totalRows = totalRows.Where(x => x.ObjId == rule.data.ToLower());
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
                    else if (rule.field == nameof(PartsView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower() == rule.data.ToLower());
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
        public ActionResult Details(string id)
        {
            var taskService = new PartsService();

            var model = taskService.GetById(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(new FileService(), new PartsService());

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

            part.Setup(new FileService(), new PartsService());

            return View(part);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPart(AddPartViewModel model)
        {
            var taskService = new PartsService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.Company = "2";
                //need to be from list 
                model.ProductType = "SERVICE";
                model.NTLogin = "1618";

                var response = taskService.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Part has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new FileService(), new PartsService());

                    return View(model);
                }

            }

            model.Setup(new FileService(), new PartsService());

            return View(model);

        }
        public ActionResult Edit(string id)
        {
            var taskService = new PartsService();

            var model = taskService.GetById(id);

            var part = new AddPartViewModel();

            part = part.MapToDto(model);

            part.Setup(new FileService(), new PartsService());

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AddPartViewModel model)
        {
            var taskService = new PartsService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.Company = "2";
                //need to be from list 
                model.ProductType = "SERVICE";
                model.NTLogin = "1618";

                var response = taskService.Save(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Part has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(new FileService(), new PartsService());

                return View(model);
            }

            model.Setup(new FileService(), new PartsService());

            return View(model);
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
                    else if (rule.field == nameof(PartTypesView.Unit))
                    {
                        totalRows = totalRows.Where(x => x.Unit.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Rev))
                    {
                        var rev = Convert.ToInt32(rule.data);
                        totalRows = totalRows.Where(x => x.Rev == rev);
                    }
                    else if (rule.field == nameof(PartTypesView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
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
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult AddPartTypes(AddPartTypesViewModel parttype)
        {
            var partTypeservice = new PartTypeService();
            if (ModelState.IsValid)
            {
                //need to de dynamic
                parttype.NTLogin = "1618";

                var response = partTypeservice.Create(parttype);

                if (response)
                {
                    TempData["SuccessMessage"] = "PartType has been created successfully.";

                    return RedirectToAction("PartTypes");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    parttype.Setup();

                    return View(parttype);
                }
            }

            parttype.Setup();

            return View(parttype);
        }
        public ActionResult SavePartTypes(string Id)
        {
            var partTypeservice = new PartTypeService();

            var model = partTypeservice.GetById(Id: Id);

            var parttype = new AddPartTypesViewModel();
            parttype = parttype.MapToDto(model);
            parttype.Setup();

            return View(parttype);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SavePartTypes(AddPartTypesViewModel parttype)
        {
            var partTypeservice = new PartTypeService();
            if (ModelState.IsValid)
            {
                //need to de dynamic
                parttype.NTLogin = "1618";

                var response = partTypeservice.Edit(parttype);
                if (response)
                {
                    TempData["SuccessMessage"] = "PartType has been edited successfully.";

                    return RedirectToAction("PartTypes");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    parttype.Setup();

                    return View(parttype);
                }
            }

            parttype.Setup();

            return View(parttype);
        }
    }
}