using System;
using System.Collections.Generic;
using System.Linq;
using Answer.Web.ViewModel.Administration;
using System.Web.Mvc;
using Msr.Models.Comman;
using Msr.Services.Administration;
using Msr.Services.Administration.Messages;
using Msr.Services.Administration.ViewModels;
using Msr.Services.Companies;
using Msr.Services.jqGrid;
using Msr.Services.Locations;
using Msr.Services.Roles;


namespace Answer.Web.Controllers
{
    [Authorize]
    public class AdministrationController : BaseController
    {
        private AdministrationService _administrationService;
        private readonly RoleService _roleService;
        private readonly LocationService _locationService;
        private readonly CompanyService _companyService;

        public AdministrationController()
        {
            _locationService = new LocationService();
            _administrationService = new AdministrationService();
            _roleService = new RoleService();
            _companyService = new CompanyService();
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

            var jobList = new List<SelectListItem> {new SelectListItem {Value = "", Text = "--Select Role--"}};
            jobList.AddRange(_administrationService.GetAssignRoleToJob());

            vm.Jobs = jobList;

            if (!string.IsNullOrWhiteSpace(selectedJobId))
            {
                vm.Companies = _administrationService.GetRolesForJob(selectedJobId, GetCurrentUser().Id);
            }

            vm.SetUp(_roleService);

            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateAssignRoleToJob(List<UpdateAssignRoleToJobItem> companiesWithRoles, string selectedJobId)
        {
            var result = _administrationService.UpdateAssignRoleToJob(new UpdateAssignRoleToJobRequest { RoleToJobItems = companiesWithRoles, SelectedJobId = selectedJobId },GetCurrentUser().Id);

            if (!result.HasErrors())
            {
                TempData["SuccessMessage"] = "Job Assignment has been updated successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            return RedirectToAction("AssignRoleToJob", new { selectedJobId });
        }

        public ActionResult AssignAccRecievableRole()
        {
            var vm = new AssignAccRecievableRoleViewModel();

            vm.Companies = _administrationService.GetAssignAccRecievableRole(GetCurrentUser().Id);

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
                    TempData["SuccessMessage"] = "Assign Wscr Closer has been updated successfully.";

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

            vm.CompaniesToView = _companyService.GetCompaniesQueryable().Where(x => x.Status == "APPROVED").Select(x => new AssignCompaniesToViewResult
            {
                Id = x.ObjectId,
                Name = x.Name
            }).ToList();

            vm.SetUp(new CompanyService());

            return View(vm);
        }

        [HttpPost]
        public ActionResult AssignCompaniesToView(List<AssignCompaniesToViewItemViewModel> recievableRoleItems)
        {
            if (ModelState.IsValid)
            {
                var result = _administrationService.UpdateAssignCompaniesToView(recievableRoleItems, GetCurrentUser().Id);

                if (!result.HasErrors())
                {
                    TempData["SuccessMessage"] = "Assign Wscr Closer has been updated successfully.";


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

            vm.SelectedRoles = _roleService.GetSelectedRolesByCompanyId(GetCurrentUser().Company);
               
            return View(vm);
        }

        [HttpPost]
        public ActionResult ModuleAccess(List<ModuleAccessViewModel> items)
        {

            if (ModelState.IsValid)
            {

                var result = _administrationService.UpdateModuleAccess(items, GetCurrentUser().Id);

                if (!result.HasErrors())
                {
                    TempData["SuccessMessage"] = "Module Access has been updated successfully.";

                    return RedirectToAction("ModuleAccess");
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
            vm.GlobalSettings = _administrationService.ReadGlobalSettings().ToList();

            vm.SetUp(new RoleService());

            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateGlobalSettings(List<XmlContentViewModel> globalSettings)
        {
            if (globalSettings.Any())
            {
                _administrationService.UpdateGlobalSettings(globalSettings);
                TempData["SuccessMessage"] = "Global words has been updated successfully.";

            }

            return RedirectToAction("EditglobalWordsII");
        }

        public ActionResult EditEmailWords()
        {
            var vm = new EmailWordsViewModel();
            
            vm.EmailWords = _administrationService.ReadEmailWords().ToList();
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateEmailWords(List<XmlContentViewModel> emailWords)
        {
            if (emailWords.Any())
            {
                _administrationService.UpdateEmailWords(emailWords);
                TempData["SuccessMessage"] = "Email words has been updated successfully.";

            }

            return RedirectToAction("EditEmailWords");
        }

        public ActionResult ReAssignBoss()
        {
            var vm = new ReAssignBossViewModel();

            vm.NTLogin = GetCurrentUser().Id;

            vm.SetUp(_administrationService);

            return View(vm);
        }
        [HttpPost]
        public ActionResult ReAssignBoss(ReAssignBossViewModel items)
        {
            items.NTLogin = GetCurrentUser().Id;
            if (ModelState.IsValid)
            {
                var model = new ReAssignBossView();
                model = MapToDto(items);
                var result = _administrationService.ReassignBoss(model);

                if (!result.HasErrors())
                {
                    TempData["SuccessMessage"] = result.SuccessMessage;

                    return RedirectToAction("ReAssignBoss");
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            var vm = new ReAssignBossViewModel();

            vm.NTLogin = GetCurrentUser().Id;

            vm.SetUp(_administrationService);

            return View(vm);
        }
        public ReAssignBossView MapToDto(ReAssignBossViewModel model)
        {
            return new ReAssignBossView
            {
                Id = model.Id,
                FullName = model.FullName,
                ToBossId = model.ToBossId,
                NTLogin = model.NTLogin
            };
        }
    }
}