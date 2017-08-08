using Msr.Models.Regions;
using Msr.Services.jqGrid;
using Msr.Services.Regions;
using Msr.Services.Regions.ViewModels;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class RegionsController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Files";

            return View(viewModel);
        }

        public ActionResult RegionsData(JqGridParam param)
        {
            var regionService = new RegionService();

            var totalRows = regionService.RegionsQueryable;

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(RegionsView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(RegionsView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(RegionsView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
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

        public ActionResult Create()
        {
            var part = new SaveRegionViewModel();

            return View(part);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveRegionViewModel model)
        {
            var regionService = new RegionService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = regionService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Region has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    return View(model);
                }
            }

            return View(model);

        }

        public ActionResult Edit(string id)
        {
            var regionService = new RegionService();

            var model = regionService.GetById(id);

            var region = new SaveRegionViewModel();

            region = region.MapToDto(model);

            return View(region);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveRegionViewModel model)
        {
            var regionService = new RegionService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = regionService.Save(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Region has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var regionService = new RegionService();

            if (!string.IsNullOrWhiteSpace(id))
            {


                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}