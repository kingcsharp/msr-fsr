using Msr.Models.Locations;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Locations.ViewModels;
using Msr.Services.Regions;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Locations)]
    public class LocationsController : BaseController
    {
        private readonly LocationService _locationService;
        private readonly RegionService _regionService;

        public LocationsController()
        {
            _locationService = new LocationService();
            _regionService = new RegionService();
        }
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
            var myCo = GetCurrentUser();

            var totalRows = _locationService.GetLocationsQueryable().Where(x => x.CreatingCo == myCo.Root_Company);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(LocationView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(LocationView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(LocationView.InternalAddress))
                    {
                        totalRows = totalRows.Where(x => x.InternalAddress.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(LocationView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(LocationView.RegionName))
                    {
                        totalRows = totalRows.Where(x => x.RegionName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(LocationView.Region))
                    {
                        totalRows = totalRows.Where(x => x.Region.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(LocationView.ParentLocationName))
                    {
                        totalRows = totalRows.Where(x => x.ParentLocationName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(LocationView.Revision))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Revision == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Revision.ToString().Contains(rule.data.ToLower()));
                        }

                    }
                    else if (rule.field == nameof(LocationView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
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

            var currentUser = GetCurrentUser();

            location.Setup(_regionService, _locationService, currentUser.Id);

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveLocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.LoggedUserIdResult = GetCurrentUser();

                var response = _locationService.Create(model);

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
            else
            {
                if (model.Parent != "" && ModelState["InternalAddress"].Errors.Count == 0 && ModelState["Name"].Errors.Count == 0)
                {
                    model.LoggedUserIdResult = GetCurrentUser();
                    var response = _locationService.Save(model);

                    if (response)
                    {
                        TempData["SuccessMessage"] = "Location has been created successfully.";

                        return RedirectToAction("Index");
                    }

                    TempData["ErrorMessage"] = "Something went wrong.";

                    return View(model);
                }
            }

            return View(model);

        }

        public ActionResult Edit(string id)
        {
            var currentUser = GetCurrentUser();

            var model = _locationService.GetById(id);

            var location = new SaveLocationViewModel();

            location = location.MapToDto(model);

            location.Setup(_regionService, _locationService, currentUser.Id);

            return View(location);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveLocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.LoggedUserIdResult = GetCurrentUser();

                var response = _locationService.Save(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Location has been updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }
            else
            {
                if (model.Parent != "" && ModelState["InternalAddress"].Errors.Count == 0 && ModelState["Name"].Errors.Count == 0)
                {
                    var saveEditModel = new SaveLocationViewModel()
                    {
                        Parent = model.Parent,
                        InternalAddress = model.InternalAddress,
                        Name = model.Name,
                        ObjectId = model.ObjectId,
                        LoggedUserIdResult = GetCurrentUser()
                    };

                    var response = _locationService.Save(saveEditModel);

                    if (response)
                    {
                        TempData["SuccessMessage"] = "Location has been updated successfully.";

                        return RedirectToAction("Index");
                    }

                    TempData["ErrorMessage"] = "Something went wrong.";

                    return View(model);
                }
            }

            model.Setup(_regionService, _locationService, GetCurrentUser().Id);

            return View(model);
        }

        public ActionResult LocationDelete(string id, string ntlogin)
        {
            var response = _locationService.Delete(id: id, ntlogin: ntlogin);

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
            var model = _locationService.GetById(id);

            var location = new SaveLocationViewModel();

            location = location.MapToDto(model);

            //location.Setup(new DocumentFilesService(), new LocationService(), new LocationTypeService());

            return View(location);
        }
    }
}