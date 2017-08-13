using System;
using System.Collections.Generic;
using System.Linq;
using Answer.Web.ViewModel.Administration;
using System.Web.Mvc;
using Msr.Services.Administration;
using Msr.Services.Administration.Messages;
using Msr.Services.Administration.ViewModels;
using Msr.Services.Companies;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Roles;

namespace Answer.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private AdministrationService _administrationService;
        private readonly RoleService _roleService;
        private readonly LocationService _locationService;

        public AdministrationController()
        {
            _locationService = new LocationService();
            _administrationService = new AdministrationService();
            _roleService = new RoleService();
        }

        public ActionResult AdminSetup()
        {
            return View();
        }

        public ActionResult AssignProcedureAdmin()
        {
            _administrationService.GetAssignProcedure();

            return View();
        }

        public ActionResult AssignNewsProcedure()
        {
            var vm = new AssignNewsProcedureViewModel();

            _administrationService.GetAssignProcedure();

            return View(vm);

        }

        [HttpPost]
        public ActionResult AssignNewsProcedure(AssignNewsProcedureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = _administrationService.AssignNewsProcedure();

                if (result.HasErrors())
                {
                    TempData["ErrorMessage"] = result.ErrorMessage();

                    return View(vm);
                }

                return RedirectToAction("AssignNewsProcedure");
            }

            return View(vm);
        }

        public ActionResult ViewCompanyUsage()
        {
            var data = _administrationService.ViewCompanyUsage();

            return View();
        }

        public ActionResult ViewCompanyUsageData(JqGridParam param)
        {
            var totalRows = _administrationService.ViewCompanyUsage();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                   
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

        public ActionResult AssignRoleToJob(string selectedJobId)
        {
            var vm = new AssignRoleToJobViewModel();
            vm.SelectedJobId = selectedJobId;

            vm.Jobs = _administrationService.GetAssignRoleToJob();

            if (!string.IsNullOrWhiteSpace(selectedJobId))
            {
                vm.Companies = _administrationService.GetRolesForJob(selectedJobId);
            }

            vm.SetUp(_roleService);

            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateAssignRoleToJob(List<UpdateAssignRoleToJobItem> companiesWithRoles, string selectedJobId)
        {
            if (ModelState.IsValid)
            {
                var result = _administrationService.UpdateAssignRoleToJob(new UpdateAssignRoleToJobRequest { RoleToJobItems = companiesWithRoles });

                if (!result.HasErrors())
                {
                    return RedirectToAction("AssignRoleToJob", new {selectedJobId});
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            var vm = new AssignRoleToJobViewModel();

            vm.Companies = _administrationService.GetRolesForJob(selectedJobId);

            vm.SetUp(_roleService);

            return View("AssignRoleToJob", vm);
        }

        public ActionResult AssignAccRecievableRole()
        {
            var vm = new AssignAccRecievableRoleViewModel();

            vm.Companies = _administrationService.GetAssignAccRecievableRole();

            vm.SetUp(_roleService, _locationService);

            return View(vm);
        }

        [HttpPost]
        public ActionResult AssignAccRecievableRole(List<AssignAccRecievableRoleItem> recievableRoleItems)
        {
            if (ModelState.IsValid)
            {
                var result = _administrationService.UpdateAssignAccRecievableRole(recievableRoleItems);

                if (!result.HasErrors())
                {
                    return RedirectToAction("AssignAccRecievableRole");
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            var vm = new AssignAccRecievableRoleViewModel();

            vm.SetUp(_roleService, _locationService);

            return View();
        }

        public ActionResult AssignCompaniesToView()
        {
            var vm = new AssignCompaniesToViewViewModel();

            vm.CompaniesToView = _administrationService.GetAssignCompaniesToView();

            vm.SetUp(new CompanyService());

            return View(vm);
        }

        [HttpPost]
        public ActionResult AssignCompaniesToView(List<AssignCompaniesToViewItemViewModel> recievableRoleItems)
        {
            if (ModelState.IsValid)
            {
                var result = _administrationService.UpdateAssignCompaniesToView(recievableRoleItems);

                if (!result.HasErrors())
                {
                    return RedirectToAction("AssignCompaniesToView");
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            var vm = new AssignCompaniesToViewViewModel();

            vm.SetUp(new CompanyService());

            return View(vm);
        }

        public ActionResult ModuleAccess()
        {
            var vm = new ModuleAccessViewViewModel();

            vm.CompaniesToView = _administrationService.GeModuleAccess();

            vm.SetUp(new RoleService());

            return View(vm);
        }

        [HttpPost]
        public ActionResult ModuleAccess(List<ModuleAccessViewModel> items)
        {

            if (ModelState.IsValid)
            {
                var result = _administrationService.UpdateModuleAccess(items);

                if (!result.HasErrors())
                {
                    return RedirectToAction("AssignCompaniesToView");
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            var vm = new ModuleAccessViewViewModel();

            vm.SetUp(new RoleService());

            return View(vm);
        }

        public ActionResult EditglobalWordsII()
        {
            var vm = new ModuleAccessViewViewModel();

            vm.CompaniesToView = _administrationService.GeModuleAccess();
            vm.GlobalSettings = _administrationService.ReadGlobalSettings();

            vm.SetUp(new RoleService());

            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateGlobalSettings(List<GlobalSettingsViewModel> globalSettings)
        {
            if (globalSettings.Any())
            {
                _administrationService.UpdateGlobalSettings(globalSettings);
            }

            return RedirectToAction("EditglobalWordsII");
        }
    }
}