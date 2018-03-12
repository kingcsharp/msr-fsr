using Msr.Models.Procedures;
using Msr.Services.jqGrid;
using Msr.Services.Procedures;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;
using Amazon.Runtime.Internal;
using Answer.Web.ViewModel;
using Msr.Models.ActualParts;
using Msr.Services.Procedures.Messages;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProcedureVerbs;
using Msr.Services.Roles;
using Msr.Services.Users;
using Msr.Services.Workflows;
using Msr.Services.EquipmentMaintenances;

namespace Answer.Web.Controllers
{
    public class ProceduresController : BaseController
    {
        private readonly ProceduresService _proceduresService;
        private readonly UserService _userService;
        private readonly WorkflowService _workflowService;

        const string AppDataGlobalsettingsXml = @"/App_Data//procedureFolder/";
        const string XSLTPath = @"/assets//styles//xslt//procedures.xsl";

        public ProceduresController()
        {
            _proceduresService = new ProceduresService();
            _userService = new UserService();
            _workflowService = new WorkflowService();
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

        public ActionResult ProceduresData(JqGridParam param)
        {
            var procedureService = new ProceduresService();

            var totalRows = procedureService.GetProceduresQueryable().Where(x => x.Status != "DELETED" && x.Status != "OLD");

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
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
                        if (Int32.TryParse(rule.data, out value))
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
            var saveProcedureViewModel = new SaveProcedureViewModel();

            saveProcedureViewModel.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService(), GetCurrentUser().Id);

            return View(saveProcedureViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SaveProcedureViewModel model)
        {
            var procedureService = new ProceduresService();

            model.NTLogin = GetCurrentUser().Id;

            if (ModelState.IsValid)
            {
                var response = procedureService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Procedure has been created successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService(), GetCurrentUser().Id);

                    return View(model);
                }
            }


            model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService(), GetCurrentUser().Id);

