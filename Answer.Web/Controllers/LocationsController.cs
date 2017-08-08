using Msr.Models.Locations;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Locations.ViewModels;
using Msr.Services.Regions;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class LocationsController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Locations";

            return View(viewModel);
        }

        public ActionResult GetLocations(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            return PartialView("_Locations");
        }
             
        public ActionResult LocationsData(JqGridParam param)
        {
            var locationService = new LocationService();

            var totalRows = locationService.GetLocationsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(LocationView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(LocationView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
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
            var location = new SaveLocationVM();

            location.Setup(new RegionService());

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveLocationVM model)
        {
            var locationService = new LocationService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = locationService.Create(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Location has been created successfully.";

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
            var locationService = new LocationService();

            var model = locationService.GetById(id);

            var location = new SaveLocationVM();

            location = location.MapToDto(model: model);

            location.Setup(new RegionService());

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveLocationVM model)
        {
            var locationService = new LocationService();

            if (ModelState.IsValid)
            {
                //Need to dynamic 
                model.NTLogin = "1618";

                var response = locationService.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Location has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            model.Setup(new RegionService());

            return View(model);
        }

        public ActionResult LocationDelete(string id)
        {
            var taskService = new LocationService();

            var response = taskService.Delete(id: id);

            if (response)
            {
                TempData["SuccessMessage"] = "Location deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            var taskService = new LocationService();

            var model = taskService.GetById(id);

            var location = new SaveLocationVM();

            location = location.MapToDto(model);

            //location.Setup(new DocumentFilesService(), new LocationService(), new LocationTypeService());

            return View(location);
        }
    }
}