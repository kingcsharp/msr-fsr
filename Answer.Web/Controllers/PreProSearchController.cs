using Msr.Services.jqGrid;
using Msr.Services.PrePro;
using Msr.Services.PrePro.ViewModel;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Models.PrePro;
using Msr.Services.Documents;
using Msr.Services.Workflows;

namespace Answer.Web.Controllers
{
    public class PreProSearchController : BaseController
    {
        private readonly PreProServices _preProServices;
        private readonly DocumentFilesService _documentFilesService;
        private WorkflowService _workflowService;
        
        public PreProSearchController()
        {
            _preProServices = new PreProServices();
            _documentFilesService = new DocumentFilesService();
            _workflowService = new WorkflowService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "PreProSearch";

            return View(viewModel);
        }

        public ActionResult PreProData(JqGridParam param)
        {
            var totalRows = _preProServices.GetPreProQueryable();

            var defaultStatusList = base.GetDefaultStatus();

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status));

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PrePropSearchView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(PrePropSearchView.Title))
                    {
                        totalRows = totalRows.Where(x => x.Title.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PrePropSearchView.ObjId))
                    {
                        totalRows = totalRows.Where(x => x.ObjId.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PrePropSearchView.StepText))
                    {
                        totalRows = totalRows.Where(x => x.StepText.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PrePropSearchView.Rev))
                    {
                        int value;

                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }
                    else if (rule.field == nameof(PrePropSearchView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(PrePropSearchView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
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

        public ActionResult PreProDelete(string id, string ntlogin)
        {
            var response = _preProServices.Delete(id, ntlogin);

            if (response)
            {
                TempData["SuccessMessage"] = "PrePro deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var currentUser = GetCurrentUser();

            var model = new ProcedurePreProViewModel {CreatingCo = currentUser.Company};


            model.Setup(new PreProServices(), new DocumentFilesService(), currentUser);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(ProcedurePreProViewModel model)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = currentUser.Id;

                var response = _preProServices.Create(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure template has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new PreProServices(), new DocumentFilesService(), currentUser);

                    return View(model);
                }

            }

            model.Setup(new PreProServices(), new DocumentFilesService(), currentUser);

            return View(model);
        }

        public ActionResult Details(string id)
        {
            var currentUser = GetCurrentUser();

            var model = _preProServices.GetById(id);

            var vm = new ProcedurePreProViewModel();

            vm = vm.MapToDto(model);

            vm.Setup(_preProServices, _documentFilesService, currentUser);

            return View(vm);
        }

        public ActionResult Edit(string id)
        {
            var currentUser = GetCurrentUser();

            var result = _workflowService.CheckOutObject(id, currentUser.Id);

            var model = _preProServices.GetById(result.Entity);

            var vm = new ProcedurePreProViewModel();

            vm = vm.MapToDto(model);

            vm.CreatingCo = currentUser.Company;

            vm.Setup(_preProServices, _documentFilesService, currentUser);

            var labors = _preProServices.GetLaborStepsList().Where(x => x.StepId == vm.ProcObjId).ToList();

            vm.Labor = labors;
            vm.ApplicationObjects = "Object Description";

            var preview = string.Join(",", vm.DocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'", x.SERVER_PATH)));
            ViewBag.Preview = preview;

            var jsonSerialiser = new JavaScriptSerializer();

            var previewConfig = jsonSerialiser.Serialize(vm.DocLinks.Select(x => new
            {
                caption = x.NAME,
                type = x.TYPE,
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new { file = x.LINKED_DOC_ID }),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }));

            ViewBag.PreviewConfig = previewConfig;

            return View(vm);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(ProcedurePreProViewModel model)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                model.NTLogin = currentUser.Id;

                var response = _preProServices.Save(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Step has been Updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_preProServices, _documentFilesService, currentUser);

                return View(model);
            }

            model.Setup(_preProServices, _documentFilesService, currentUser);

            return View(model);
        }

        public ActionResult Labors(string id, string procStepId, string relationship)
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "PreProSearch";

            ViewBag.Id = id;
            ViewBag.ProcedureStepId = procStepId;
            ViewBag.relationship = relationship;

            return View(viewModel);
        }
        public ActionResult LaborsData(JqGridParam param, string id)
        {
            var preproService = new PreProServices();


            var totalRows = preproService.GetLaborStepsList().Where(x => x.StepId == id);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureObjectsLaborStepView.RoleName))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(ProcedureObjectsLaborStepView.ObjId))
                    {
                        totalRows = totalRows.Where(x => x.ObjId.ToLower().Contains(rule.data.ToLower()));
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddLabor(string id, string procStepId, string relationship)
        {
            var model = new ProcedureObjectViewModel
            {
                ProcedureObjectId = id,
                ProcedureStepId = procStepId,
                Relationship = relationship
            };
            model.SetUp(new PreProServices());
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddLabor(ProcedureObjectViewModel model)
        {
            var preProServices = new PreProServices();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = preProServices.AddLabor(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Labor has been created successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.SetUp(new PreProServices());

                return View(model);
            }

            model.SetUp(new PreProServices());

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditLabor(string id)
        {
            var preProServices = new PreProServices();

            var procedureObjectViewModel = new ProcedureObjectViewModel();

            var model = preProServices.GetLaborStepsList().SingleOrDefault(x => x.Id == id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.SetUp(new PreProServices());

            return View(procedureObjectViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLabor(ProcedureObjectViewModel model)
        {
            var preProServices = new PreProServices();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;

                var response = preProServices.SaveLabor(model: model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Labor has been edited successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.SetUp(new PreProServices());

                return View(model);
            }

            model.SetUp(new PreProServices());

            return View(model);
        }

        public ActionResult ApplicableObjects(string id)
        {
            var applicableObjectsView = new ApplicableObjectsView();

            applicableObjectsView.StepId = id;
            applicableObjectsView.Setup(new PreProServices(), GetCurrentUser().Company);

            return View(applicableObjectsView);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult ApplicableObjects(ApplicableObjectsView model)
        {
            var preProServices = new PreProServices();

            if (ModelState.IsValid)
            {
                model.NTLogin = GetCurrentUser().Id;
                var response = false;


                model.LinkId = "NEW__1";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__2";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__3";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__4";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__5";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__6";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__7";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__8";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__9";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__10";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__11";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                model.LinkId = "NEW__12";
                model.ObjectId = model.NEW__1;
                response = preProServices.UpdateApplicableObjects(model);

                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Step has been Updated successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new PreProServices(), GetCurrentUser().Id);

                    return View(model);
                }
            }

            model.Setup(new PreProServices(), GetCurrentUser().Id);

            return View(model);
        }
    }
}