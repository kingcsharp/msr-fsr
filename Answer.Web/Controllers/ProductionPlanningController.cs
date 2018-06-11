using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Infrastructure.Common.Constansts;
using Msr.Services.jqGrid;
using Msr.Services.ProductionPlanning;
using Msr.Models.CustomerRequirements;
using Msr.Services;
using Msr.Services.AdminCostSettings;
using Msr.Services.Documents;
using Msr.Services.Parts;
using Msr.Services.Parts.ViewModels;
using Msr.Services.PartTypes;
using Msr.Services.PrePro;
using Msr.Services.Procedures;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Services.Quotes;
using Msr.Services.Roles;
using Msr.Services.Workflows;
using Msr.Services.Workflows.ViewModels;
using Msr.Services.Products;

namespace Answer.Web.Controllers
{
    public class ProductionPlanningController : BaseController
    {
        private readonly ProductionPlanningService _productionPlanService;
        private readonly ProceduresService _proceduresService;
        private readonly QuoteService _quoteService;
        private readonly WorkflowService _workflowService;
        private readonly PreProServices _preProServices;
        private readonly PartsService _partsService;
        private readonly RoleService _roleService;
        private readonly PartTypeService _partTypeService;
        private readonly DocumentFilesService _documentFilesService;
        private readonly AdminCostSettingService _adminCostSettingService;
        private readonly ProductService _productService;

        public ProductionPlanningController()
        {
            _adminCostSettingService = new AdminCostSettingService();
            _proceduresService = new ProceduresService();
            _productionPlanService = new ProductionPlanningService();
            _quoteService = new QuoteService();
            _workflowService = new WorkflowService();
            _preProServices = new PreProServices();
            _partsService = new PartsService();
            _partTypeService = new PartTypeService();
            _documentFilesService = new DocumentFilesService();
            _productService = new ProductService();
        }

        public ActionResult Index()
        {
            ViewBag.ActiveClass = "ProductionPlanning";
            return View();
        }

        public ActionResult ProductionPlanningData(JqGridParam param)
        {

            var totalRows = _productionPlanService.GetProductionPlaningQueryable().Where(x =>
                x.Status != "DELETED" && x.Status != "OLD");

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(CustomerRequirementView.Company))
                    {
                        totalRows = totalRows.Where(x => x.Company.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.SubmittedDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.SubmittedDate.Day == value.Day &&
                                                             q.SubmittedDate.Month == value.Month &&
                                                             q.SubmittedDate.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(CustomerRequirementView.Division))
                    {
                        totalRows = totalRows.Where(x => x.Division.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.SubmittedBy))
                    {
                        totalRows = totalRows.Where(x => x.SubmittedBy.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.PartKitNo))
                    {
                        totalRows = totalRows.Where(x => x.PartKitNo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.ProductName))
                    {
                        totalRows = totalRows.Where(x => x.ProductName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.Respresentative))
                    {
                        totalRows = totalRows.Where(x => x.Respresentative.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.ProcedureName))
                    {
                        totalRows = totalRows.Where(x => x.ProcedureName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            if (statusList.Any(x => x.Contains("received")))
                            {
                                totalRows = totalRows.Where(x => x.Status == "creating" && x.IsProduct == false);
                            }
                            else if (statusList.Any(x => x.Contains("creating")))
                            {
                                totalRows = totalRows.Where(x => x.Status == "creating" && x.IsProduct);
                            }
                            else
                            {
                                totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                            }
                        }
                    }
                }
            }

            var orderBy = nameof(CustomerRequirementView.Company);

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

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var vm = new RequirementStepsViewModel();

            var currentUser = GetCurrentUser();

            vm.AdminCostSettings = _adminCostSettingService.GetAdminCostSettings();
            var requirment = _quoteService.GetCustomerRequirementView(id);
            var productsView = _productService.GetProducts().SingleOrDefault(x => x.Object_Id == id.ToString());

            vm.Read(_productionPlanService, productsView, requirment);

            var procedureObjectId = "";

