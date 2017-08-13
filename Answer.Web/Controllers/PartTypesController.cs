using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Parts;
using Msr.Services.jqGrid;
using Msr.Services.PartTypes;
using Msr.Services.PartTypes.ViewModels;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    public class PartTypesController : Controller
    {
        public ActionResult Index()
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
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }

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
            var addModel = new AddPartTypesViewModel();

            addModel.Setup();

            return View(addModel);
        }
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(AddPartTypesViewModel parttype)
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


                    return RedirectToAction("Index");
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
        public ActionResult Edit(string id)
        {
            var partTypeservice = new PartTypeService();

            var model = partTypeservice.GetById(id: id);

            var parttype = new AddPartTypesViewModel();
            parttype = parttype.MapToDto(model);
            parttype.Setup();

            return View(parttype);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AddPartTypesViewModel parttype)
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

                    return RedirectToAction("Index");
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


        public ActionResult Delete(string id)
        {
            var taskService = new PartTypeService();

            var response = taskService.Delete(id: id);

            if (response)
            {
                TempData["SuccessMessage"] = "Part Type deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
    }
}