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
    [Authorize]
    public class LocationsController : BaseController
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

            var defaultStatusList = "CREATING,DENIED,APPROVED,APPROVED_BUT_REVISING,APPROVED_BUT_DELETING".Split(',');

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status));

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
                    else if (rule.field == nameof(LocationView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
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
            var location = new SaveLocationViewModel();

            location.Setup(new RegionService(), new LocationService());

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveLocationViewModel model)
        {
            var locationService = new LocationService();

            if (ModelState.IsValid)
            {
                model.LoggedUserIdResult = GetCurrentUser();

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

            var location = new SaveLocationViewModel();

            location = location.MapToDto(model: model);

            location.Setup(new RegionService(), locationService);

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveLocationViewModel model)
        {
            var locationService = new LocationService();

            if (ModelState.IsValid)
            {
                model.LoggedUserIdResult = GetCurrentUser();

                var response = locationService.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Location has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            model.Setup(new RegionService(), locationService);

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

            var location = new SaveLocationViewModel();

            location = location.MapToDto(model);

            //location.Setup(new DocumentFilesService(), new LocationService(), new LocationTypeService());

            return View(location);
        }
    }
}