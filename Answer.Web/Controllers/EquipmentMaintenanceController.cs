using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Infrastructure.Common.Constansts;
using Msr.Services.jqGrid;
using Msr.Models.EquipmentMaintenances;
using Msr.Models.Menus;
using Msr.Services.EquipmentMaintenances;
using Msr.Services.EquipmentMaintenances.ViewModels;
using Msr.Services.Locations;
using Msr.Services.Roles;
using Msr.Models.Locations;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Administration)]
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

            var loggedUser = GetCurrentUser();

            var myRoles = _roleService.GetAssignedRoles(loggedUser.Id);

            ViewBag.HasMaintenanceTechnicianRole = myRoles.Any(x => x.Role_Name.Contains(RoleConstants.MaintenanceTechnician));
            ViewBag.HasProductionManagerRole = myRoles.Any(x => x.Role_Name.Contains(RoleConstants.ProductionManager));
            ViewBag.AssignedTo = _equipmentMaintenanceService.GetEquipmentsQueryable().Where(x => x.AssignedTo != null).Select(x => x.AssignedTo).Distinct()
                .ToArray();
            return View();
        }

        public ActionResult EquipmentMaintenanceData(JqGridParam param)
        {
            var totalRows = _equipmentMaintenanceService.GetEquipmentsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {

                   if (rule.field == nameof(EquipmentMaintenanceView.DateTime))
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
                    else if (rule.field == nameof(EquipmentMaintenanceView.AssignedTo) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.AssignedTo.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.TroubleState) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => Boolean.Parse(x.Trim().ToLower())).ToArray();
                        if (list.Any() && list.Length < 2)
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.TroubleState));
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.Comments))
                    {
                        totalRows = totalRows.Where(x => x.Comments.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.MaintenanceTask) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.MaintenanceTask.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.Status) && rule.data != "")
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.PemLastCompletedDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.PemLastCompletedDate.HasValue && q.PemLastCompletedDate.Value.Day == value.Day &&
                                                             q.PemLastCompletedDate.Value.Month == value.Month && q.PemLastCompletedDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.FrequencyField))
                    {
                        int value;
                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.FrequencyField == value);
                        }
                    }
                    else if (rule.field == nameof(EquipmentMaintenanceView.Id))
                    {
                        int value;
                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.Id == value);
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

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            var model = new CreateEquipmentMaintenanceViewModel();

            var loggedUser = GetCurrentUser();
            model.NTLogin = loggedUser.Id;

            TryUpdateModel(model, form);

            var myRoles = _roleService.GetAssignedRoles(loggedUser.Id);

            var hasProductionManagerRole = myRoles.Any(x => x.Role_Name.Contains(RoleConstants.ProductionManager));

            if (hasProductionManagerRole && !model.TroubleState)
            {
                if (!model.PemLastCompletedDate.HasValue)
                {
                    ModelState.AddModelError(nameof(CreateEquipmentMaintenanceViewModel.PemLastCompletedDate), "PemLastCompletedDate is required");
                }
                if (!model.FrequencyField.HasValue)
                {
                    ModelState.AddModelError(nameof(CreateEquipmentMaintenanceViewModel.FrequencyField), "FrequencyField is required");
                }
            }

            if (ModelState.IsValid)
            {
                var response = _equipmentMaintenanceService.Create(model);

                if (!response.HasErrors())
                {
                    TempData[NotificationConstants.SuccessMessage] = "Equipment for maintenance has been created successfully.";

                    return RedirectToAction("Index");
                }

                TempData[NotificationConstants.ErrrorMessage] = response.ErrorMessage;
            }

            model.Setup(_equipmentMaintenanceService, _roleService);

            return View(model);
        }

        public ActionResult Edit(int id)
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
        public ActionResult Edit(FormCollection form)
        {
            var model = new EditEquipmentMaintainanceViewModel();

            TryUpdateModel(model, form);

            if (ModelState.IsValid)
            {
                var response = _equipmentMaintenanceService.Update(model);

                if (!response.HasErrors())
                {
                    TempData[NotificationConstants.SuccessMessage] = "Equipment for maintenance has been updated successfully.";

                    return RedirectToAction("Index");
                }
            }

            TempData[NotificationConstants.ErrrorMessage] = "There is an error with the request.";

            var loggedUser = GetCurrentUser();

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

        public JsonResult GetScanBarCodeLocations(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            LocationView locationView = _equipmentMaintenanceService.GetEquipmentRoom().Where(x => x.InternalAddress.ToLower() == id.ToLower()).FirstOrDefault();

            return Json(new { locationId = locationView?.ObjectId }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TakeOwnership(int id)
        {
            var loggedUser = GetCurrentUser();

            var takeOwnershipResult = _equipmentMaintenanceService.TakeOwnership(id, loggedUser.Id);

            if (!takeOwnershipResult.HasErrors())
            {
                TempData[NotificationConstants.SuccessMessage] = "Equipment maintenance has been assigned successfully.";
            }
            else
            {
                TempData[NotificationConstants.ErrrorMessage] = takeOwnershipResult.ErrorMessage;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarkCompleted(int id)
        {
            var loggedUser = GetCurrentUser();

            var result = _equipmentMaintenanceService.MarkCompleted(id, loggedUser.Id);

            if (!result.HasErrors())
            {
                TempData[NotificationConstants.SuccessMessage] = "Equipment maintenance status has been set to completed.";
            }
            else
            {
                TempData[NotificationConstants.ErrrorMessage] = result.ErrorMessage;
            }
            return RedirectToAction("Index");
        }
    }
}