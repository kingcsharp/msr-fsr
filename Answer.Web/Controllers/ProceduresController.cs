using Msr.Models.Procedures;
using Msr.Services.jqGrid;
using Msr.Services.Procedures;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Answer.Web.ViewModel;
using Msr.Commons.Files;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.ActualParts;
using Msr.Services.Documents;
using Msr.Services.Procedures.Messages;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using Msr.Services.Roles;
using Msr.Services.Users;
using Msr.Services.EquipmentMaintenances;

namespace Answer.Web.Controllers
{
    public class ProceduresController : BaseController
    {
        private readonly ProceduresService _proceduresService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly ProcedureVerbsService _procedureVerbsService;
        private readonly DocumentFilesService _documentFilesService;

        public ProceduresController()
        {
            _proceduresService = new ProceduresService();
            _userService = new UserService();
            _roleService = new RoleService();
            _procedureVerbsService = new ProcedureVerbsService();
            _documentFilesService = new DocumentFilesService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Procedures";

            return View(viewModel);
        }

        public ActionResult GetProcedures(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            return PartialView("_Procedures");
        }

        public ActionResult ProceduresApprovedData(JqGridParam param)
        {
            var totalRows = _proceduresService.GetProcedureApprovedQueryable(GetCurrentUser().Id);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureApprovedView.ROOT))
                    {
                        totalRows = totalRows.Where(x => x.ROOT == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureApprovedView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureApprovedView.Creating_co_Name))
                    {
                        totalRows = totalRows.Where(x => x.Creating_co_Name.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ProcedureApprovedView.Name);

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

        public ActionResult ProceduresData(JqGridParam param)
        {
            var totalRows = _proceduresService.GetProceduresQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.VerbName))
                    {
                        totalRows = totalRows.Where(x => x.VerbName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.SecurityLevel))
                    {
                        totalRows = totalRows.Where(x => x.SecurityLevel.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.Rev))
                    {
                        int value;

                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Rev.ToString().ToLower() == rule.data.ToLower());
                        }
                    }
                    else if (rule.field == nameof(ProcedureView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(ProcedureView.CreatingCoName))
                    {
                        totalRows = totalRows.Where(x => x.CreatingCoName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.DeptName))
                    {
                        totalRows = totalRows.Where(x => x.DeptName.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ProcedureView.Id);

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }

            totalRows = param.sortOrder == "desc" ? totalRows.OrderByDescending(orderBy) : totalRows.OrderBy(orderBy);

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

        public ActionResult Create()
        {
            var currentUser = GetCurrentUser();
            var saveProcedureViewModel = new SaveProcedureViewModel();

            saveProcedureViewModel.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

            return View(saveProcedureViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveProcedureViewModel model)
        {
            var currentUser = GetCurrentUser();

            model.NTLogin = GetCurrentUser().Id;

            if (ModelState.IsValid)
            {
                var response = _proceduresService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Procedure has been created successfully.";

                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

                return View(model);
            }

            model.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

            return View(model);
        }

        public ActionResult ProcedureDelete(string id)
        {

            var response = _proceduresService.DeleteProcedure(id: id, ntlogin: GetCurrentUser().Id);

            if (response)
            {
                TempData["SuccessMessage"] = "Procedure deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var vm = new SaveProcedureViewModel();

            var currentUser = GetCurrentUser();

            var procedureView = _proceduresService.GetProcedureById(id);

            vm.MapToDto(procedureView);

            vm.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

            var viewModel = new GetStepDataResult
            {
                GetStepDataResults = _proceduresService.GetStepsData(id, currentUser.Id)
            };

            viewModel.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());
            viewModel.AddMonitorForProcedureViewModel.Related_Object_Id = id;
            viewModel.AddMonitorForProcedureViewModel.Step_Id = viewModel.GetMoniterViewModels.FirstOrDefault()?.Step_Id;

            ViewBag.ProcObjectId = id;
            ViewBag.ProdecureName = procedureView.Name;

            vm.StepTemplateList.Add(new SelectListItem { Text = "None", Value = "" });
            vm.StepTemplateList.AddRange(_proceduresService.GetStepTemplateList(currentUser.Root_Company).Select(x =>
                new SelectListItem
                {
                    Text = x.Show,
                    Value = x.Value.ToString()
                }).OrderBy(o => o.Text).ToList());

            vm.GetStepDataResults = viewModel;

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
        [ValidateInput(false)]
        public ActionResult Edit(SaveProcedureViewModel model, string command, string addNewStep)
        {
            var currentUser = GetCurrentUser();

            if (!string.IsNullOrWhiteSpace(addNewStep))
            {
                if (string.IsNullOrWhiteSpace(model.AddStepTemplateId))
                {
                    var vm = new GetStepEditDataViewModel
                    {
                        NtLogin = currentUser.Id,
                        GetStepEditData =
                        {
                            Start_On_Counter = 0,
                            Duration_Type = "TIME_SYS_SECONDS",
                            System_Task = "SYS_COMP_TEST"
                        }
                    };
                    vm.ProcObjId = model.ObjectId;
                    _proceduresService.CreateStepData(vm);
                }
                else
                {
                    _proceduresService.PrePopSave(model.ObjectId, model.AddStepTemplateId, currentUser.Id);
                }

                var steps = _proceduresService.GetStepsData(model.ObjectId, currentUser.Id);

                var stepsOrders = steps.ToDictionary(x => x.Id, x => x.Print_Order);
                
                _proceduresService.SaveReorderSteps(stepsOrders, model.ObjectId, currentUser.Id);

                TempData["SuccessMessage"] = "Step has been Created successfully.";

                return RedirectToAction("Edit", "Procedures", new { id = model.ObjectId });
            }

            model.NTLogin = currentUser.Id;

            if (ModelState.IsValid)
            {
                if (command == "Update")
                {
                    var response = _proceduresService.Save(model);

                    if (response)
                    {
                        AddSuccessNotification("Procedure has been updated successfully.");

                        return RedirectToAction("Index");
                    }

                    AddErrorNotification("Something went wrong.");

                    model.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

                    return View("Edit", model);
                }

                if (command == "Cancel And Roll Back")
                {
                    var response = _proceduresService.RollBack(model);
                    if (response)
                    {
                        AddSuccessNotification("Procedure has been canceled and roll back successfully.");

                        return RedirectToAction("Index");
                    }

                    AddErrorNotification("Something went wrong.");

                    model.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

                    return View("Edit", model);
                }

            }

            model.Setup(_proceduresService, _roleService, _procedureVerbsService, _documentFilesService, currentUser.Id, currentUser.Root_Company);

            return View("Edit", model);
        }

        public ActionResult View(string id)
        {
            var viewModel = new ProcedureViewModel();

            var model = _proceduresService.GetProcedureById(id);

            var stepDataResults = _proceduresService.GetStepsData(id, GetCurrentUser().Id);

            viewModel.ProcedureView = model;
            viewModel.StepDataList = stepDataResults;

            return View(viewModel);
        }

        public ActionResult AssignProcedure(string id)
        {
            var vm = new AssignProcedureViewModel();

            vm.Setup(_userService);

            var procedureName = _proceduresService.GetProcedureName(id);

            vm.ProcedureName = procedureName;
            vm.Id = id;

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AssignProcedure(AssignProcedureViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.LoginId = GetCurrentUser().Id;
                var response = _proceduresService.SaveAssignProcedure(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Procedure has been assigned successfully.";

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_userService);

                return View(model);
            }

            model.Setup(_userService);

            return View(model);
        }

        public ActionResult Steps(string id)
        {
            var getCurrentUser = GetCurrentUser();
            var procedureName = _proceduresService.GetProceduresQueryable().SingleOrDefault(x => x.ObjectId == id)?.Name;

            var viewModel = new GetStepDataResult
            {
                GetStepDataResults = _proceduresService.GetStepsData(id, getCurrentUser.Id)
            };
            viewModel.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());
            viewModel.AddMonitorForProcedureViewModel.Related_Object_Id = id;
            ViewBag.ProcObjectId = id;
            ViewBag.ProdecureName = procedureName;

            ViewBag.Procedure = new SelectList(_proceduresService.GetStepTemplateList(getCurrentUser.Root_Company), "Value", "Show");


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveStep(string procObjectId, string procedure)
        {
            var id = _proceduresService.PrePopSave(procObjectId, procedure, GetCurrentUser().Id);

            ViewBag.Id = id;
            ViewBag.Procedure = procedure;
            var currentUser = GetCurrentUser();

            var vm = new GetStepEditDataViewModel
            {
                NtLogin = currentUser.Login,

                GetStepEditData =
                {
                    Start_On_Counter = 0,
                    Duration_Type = "TIME_SYS_SECONDS",
                    System_Task = "SYS_COMP_TEST"
                }
            };
            vm.SetUp(_proceduresService, _procedureVerbsService, procObjectId);

            var singleOrDefault = _proceduresService.GetStepTemplateList(currentUser.Root_Company).SingleOrDefault(x => x.Value == procedure);
            if (singleOrDefault != null)
            {
                vm.GetStepEditData.Step_Text = singleOrDefault.StepText;
                vm.GetStepEditData.Title = singleOrDefault.Show;
                vm.ProcObjId = procObjectId;
            }
            var response = _proceduresService.CreateStepData(vm);

            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = "Step has been created successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong.";
            }


            return RedirectToAction("Edit", "Procedures", new { Id = procObjectId });
        }

        public ActionResult CreateStep(string procedureObjectId, string procedure)
        {
            var currentUser = GetCurrentUser();

            var vm = new GetStepEditDataViewModel { NtLogin = currentUser.Login };

            vm.SetUp(_proceduresService, _procedureVerbsService, procedureObjectId);

            var singleOrDefault = _proceduresService.GetStepTemplateList(currentUser.Root_Company).SingleOrDefault(x => x.Value == procedure);
            if (singleOrDefault == null) return View(vm);

            if (singleOrDefault.Show != "NONE")
            {
                vm.GetStepEditData.Step_Text = singleOrDefault.StepText;
            }

            vm.ProcObjId = procedureObjectId;

            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateStep(GetStepEditDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NtLogin = GetCurrentUser().Id;

                var response = _proceduresService.CreateStepData(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Procedure has been assigned successfully.";

                    return RedirectToAction("Edit", "Procedures", new { Id = model.ProcObjId });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                return View(model);
            }

            return View(model);
        }

        public ActionResult EditStep(string stepId, string procedureObjectId)
        {
            var currentUser = GetCurrentUser();

            var vm = _proceduresService.GetStepData(stepId, procedureObjectId, currentUser.Id);

            vm.NtLogin = currentUser.Login;

            vm.SetUp(_proceduresService, _procedureVerbsService, procedureObjectId);

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditStep(GetStepDataResult viewModel, string procedureId, string id, string updateAction)
        {
            if (!string.IsNullOrWhiteSpace(updateAction) && updateAction == "delete")
            {
                var response = _proceduresService.DeleteProcedureStep(id, GetCurrentUser().Id);

                if (!response.HasErrors())
                {
                    AddSuccessNotification("Step deleted successfully.");
                }
                else
                {
                    AddErrorNotification("Something went wrong.");
                }

                return RedirectToAction("Edit", "Procedures", new {Id = procedureId});
            }

            viewModel.Id = id;
            viewModel.ProcObjId = procedureId;
            _proceduresService.UpdateStepData(viewModel);

            TempData["SuccessMessage"] = "Procedure step has been updated successfully.";

            return RedirectToAction("Edit", "Procedures", new {Id = procedureId});
        }

        public ActionResult EditProcedureObject(string pid, string relationship)
        {
            var part = new EditProcedureObjectViewModel();
            ViewBag.ProcObjectId = pid;
            ViewBag.relationships = relationship;
            part.Setup(new ProceduresService());

            return View(part);
        }

        [HttpPost]
        public ActionResult EditProcedureObject(string pid, string relationship, EditProcedureObjectViewModel model)
        {
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.CreateProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureObject", new { Pid = pid, relationship = relationship });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService);

                return View(model);
            }


            model.Setup(new ProceduresService());

            return View(model);
        }

        public ActionResult ShowProcedureObject(string Pid, string relationship)
        {
            var viewModel = new EngineeringViewModel();
            ViewBag.id = Pid;
            ViewBag.relation = relationship;
            return View(viewModel);
        }

        public ActionResult ShowProcedureView(string Pid, string relation, JqGridParam param)
        {
            var totalRows = _proceduresService.GetSelectedProcedureObject(Pid, null, relation, GetCurrentUser().Id).AsQueryable();

            var orderBy = nameof(ActualPartsView.PartDesc);
            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }
            totalRows = param.sortOrder == "desc" ? totalRows.OrderByDescending(orderBy) : totalRows.OrderBy(orderBy);

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

        public ActionResult DeleteProcedureObject(string id)
        {
            var value = id;
            var data = value.Split(',');
            var ID = data[0];
            var pid = data[1];
            var relationship = data[2];

            var response = _proceduresService.Delete(id: ID, ntlogin: GetCurrentUser().Id);

            if (response)
            {
                TempData["SuccessMessage"] = "Procedure Object deleted successfully.";

                return RedirectToAction("editProcedureObject", new { Pid = pid, relationship = relationship });
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("ShowProcedureObject");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditProcedureObjectEdit(string id)
        {
            var value = id;
            var words = value.Split(',');
            var objid = words[0];
            var relationship = words[1];
            var pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = pid;

            var procedureObjectViewModel = new EditProcedureObjectViewModel();

            var model = _proceduresService.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(_proceduresService);

            return View(procedureObjectViewModel);
        }

        [HttpPost]
        public ActionResult EditProcedureObjectEdit(string Pid, string relationship, string ObjId, EditProcedureObjectViewModel model)
        {
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.SaveProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureObject", new { Pid = Pid, relationship = relationship });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService);

                return View(model);
            }


            model.Setup(_proceduresService);

            return View(model);
        }

        public ActionResult EditPartsProvideTakeBack(string id)
        {
            var value = id;
            var words = value.Split(',');
            var objid = words[0];

            var relationship = words[1];
            var pId = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = pId;

            var procedureObjectViewModel = new PartsProvideTakeBackViewModel();

            var model = _proceduresService.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(_proceduresService);

            return View(procedureObjectViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditPartsProvideTakeBack(string Pid, string relationship, string ObjId, PartsProvideTakeBackViewModel model)
        {
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;

            if (ModelState.IsValid)
            {
                var response = _proceduresService.SavePartsProvideTakeBack(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowPartsProvidedTakeBack", new { Pid, relationship });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService);

                return View(model);
            }

            model.Setup(new ProceduresService());

            return View(model);
        }

        public ActionResult CreatePartsProvideTakeBack(string Pid, string relationship)
        {
            var part = new PartsProvideTakeBackViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(_proceduresService);

            return View(part);
        }

        [HttpPost]
        public ActionResult CreatePartsProvideTakeBack(string Pid, string relationship, PartsProvideTakeBackViewModel model)
        {
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.CreatePartsProvideTakeBack(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureObject", new { Pid = Pid, relationship = relationship });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService);

                return View(model);
            }


            model.Setup(new ProceduresService());

            return View(model);
        }

        public ActionResult ProcedureConsumedCreate(string Pid, string relationship)
        {
            var part = new EditProcedureObjectViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(_proceduresService);

            return View(part);
        }

        [HttpPost]
        public ActionResult ProcedureConsumedCreate(string Pid, string relationship, EditProcedureObjectViewModel model)
        {
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.CreateProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Consumed has been created successfully.";

                    return RedirectToAction("ShowProcedureConsumed", new { Pid = Pid, relationship = relationship });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService);

                return View(model);
            }


            model.Setup(_proceduresService);

            return View(model);
        }

        public ActionResult ShowProcedureConsumed(string Pid, string relationship)
        {
            var viewModel = new EngineeringViewModel();
            ViewBag.id = Pid;
            ViewBag.relation = relationship;
            return View(viewModel);
        }

        public ActionResult ShowProcedureCondumedView(string Pid, string relation, JqGridParam param)
        {
            var totalRows = _proceduresService.GetSelectedProcedureObject(Pid, null, relation, GetCurrentUser().Id).AsQueryable();

            var orderBy = nameof(ActualPartsView.PartDesc);
            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }
            totalRows = param.sortOrder == "desc" ? totalRows.OrderByDescending(orderBy) : totalRows.OrderBy(orderBy);

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

        public ActionResult DeleteProcedureConsumed(string id)
        {
            var value = id;
            var data = value.Split(',');
            var ID = data[0];
            var Pid = data[1];
            var relationship = data[2];

            var response = _proceduresService.Delete(id: ID, ntlogin: GetCurrentUser().Id);

            if (response)
            {
                TempData["SuccessMessage"] = "Procedure Object deleted successfully.";

                return RedirectToAction("ShowProcedureConsumed", new { Pid = Pid, relationship = relationship });
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("ShowProcedureConsumed");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ProcedureConsumedEdit(string ID)
        {
            var value = ID;
            var words = value.Split(',');
            var objid = words[0];
            var relationship = words[1];
            var Pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = Pid;

            var procedureObjectViewModel = new EditProcedureObjectViewModel();

            var model = _proceduresService.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(_proceduresService);

            return View(procedureObjectViewModel);
        }

        [HttpPost]
        public ActionResult ProcedureConsumedEdit(string Pid, string relationship, string ObjId, EditProcedureObjectViewModel model)
        {

            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.SaveProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureConsumed", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(_proceduresService);

                    return View(model);
                }
            }

            model.Setup(_proceduresService);

            return View(model);
        }

        public ActionResult CreatePartsProductactivity(string Pid, string relationship)
        {
            var part = new EditProcedureObjectViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(_proceduresService);

            return View(part);
        }
        [HttpPost]
        public ActionResult CreatePartsProductactivity(string Pid, string relationship, EditProcedureObjectViewModel model)
        {
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.CreateProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Part Stay has been created successfully.";

                    return RedirectToAction("ShowProductPartStay", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(_proceduresService);

                    return View(model);
                }
            }


            model.Setup(_proceduresService);

            return View(model);
        }

        public ActionResult ShowProductPartStay(string Pid, string relationship)
        {
            var viewModel = new EngineeringViewModel();
            ViewBag.id = Pid;
            ViewBag.relation = relationship;
            return View(viewModel);
        }

        public ActionResult ShowProductPartStayView(string Pid, string relation, JqGridParam param)
        {
            var totalRows = _proceduresService.GetSelectedProcedureObject(Pid, null, relation, GetCurrentUser().Id).AsQueryable();

            var orderBy = nameof(ActualPartsView.PartDesc);
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

        public ActionResult DeleteProductPartStay(string id)
        {
            var value = id;
            var data = value.Split(',');
            var ID = data[0];
            var Pid = data[1];
            var relationship = data[2];

            var response = _proceduresService.Delete(id: ID, ntlogin: GetCurrentUser().Id);

            if (response)
            {
                TempData["SuccessMessage"] = "Procedure Part Stay deleted successfully.";

                return RedirectToAction("CreatePartsProductactivity", new { Pid = Pid, relationship = relationship });
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("ShowProductPartStay");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditProductPartStay(string ID)
        {
            var value = ID;
            var words = value.Split(',');
            var objid = words[0];
            var relationship = words[1];
            var Pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = Pid;

            var procedureObjectViewModel = new EditProcedureObjectViewModel();

            var model = _proceduresService.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(_proceduresService);

            return View(procedureObjectViewModel);
        }
        [HttpPost]
        public ActionResult EditProductPartStay(string Pid, string relationship, string ObjId, EditProcedureObjectViewModel model)
        {
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = _proceduresService.SaveProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Part Stay has been edited successfully.";

                    return RedirectToAction("ShowProductPartStay", new { Pid = Pid, relationship = relationship });
                }

                TempData["ErrorMessage"] = "Something went wrong.";

                model.Setup(_proceduresService);

                return View(model);
            }

            model.Setup(_proceduresService);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteMonitor(string id, string stepId)
        {
            var result = _proceduresService.DeleteMonitor(id, GetCurrentUser().Id);

            if (result)
            {
                var moniter = _proceduresService.GetMoniterByStepId(stepId, GetCurrentUser().Id);

                return PartialView("Partials/_InnerMoniter", moniter);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error with the request");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReorderSteps(string procObjectId, string[] array)
        {
            var currentUser = GetCurrentUser();

            var stepsToOrders = new Dictionary<string, double?>();

            double index = 0;
            foreach (var step in array.Where(x=> !string.IsNullOrWhiteSpace(x)))
            {
                var stepParts = step.Split(',');

                stepsToOrders.Add(stepParts[1], index);

                index++;
            }

            _proceduresService.SaveReorderSteps(stepsToOrders, procObjectId, currentUser.Id);

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMonitor(string relatedObject, string stepId)
        {
            var model = new GetStepDataResult
            {
                AddMonitorForProcedureViewModel =
                {
                    Related_Object_Id = relatedObject,
                    Step_Id = stepId
                }
            };
            model.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());
            return PartialView("_Monitor", model);
        }

        public ActionResult EditMonitor(string objectId)
        {
            var currentUser = GetCurrentUser();

            var result = _proceduresService.EditMonitorSteps(objectId, currentUser.Id);

            var model = new GetStepDataResult
            {
                AddMonitorForProcedureViewModel =
                {
                    Id = objectId,
                    Monitor_Type = result.Monitor_Type,
                    Input_Type = result.Input_Type,
                    Fail_Action = result.Fail_Action,
                    Description = result.Description,
                    Related_Object_Id = result.Related_Object_Id,
                    Step_Id = result.Step_Id,
                    Should_Be = result.Should_Be,
                    Highest_Threshold =result.Highest_Threshold,
                    Lowest_Threshold =result.Lowest_Threshold,
                    Target_Object =result.Target_Object,
                    Text_Target =result.Text_Target,
                    Target = result.Target,
                    Correct_Answer = result.Correct_Answer,
                    YES_NO_ANSWER = result.YES_NO_ANSWER
                }
            };

            if (result.Monitor_Type == "NUMBER" && result.Should_Be == "BETWEEN")
            {
                model.AddMonitorForProcedureViewModel.Highest_Threshold = result.Highest_Threshold;
                model.AddMonitorForProcedureViewModel.Lowest_Threshold = result.Lowest_Threshold;
            }
            else if (result.Monitor_Type == "NUMBER" && result.Should_Be != "BETWEEN" && result.Target.HasValue)
            {
                model.AddMonitorForProcedureViewModel.Target_Object = result.Target.Value.ToString();
            }
            else if (result.Monitor_Type == "EQUIPMENT" && result.Target.HasValue)
            {
                model.AddMonitorForProcedureViewModel.Target_Object = result.Target.Value.ToString();
            }
            else if (result.Monitor_Type == "TEXT")
            {
                model.AddMonitorForProcedureViewModel.Target_Object = result.Text_Target;
            }
            else if (result.Monitor_Type == "YES_NO")
            {
                model.AddMonitorForProcedureViewModel.Target_Object = result.YES_NO_ANSWER?.ToString() ?? "";
            }
            else if (result.Monitor_Type == "PASS_FAIL")
            {
                model.AddMonitorForProcedureViewModel.Target_Object = result.Target_Object;
            }

            model.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());

            return PartialView("_Monitor", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveMonitor(GetStepDataResult model)
        {
            if (ModelState.IsValid)
            {
                model.AddMonitorForProcedureViewModel.StrNTLogin = GetCurrentUser().Id;

                if (model.AddMonitorForProcedureViewModel.Monitor_Type != "NUMBER")
                {
                    model.AddMonitorForProcedureViewModel.Should_Be = "EQUAL";
                    model.AddMonitorForProcedureViewModel.Highest_Threshold = null;
                    model.AddMonitorForProcedureViewModel.High_Threshold = null;
                    model.AddMonitorForProcedureViewModel.Target = null;
                    model.AddMonitorForProcedureViewModel.Low_Threshold = null;
                    model.AddMonitorForProcedureViewModel.Lowest_Threshold = null;
                }

                var response = _proceduresService.AddMonitorForProcedure(model.AddMonitorForProcedureViewModel);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = model.AddMonitorForProcedureViewModel.Id == null
                        ? "Monitor has been added successfully."
                        : "Monitor has been Updated successfully.";

                    var moniter = _proceduresService.GetMoniterByStepId(model.AddMonitorForProcedureViewModel.Step_Id, GetCurrentUser().Id);
                    return PartialView("Partials/_InnerMoniter", moniter);
                }
            }

            return Content("error");
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile)
        {
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                var currentUser = GetCurrentUser();

                var response = _proceduresService.ImportProcedures(postedFile, currentUser);

                if (response.HasErrors())
                {
                    TempData[NotificationConstants.ErrrorMessage] = response.ErrorMessage;
                }
                else
                {
                    if (response.Entity.Any(x => !x.Processed))
                    {
                        TempData[NotificationConstants.WarningMessage] = "File has been processed with errors";
                    }
                    else
                    {
                        TempData[NotificationConstants.SuccessMessage] = "File has been processed successfully";
                    }
                }

                return View(response.Entity);
            }

            TempData[NotificationConstants.ErrrorMessage] = "Please upload a file";

            return View();
        }

        public void ImportSampleFile()
        {
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=procedures-upload.csv");
            Response.Write(string.Join(",", ProcedureImportViewModel.GetHeaderColumns()));
            Response.End();
        }
    }

}