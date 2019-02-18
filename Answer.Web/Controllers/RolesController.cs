using Msr.Models.Roles;
using Msr.Services.jqGrid;
using Msr.Services.Roles;
using Msr.Services.Roles.ViewModels;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;
using Msr.Services.Users;
using Msr.Services.Documents;
using System.Web.Script.Serialization;
using Msr.Commons.Files;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.People)]
    public class RolesController : BaseController
    {
        private readonly RoleService _roleService;
        private readonly UserService _userService;
        private readonly DocumentFilesService _documentFilesService;


        public RolesController()
        {
            _roleService = new RoleService();
            _userService = new UserService();
            _documentFilesService = new DocumentFilesService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Roles";

            return View(viewModel);
        }

        public ActionResult UserRolesData(JqGridParam param)
        {
            var totalRows = _roleService.GetUserRolesQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(RolesView.RoleName))
                    {
                        totalRows = totalRows.Where(x => x.RoleName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(RolesView.SecurityLevelName))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevelName.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.SecurityLevel))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevel.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.Revision))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Revision == value);
                        }
                    }
                    else if (rule.field == nameof(RolesView.LockedBy))
                    {
                        totalRows = totalRows.Where(x => x.LockedBy.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower() == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(RolesView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }
            var orderBy = param.sortColumn;

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

            var results = totalRows.Select(x => new
            {
                x.RoleName,
                x.SecurityLevelName,
                x.SecurityLevel,
                x.LockedBy,
                x.LockedByName,
                x.Revision,
                x.Status,
                x.ObjectId
            }).ToList();

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
            var currentUser = GetCurrentUser();

            var vm = new SaveRoleViewModel();

            vm.Setup(_documentFilesService, _roleService, _userService, currentUser);

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveRoleViewModel vm)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                vm.NTLogin = currentUser.Id;

                var response = _roleService.Create(model: vm);

                if (response)
                {
                    TempData["SuccessMessage"] = "User Role has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    vm.Setup(_documentFilesService, _roleService, _userService, currentUser);

                    return View(vm);
                }
            }

            vm.Setup(_documentFilesService, _roleService, _userService, currentUser);

            return View(vm);
        }

        public ActionResult Edit(string id)
        {
            var vm = new SaveRoleViewModel();

            var currentUser = GetCurrentUser();
            var role = _roleService.GetRoleById(id);

            vm.Read(role);
            vm.Setup(_documentFilesService, _roleService, _userService, currentUser);

            var preview = string.Join(",", vm.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));
            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();
            var previewConfig = jsonSerialiser.Serialize(vm.DocLinks.Select(x => new
            {
                caption = x.NAME,
                type = MimeTypes.GetContentType(x.CONTENTTYPE),
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new { file = x.LINKED_DOC_ID }),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }));

            ViewBag.PreviewConfig = previewConfig;

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SaveRoleViewModel model)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = currentUser.Id;

                var response = _roleService.Update(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "User Role has been Updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";
                    model.Setup(_documentFilesService, _roleService, _userService, currentUser);
                    return View(model);
                }
            }

            model.Setup(_documentFilesService , _roleService, _userService, currentUser);

            return View(model);
        }


        public ActionResult RoleDelete(string id,string ntlogin)
        {
            var response = _roleService.Delete(id: id,ntlogin:ntlogin);

            if (response)
            {
                TempData["SuccessMessage"] = "Role deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }


        public ActionResult Detail(string id)
        {
            var currentUser = GetCurrentUser();

            var model = _roleService.GetRoleById(id: id);

            var saveRoleViewModel = new SaveRoleViewModel();

            saveRoleViewModel.Read(model);
            saveRoleViewModel.Setup(_documentFilesService, _roleService, _userService, currentUser);

            return View(saveRoleViewModel);
        }

        public ActionResult LoadPerson()
        {
            return PartialView("PersonDate");
        }

    }
}