            return View(model);
        }


        public ActionResult ProcedureDelete(string id)
        {
            var procedureService = new ProceduresService();

            var response = procedureService.DeleteProcedure(id: id, ntlogin: GetCurrentUser().Id);

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
            vm.Id = id;

            var currrentUser = GetCurrentUser();
            var result = _workflowService.CheckOutObject(id, currrentUser.Id);
            id = result.Entity;

            var model = _proceduresService.GetProcedureById(id);

            vm.MapToDto(model);
            vm.Setup(_proceduresService, new RoleService(), new ProcedureVerbsService(), currrentUser.Id);

            var procedureName = _proceduresService.GetProceduresQueryable().Where(x => x.ObjectId == id).SingleOrDefault().Name;

            var viewModel = new GetStepDataResult
            {
                GetStepDataResults = _proceduresService.GetStepsData(id, currrentUser.Id)
            };

            viewModel.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());
            viewModel.AddMonitorForProcedureViewModel.Related_Object_Id = id;
            ViewBag.ProcObjectId = id;
            ViewBag.ProdecureName = procedureName;

            ViewBag.Procedure = new SelectList(_proceduresService.GetProcedurelist(), "Value", "Show");

            vm.GetStepDataResults = viewModel;
            return View(vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(SaveProcedureViewModel model, string command)
        {

            var procedureService = new ProceduresService();

            model.NTLogin = GetCurrentUser().Id;

            if (ModelState.IsValid)
            {
                if (command == "Update")
                {
                    var response = procedureService.Save(model);
                    if (response)
                    {
                        TempData["SuccessMessage"] = "Procedure has been updated successfully.";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong.";

                        model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService(), GetCurrentUser().Id);

                        return View("Edit", model);
                    }
                }
                else if (command == "Cancel And Roll Back")
                {
                    var response = procedureService.RollBack(model);
                    if (response)
                    {
                        TempData["SuccessMessage"] = "Procedure has been canceled and roll back successfully.";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong.";

                        model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService(),
                            GetCurrentUser().Id);

                        return View("Edit", model);
                    }
                }

            }

            model.Setup(new ProceduresService(), new RoleService(), new ProcedureVerbsService(), GetCurrentUser().Id);

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

            vm.Setup(new UserService());

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
            var procedureName = _proceduresService.GetProceduresQueryable().Where(x => x.ObjectId == id).SingleOrDefault().Name;

            var viewModel = new GetStepDataResult
            {
                GetStepDataResults = _proceduresService.GetStepsData(id, GetCurrentUser().Id)
            };
            viewModel.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());
            viewModel.AddMonitorForProcedureViewModel.Related_Object_Id = id;
            ViewBag.ProcObjectId = id;
            ViewBag.ProdecureName = procedureName;

            ViewBag.Procedure = new SelectList(_proceduresService.GetProcedurelist(), "Value", "Show");


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SaveStep(string ProcObjectId, string Procedure)
        {
            var id = _proceduresService.PrePopSave(ProcObjectId, Procedure, GetCurrentUser().Id);

            ViewBag.Id = id;
            ViewBag.Procedure = Procedure;
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
            vm.SetUp(new ProceduresService(), new ProcedureVerbsService(), ProcObjectId);

            var singleOrDefault = _proceduresService.GetProcedurelist().SingleOrDefault(x => x.Value == Procedure);
            if (singleOrDefault != null)
            {
                var value = singleOrDefault.Show;

                vm.GetStepEditData.Step_Text = value;
                vm.ProcObjId = ProcObjectId;
            }
            var response = _proceduresService.CreateStepData(vm);

            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = "Step has been Created successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong.";
            }


            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
     

        public ActionResult CreateStep(string procedureObjectId, string NewObjectId, string Procedure)
        {
            var currentUser = GetCurrentUser();

            var vm = new GetStepEditDataViewModel();
            vm.NtLogin = currentUser.Login;

            vm.SetUp(new ProceduresService(), new ProcedureVerbsService(), procedureObjectId);

            var singleOrDefault = _proceduresService.GetProcedurelist().SingleOrDefault(x => x.Value == Procedure);
            if (singleOrDefault != null)
            {
                var value = singleOrDefault.Show;

                vm.GetStepEditData.Step_Text = value;
                vm.ProcObjId = procedureObjectId;
            }

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
            var vm = _proceduresService.GetStepData(stepId, procedureObjectId, GetCurrentUser().Id);

            var currentUser = GetCurrentUser();
            vm.NtLogin = currentUser.Login;

            vm.SetUp(new ProceduresService(), new ProcedureVerbsService(), procedureObjectId);

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditStep(GetStepDataResult viewModel, string procstepId, string Step_Text, string Id)
        {
            viewModel.Id = Id;
            viewModel.Step_Text = Step_Text;
            viewModel.ProcObjId = procstepId;
            _proceduresService.UpdateStepData(viewModel);

            TempData["SuccessMessage"] = "Procedure Step been updated successfully.";
            return RedirectToAction("Edit", "Procedures", new { Id = procstepId });
        }



        public ActionResult editProcedureObject(string Pid, string relationship)
        {
            var part = new EditProcedureObjectViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(new ProceduresService());

            return View(part);
        }
        [HttpPost]
        public ActionResult editProcedureObject(string Pid, string relationship, EditProcedureObjectViewModel model)
        {
            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.CreateProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureObject", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
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
            var ProceduresService = new ProceduresService();

            var totalRows = ProceduresService.GetSelectedProcedureObject(Pid, null, relation, GetCurrentUser().Id).AsQueryable();

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

        public ActionResult DeleteProcedureObject(string id)
        {
            string value = id;
            string[] data = value.Split(',');
            string ID = data[0];
            string Pid = data[1];
            string relationship = data[2];
            var taskService = new ProceduresService();

            var response = taskService.Delete(id: ID, ntlogin: GetCurrentUser().Id);

            if (response)
            {
                TempData["SuccessMessage"] = "Procedure Object deleted successfully.";

                return RedirectToAction("editProcedureObject", new { Pid = Pid, relationship = relationship });
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("ShowProcedureObject");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditProcedureObjectEdit(string ID)
        {
            string value = ID;
            string[] words = value.Split(',');
            string objid = words[0];
            string relationship = words[1];
            string Pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = Pid;
            var procedureservices = new ProceduresService();

            var procedureObjectViewModel = new EditProcedureObjectViewModel();

            var model = procedureservices.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(new ProceduresService());

            return View(procedureObjectViewModel);
        }
        [HttpPost]
        public ActionResult EditProcedureObjectEdit(string Pid, string relationship, string ObjId, EditProcedureObjectViewModel model)
        {
            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.SaveProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureObject", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService());

            return View(model);
        }

        public ActionResult EditPartsProvideTakeBack(string id)
        {
            string value = id;
            string[] words = value.Split(',');
            string objid = words[0];

            string relationship = words[1];
            string Pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = Pid;
            var preProServices = new ProceduresService();

            var procedureObjectViewModel = new PartsProvideTakeBackViewModel();

            var model = preProServices.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(new ProceduresService());

            return View(procedureObjectViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditPartsProvideTakeBack(string Pid, string relationship, string ObjId, PartsProvideTakeBackViewModel model)
        {
            var procedureService = new ProceduresService();
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;

            if (ModelState.IsValid)
            {
                var response = procedureService.SavePartsProvideTakeBack(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowPartsProvidedTakeBack", new { Pid, relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService());

            return View(model);
        }
        public ActionResult CreatePartsProvideTakeBack(string Pid, string relationship)
        {
            var part = new PartsProvideTakeBackViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(new ProceduresService());

            return View(part);
        }
        [HttpPost]
        public ActionResult CreatePartsProvideTakeBack(string Pid, string relationship, PartsProvideTakeBackViewModel model)
        {
            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.CreatePartsProvideTakeBack(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureObject", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService());

            return View(model);
        }


        public ActionResult ProcedureConsumedCreate(string Pid, string relationship)
        {
            var part = new EditProcedureObjectViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(new ProceduresService());

            return View(part);
        }
        [HttpPost]
        public ActionResult ProcedureConsumedCreate(string Pid, string relationship, EditProcedureObjectViewModel model)
        {
            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.CreateProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Consumed has been created successfully.";

                    return RedirectToAction("ShowProcedureConsumed", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService());

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
            var ProceduresService = new ProceduresService();

            var totalRows = ProceduresService.GetSelectedProcedureObject(Pid, null, relation, GetCurrentUser().Id).AsQueryable();

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

        public ActionResult DeleteProcedureConsumed(string id)
        {
            string value = id;
            string[] data = value.Split(',');
            string ID = data[0];
            string Pid = data[1];
            string relationship = data[2];
            var taskService = new ProceduresService();

            var response = taskService.Delete(id: ID, ntlogin: GetCurrentUser().Id);

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
            string value = ID;
            string[] words = value.Split(',');
            string objid = words[0];
            string relationship = words[1];
            string Pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = Pid;
            var procedureservices = new ProceduresService();

            var procedureObjectViewModel = new EditProcedureObjectViewModel();

            var model = procedureservices.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(new ProceduresService());

            return View(procedureObjectViewModel);
        }
        [HttpPost]
        public ActionResult ProcedureConsumedEdit(string Pid, string relationship, string ObjId, EditProcedureObjectViewModel model)
        {
            var preProServices = new ProceduresService();

            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.SaveProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Object has been created successfully.";

                    return RedirectToAction("ShowProcedureConsumed", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }

            model.Setup(new ProceduresService());

            return View(model);
        }

        public ActionResult CreatePartsProductactivity(string Pid, string relationship)
        {
            var part = new EditProcedureObjectViewModel();
            ViewBag.ProcObjectId = Pid;
            ViewBag.relationships = relationship;
            part.Setup(new ProceduresService());

            return View(part);
        }
        [HttpPost]
        public ActionResult CreatePartsProductactivity(string Pid, string relationship, EditProcedureObjectViewModel model)
        {
            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.CreateProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Part Stay has been created successfully.";

                    return RedirectToAction("ShowProductPartStay", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }


            model.Setup(new ProceduresService());

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
            var ProceduresService = new ProceduresService();

            var totalRows = ProceduresService.GetSelectedProcedureObject(Pid, null, relation, GetCurrentUser().Id).AsQueryable();

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

        public ActionResult DeleteProductPartStay(string id)
        {
            string value = id;
            string[] data = value.Split(',');
            string ID = data[0];
            string Pid = data[1];
            string relationship = data[2];
            var taskService = new ProceduresService();

            var response = taskService.Delete(id: ID, ntlogin: GetCurrentUser().Id);

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
            string value = ID;
            string[] words = value.Split(',');
            string objid = words[0];
            string relationship = words[1];
            string Pid = words[2];

            ViewBag.ObjId = objid;
            ViewBag.relation = relationship;
            ViewBag.id = Pid;
            var preProServices = new ProceduresService();

            var procedureObjectViewModel = new EditProcedureObjectViewModel();

            var model = preProServices.EditProcedureObject(id: objid, ntlogin: GetCurrentUser().Id);

            procedureObjectViewModel = procedureObjectViewModel.MapToDto(model);

            procedureObjectViewModel.Setup(new ProceduresService());

            return View(procedureObjectViewModel);
        }
        [HttpPost]
        public ActionResult EditProductPartStay(string Pid, string relationship, string ObjId, EditProcedureObjectViewModel model)
        {
            var procedureService = new ProceduresService();
            //need to be dynamic
            model.NTLogin = GetCurrentUser().Id;
            model.PROCEDURE_ID = Pid;
            model.ID = ObjId;
            model.RELATIONSHIP = relationship;
            if (ModelState.IsValid)
            {
                var response = procedureService.SaveProcedureObject(model);
                if (response)
                {
                    TempData["SuccessMessage"] = "Procedure Part Stay has been edited successfully.";

                    return RedirectToAction("ShowProductPartStay", new { Pid = Pid, relationship = relationship });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    model.Setup(new ProceduresService());

                    return View(model);
                }
            }

            model.Setup(new ProceduresService());

            return View(model);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult DeleteStep(string id, string procStepId, string ProdecureName)
        {
            var proceduresService = new ProceduresService();

            var result = proceduresService.DeleteStep(id, GetCurrentUser().Id);
            if (result)
            {
                TempData["SuccessMessage"] = "Step has been deleted successfully.";

                return Json("Ok", JsonRequestBehavior.AllowGet);
            }

            TempData["ErrorMessage"] = "Something went wrong.";

            return RedirectToAction("Steps", "Procedures", new { id = procStepId, ProdecureName = ProdecureName });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ReorderCustom(string id)
        {
            var model = _proceduresService.GetReorderSteps(procObjectId: id);

            ViewBag.procObjectId = id;

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReorderCustom(string procObjectId, IList<ProcedureStepOtherStepListView> steps)
        {
            var currentUser = GetCurrentUser();
            var stringArray = steps.Select(item => item.print_Order + "," + item.Id).ToList();

            _proceduresService.SaveReorderSteps(stringArray.ToArray(), procObjectId, currentUser.Id);

            return RedirectToAction("Edit", new { id = procObjectId });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReorderSteps(string procObjectId, string[] array)
        {
            var currentUser = GetCurrentUser();

            _proceduresService.SaveReorderSteps(array, procObjectId, currentUser.Id);

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMonitor(string moniterType, string inputType, string failAction, string description, string objectId, string relatedObject, string stepId, string procedureName, string shouldBe, float? highestThreshold, float? highThreshold, float? target, float? lowThreshold, float? lowestThreshold, string targetObject)
        {
            var model = new GetStepDataResult
            {
                AddMonitorForProcedureViewModel =
                {
                    Id = objectId,
                    Monitor_Type = moniterType,
                    Input_Type = inputType,
                    Fail_Action = failAction,
                    Description = description ?? "",
                    Related_Object_Id = relatedObject,
                    Step_Id = stepId,
                    ProcedureName = procedureName,
                    Should_Be = shouldBe,
                    Highest_Threshold =highestThreshold,
                    High_Threshold =highThreshold,
                    Target= target,
                    Low_Threshold =lowThreshold,
                    Lowest_Threshold =lowestThreshold,
                    Target_Object =targetObject

                }
            };
            ViewBag.ProcedureName = procedureName;
            model.AddMonitorForProcedureViewModel.Setup(new EquipmentMaintenanceService());
            return PartialView("_Monitor", model);
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult SaveMonitor(GetStepDataResult model)
        {
            var proceduresService = new ProceduresService();
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

                var response = proceduresService.AddMonitorForProcedure(model: model.AddMonitorForProcedureViewModel);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = model.AddMonitorForProcedureViewModel.Id == null
                        ? "Monitor has been added successfully."
                        : "Monitor has been Updated successfully.";


                    return RedirectToAction(actionName: "Edit", controllerName: "Procedures", routeValues: new { id = model.AddMonitorForProcedureViewModel.Related_Object_Id });
                }

                TempData["ErrorMessage"] = "Something went wrong.";
                return RedirectToAction("Edit", "Procedures", new
                {
                    id = model.AddMonitorForProcedureViewModel.Related_Object_Id
                });
            }

            return RedirectToAction("Edit", "Procedures", new
            {
                id = model.AddMonitorForProcedureViewModel.Related_Object_Id

            });
        }
    }

}