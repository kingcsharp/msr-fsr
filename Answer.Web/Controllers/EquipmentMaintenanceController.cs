using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Infrastructure.Common.Constansts;
using Msr.Services.jqGrid;
using Msr.Models.EquipmentMaintenances;
using Msr.Services.EquipmentMaintenances;
using Msr.Services.EquipmentMaintenances.ViewModels;
using Msr.Services.Locations;
using Msr.Services.Roles;

namespace Answer.Web.Controllers
{
    public class EquipmentMaintenanceController : BaseController
    {
        private readonly EquipmentMaintenanceService _equipmentMaintenanceService;
        private readonly LocationService _locationService;
        private readonly RoleService _roleService;

        public EquipmentMaintenanceController()
        {
            _locationService = new LocationService();
            _equipmentMaintenanceService = new EquipmentMaintenanceService();
            _roleService = new RoleService();
        }

        public ActionResult Index()
        {
            ViewBag.ActiveClass = "EquipmentMaintenance";

            return View();
        }

        public ActionResult EquipmentMaintenanceData(JqGridParam param)
        {
            var totalRows = _equipmentMaintenanceService.GetEquipmentsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(EquipmentMaintenanceView.ObjectId))
                    {
                        totalRows = totalRows.Where(x => x.ObjectId == rule.data.ToLower());
                    }

                    else if (rule.field == nameof(EquipmentMaintenanceView.ParentLocation))
                    {
                        totalRows = totalRows.Where(x => x.ParentLocation.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.SubLocationFirst))
                    {
                        totalRows = totalRows.Where(x => x.SubLocationFirst.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.SubLocationSecond))
                    {
                        totalRows = totalRows.Where(x => x.SubLocationSecond.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.DateTime))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.DateTime.HasValue && q.DateTime.Value.Day == value.Day &&
                                                             q.DateTime.Value.Month == value.Month && q.DateTime.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.RequestedBy))
                    {
                        totalRows = totalRows.Where(x => x.RequestedBy.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.ApprovedBy))
                    {
                        totalRows = totalRows.Where(x => x.ApprovedBy.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.TroubleState))
                    {
                        totalRows = totalRows.Where(x => x.TroubleState.ToString().ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.Comments))
                    {
                        totalRows = totalRows.Where(x => x.Comments.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToList();

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }

                }
            }
            var orderBy = nameof(EquipmentMaintenanceView.Id);

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
            var totalPages = (int)Math.Ceiling(totalRecords / (float)param.pageSize);

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

        public ActionResult Create(string id)
        {
            var loggedUser = GetCurrentUser();

            var model = new CreateEquipmentMaintenanceViewModel();
            model.NTLogin = loggedUser.Id;
            model.Setup(_equipmentMaintenanceService, _roleService);

            var myRoles = _roleService.GetMyRoles(loggedUser.Id);

            model.CanAddPreventativeEm = myRoles.Any(x => x.Role_Name.Contains(RoleConstants.ProductionManager));

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form, string id)
        {
            var model = new CreateEquipmentMaintenanceViewModel();

            var loggedUser = GetCurrentUser();

            if (TryUpdateModel(model, form))
            {
                model.Id = null;

                if (!model.TroubleState)
                {
                    model.RequestedById = loggedUser.Id;
                    model.MaintenanceTask = EquipmentMaintenanceTypeConstants.RoutineMaintenance;
                    model.Status = EquipmentMaintenanceConstants.Assigned;
                }
                else
                {
                    model.MaintenanceTask = EquipmentMaintenanceTypeConstants.Repair;
                    model.Status = EquipmentMaintenanceConstants.Requested;
                }

                var response = _equipmentMaintenanceService.Create(model);

                if (!response.HasErrors())
                {
                    TempData[NotificationConstants.SuccessMessage] = "Equipment for maintenance has been created successfully.";

                    return RedirectToAction("Index");
                }

                TempData[NotificationConstants.ErrrorMessage] = response.ErrorMessage;
            }

            model.NTLogin = loggedUser.Id;
            model.Setup(_equipmentMaintenanceService, _roleService);

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var loggedUser = GetCurrentUser();

            var vm = new EditEquipmentMaintainanceViewModel();

            var model = _equipmentMaintenanceService.GetById(id);

            vm.Read(model);

            vm.NTLogin = loggedUser.Id;
            vm.Setup(_equipmentMaintenanceService, _roleService);

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form, string id)
        {
            var model = new EditEquipmentMaintainanceViewModel();
            var loggedUser = GetCurrentUser();
            model.ApprovedById = loggedUser.Id;
            model.NTLogin = loggedUser.Id;
            
            if (TryUpdateModel(model, form))
            {
                var response = _equipmentMaintenanceService.Update(model);

                if (!response.HasErrors())
                {
                    TempData[NotificationConstants.SuccessMessage] = "Equipment for maintenance has been updated successfully.";

                    return RedirectToAction("Index");
                }
            }

            TempData[NotificationConstants.ErrrorMessage] = "There is an error with the request.";

            model.NTLogin = loggedUser.Id;
            model.Setup(_equipmentMaintenanceService, _roleService);

            return View(model);
        }
        public JsonResult GetAllParentLocations()
        {
            var locations = _locationService.GetLocationsQueryable().Where(x => x.ParentLocation == null).Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ParentLocation,
                }).OrderBy(o => o.Text).ToList();

            if (!locations.Any())
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locationsList =
                new List<SelectListItem> { new SelectListItem { Value = "", Text = @"Select a Sublocation" } };

            locationsList.AddRange(locations);


            return Json(new { locations = locationsList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChildLocations(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locations = _locationService.GetLocationsQueryable().Where(x => x.ObjectId == id).Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ParentLocation,
                }).OrderBy(o => o.Text).ToList();

            if (!locations.Any())
            {
                return Json(new { locations = "" }, JsonRequestBehavior.AllowGet);
            }

            var locationsList = new List<SelectListItem>();

            locationsList.AddRange(locations);


            return Json(new { locations = locationsList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSecoundLocationsManually(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locations = _locationService.GetLocationsQueryable().Where(x => x.ParentLocation == id).Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ObjectId,
                }).OrderBy(o => o.Text).ToList();

            if (!locations.Any())
            {
                return Json(new { locations = "" }, JsonRequestBehavior.AllowGet);
            }

            var locationsList =
                new List<SelectListItem> { new SelectListItem { Value = "", Text = @"Select a Sublocation" } };

            locationsList.AddRange(locations);


            return Json(new { locations = locationsList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetthirdLocationsManually(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locations = _locationService.GetLocationsQueryable().Where(x => x.ParentLocation == id).Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ObjectId,
                }).OrderBy(o => o.Text).ToList();

            if (!locations.Any())
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locationsList = new List<SelectListItem>();

            locationsList.AddRange(locations);


            return Json(new { locations = locationsList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSecoundLocations(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locations = _locationService.GetSecoundLocations(id).Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ObjectId,
                }).OrderBy(o => o.Text).ToList();

            if (!locations.Any())
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locationsList = new List<SelectListItem>();

            locationsList.AddRange(locations);


            return Json(new { locations = locationsList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParentLocations(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            var locations = _locationService.GetParentLocations(id);

            if (!locations.Any())
            {
                return Json(new { locations = "" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { locations }, JsonRequestBehavior.AllowGet);
        }
    }
}