            if (!string.IsNullOrWhiteSpace(vm.ProductProcedureId))
            {
                vm.OldProductProcedureId = vm.ProductProcedureId;
                procedureObjectId = _productionPlanService.GetProceduretById(vm.ProductProcedureId).Value;
            }

            vm.Setup(_productionPlanService, _preProServices, currentUser);

            var procedureSteps = _proceduresService.GetStepsData(procedureObjectId, currentUser.Id);

            if (!string.IsNullOrWhiteSpace(vm.ProductProcedureId))
            {
                foreach (var step in procedureSteps)
                {
                    vm.Steps.Add(new RequirementStepsDetailsViewModel
                    {
                        Id = Convert.ToInt32(step.Id),
                        ObjectId = step.Id,
                        Process = step.Title,
                        StepTitle = step.Title,
                        Step = (int)step.Print_Order,
                        StandardDirectLaborMinutes = step.Duration,
                        StandardMachineMinutes = step.EquipmentTime,
                        ReplacementCost = step.ReplacementCost,
                        Utilization = step.Utilization,
                        UsefulLife = step.UsefulLife,
                    });
                }
            }

            return View(vm);
        }

        public ActionResult View(int id)
        {
            var currentUser = GetCurrentUser();
            var requirment = _quoteService.GetCustomerRequirementView(id);

            var viewModel = new RequirementStepsViewModel();

            viewModel.AdminCostSettings = _adminCostSettingService.GetAdminCostSettings();

            var productsView = _productService.GetProducts().SingleOrDefault(x => x.Object_Id == id.ToString());

            viewModel.Read(_productionPlanService, productsView, requirment);

            var procedureObjectId = _productionPlanService.GetProceduretById(viewModel.ProductProcedureId).Value;
            var procedureSteps = _proceduresService.GetStepsData(procedureObjectId, currentUser.Id);
            viewModel.Setup(_productionPlanService, _preProServices, currentUser);

            if (!string.IsNullOrWhiteSpace(viewModel.ProductProcedureId) && !viewModel.Steps.Any())
            {
                foreach (var step in procedureSteps)
                {
                    viewModel.Steps.Add(new RequirementStepsDetailsViewModel
                    {
                        Id = Convert.ToInt32(step.Id),
                        ObjectId = step.Id,
                        Process = step.Title,
                        StepTitle = step.Title,
                        Step = (int)step.Print_Order,
                        StandardDirectLaborMinutes = step.Duration,
                        StandardMachineMinutes = step.EquipmentTime,
                        ReplacementCost = step.ReplacementCost,
                        Utilization = step.Utilization,
                        UsefulLife = step.UsefulLife
                    });
                }
            }

            viewModel.ProductName = requirment.ProductName;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(RequirementStepsViewModel vm, string saveSubmit, bool isNewProcedure = false)
        {
            var currentUser = GetCurrentUser();
            ModelState.Clear();

            if (isNewProcedure)
            {
                vm.ProductProcedureId = null;
                vm.NewProcedure = true;
                vm.Steps = new List<RequirementStepsDetailsViewModel>
                {
                    new RequirementStepsDetailsViewModel { Step = 1}
                };
                vm.Setup(_productionPlanService, _preProServices, currentUser);

                return View(vm);
            }

            if (!vm.NewProcedure && string.IsNullOrWhiteSpace(vm.ProductProcedureId))
            {
                ModelState.AddModelError(nameof(vm.ProductProcedureId), "Procedure is required");
            }

            if (vm.ProductProcedureId == vm.OldProductProcedureId || vm.NewProcedure)
            {
                foreach (var item in vm.Steps)
                {
                    if (item.Process == null)
                    {
                        ModelState.AddModelError(nameof(item.Process), "Process is required");
                    }

                    if (item.StandardDirectLaborMinutes == null)
                    {
                        ModelState.AddModelError(nameof(item.StandardDirectLaborMinutes),
                            "Standard DirectLabor Minutes is required");
                    }

                    if (item.StandardMachineMinutes == null)
                    {
                        ModelState.AddModelError(nameof(item.StandardDirectLaborMinutes),
                            "Standard MachineMinutes is required");
                    }

                    if (vm.Steps.Count < 0)
                    {
                        ModelState.AddModelError(nameof(item.StandardDirectLaborMinutes), "Step is required");
                    }
                }
            }

            vm.AdminCostSettings = _adminCostSettingService.GetAdminCostSettings();

            if (!string.IsNullOrWhiteSpace(vm.ProductProcedureId) && vm.ProductProcedureId != vm.OldProductProcedureId)
            {
                vm.OldProductProcedureId = vm.ProductProcedureId;

                var procedureObjectId = _productionPlanService.GetProceduretById(vm.ProductProcedureId).Value;
                var procedureSteps = _proceduresService.GetStepsData(procedureObjectId, currentUser.Id);

                vm.Steps = new List<RequirementStepsDetailsViewModel>();

                foreach (var step in procedureSteps)
                {
                    vm.Steps.Add(new RequirementStepsDetailsViewModel
                    {
                        Id = Convert.ToInt32(step.Id),
                        ObjectId = step.Id,
                        Process = step.Title,
                        Step = (int)step.Print_Order,
                        StandardDirectLaborMinutes = step.Duration,
                        StandardMachineMinutes = step.EquipmentTime,
                        ReplacementCost = step.ReplacementCost,
                        Utilization = step.Utilization,
                        UsefulLife = step.UsefulLife
                    });
                }

                vm.Setup(_productionPlanService, _preProServices, currentUser);

                return View(vm);
            }
      
            if (ModelState.IsValid)
            {
                vm.LoginId = currentUser.Id;

                var response = _productionPlanService.Save(vm, saveSubmit, currentUser);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    if (saveSubmit == "SaveSubmit")
                    {
                        return RedirectToAction("Submit", "Workflow",
                            new { objId = response.Entity.PObjectId, returnUrl = Url.Content("~/ProductionPlanning") });
                    }

                    vm.Setup(_productionPlanService, _preProServices, currentUser);

                    return RedirectToAction("Index");

                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            vm.Setup(_productionPlanService, _preProServices, currentUser);

            return View(vm);
        }

        public ActionResult Status(int id, string currentStatus)
        {

            var response = _productionPlanService.ChangeStatus(id, currentStatus);

            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = response.SuccessMessage;
            }

            TempData["ErrorMessage"] = response.ErrorMessage;

            return null;
        }

        public ActionResult EditStatus(string id, string currentStatus)
        {
            RequirementStatusViewModel model = new RequirementStatusViewModel();

            model.Id = id;

            model.Status = currentStatus;

            model.Setup();

            return View(model);
        }

        [HttpGet]
        public ActionResult AddPart()
        {
            var currentUser = GetCurrentUser();

            var vm = new AddPartViewModel();
            vm.Setup(_documentFilesService, _partsService, _partTypeService, currentUser.Company, currentUser.Id);

            return PartialView("_Parts", vm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPart(AddPartViewModel model)
        {
            var currentUser = GetCurrentUser();

            var result = new ResultNotification<string>();
            var partservice = new PartsService();

            var response = partservice.Create(model);

            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = "Part has been created successfully.";

                var checkOutObject = _workflowService.CheckOutObject(response.Entity, currentUser.Id);

                var submitWorkflow = new SubmitWorkflowViewModel();
                submitWorkflow.CompletionStart = "APPROVED";
                submitWorkflow.LoggedUserIdResult = currentUser;
                submitWorkflow.ObjectId = checkOutObject.Entity;
                submitWorkflow.ApprovalWorflowId = "37";
                submitWorkflow.Comment = "Part approved by system";
                submitWorkflow.LoginId = currentUser.Id;
                _workflowService.SubmitWorkflow(submitWorkflow);

                var partList = _productionPlanService.GetPartList();

                return Json(new { Message = result.SuccessMessage, data = partList, PartId = response.Entity }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Message = result.ErrorMessage }, JsonRequestBehavior.AllowGet);

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

                var response = _productionPlanService.ImportProducts(postedFile, currentUser);

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
            Response.AddHeader("Content-Disposition", "attachment;filename=products-upload.csv");
            Response.Write(string.Join(",", ProductImportViewModel.GetHeaderColumns()));

        }
    }
}