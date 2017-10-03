using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.jqGrid;
using Msr.Models.EquipmentMaintenances;
using Msr.Services.EquipmentMaintenances;
using Msr.Services.EquipmentMaintenances.ViewModels;
using Msr.Services.Locations;

namespace Answer.Web.Controllers
{
    public class EquipmentMaintenanceController : BaseController
    {
        private readonly EquipmentMaintenanceService _equipmentMaintenanceService;
        private readonly LocationService _locationService;

        public EquipmentMaintenanceController()
        {
            _locationService = new LocationService();
            _equipmentMaintenanceService = new EquipmentMaintenanceService();
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
                
                    else if (rule.field == nameof(EquipmentMaintenanceView.PrimaryLocation))
                    {
                        totalRows = totalRows.Where(x => x.PrimaryLocation.ToLower().Contains(rule.data.ToLower()));
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
                    else if (rule.field == nameof(EquipmentMaintenanceView.Technician))
                    {
                        totalRows = totalRows.Where(x => x.Technician.ToLower().Contains(rule.data.ToLower()));
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
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }

                }
            }
            var orderBy = nameof(EquipmentMaintenanceView.Id);
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

        public ActionResult Create(string id)
        {
            var model = new CreateEquipmentMaintenanceViewModel();

            model.Setup(_equipmentMaintenanceService, id);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(CreateEquipmentMaintenanceViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = GetCurrentUser();

                model.NTLogin = loggedUser.Id;

                var response = _equipmentMaintenanceService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Equipment for maintenance has been added successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            model.Setup(_equipmentMaintenanceService, id);

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var vm = new EditEquipmentMaintainanceViewModel();

            var model = _equipmentMaintenanceService.GetById(id);

            vm.Read(model);
            
            vm.Setup(_equipmentMaintenanceService, id);

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(EditEquipmentMaintainanceViewModel model, string id)
        {
            var equipmentMaintenanceService = new EquipmentMaintenanceService();

            if (ModelState.IsValid)
            {
                var response = equipmentMaintenanceService.Update(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Equipment for maintenance has been updated successfully.";

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public JsonResult GetChildLocations(string id)
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
            locationsList.Add(new SelectListItem { Value = "", Text = "Select a Sublocation" });

            locationsList.AddRange(locations);


            return Json(new { locations = locationsList }, JsonRequestBehavior.AllowGet);
        }
    }
}