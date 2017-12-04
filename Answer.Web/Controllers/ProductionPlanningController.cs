using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.jqGrid;
using Msr.Services.ProductionPlanning;
using Msr.Models.CustomerRequirements;
using Msr.Services.Procedures;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Services.Quotes;

namespace Answer.Web.Controllers
{
    public class ProductionPlanningController : BaseController
    {
        private readonly ProductionPlanningService _productionPlanService;
        private readonly ProceduresService _proceduresService;
        private readonly QuoteService _quoteService;

        public ProductionPlanningController()
        {
            _proceduresService = new ProceduresService();
            _productionPlanService = new ProductionPlanningService();
            _quoteService = new QuoteService();
        }

        public ActionResult Index()
        {
            ViewBag.ActiveClass = "ProductionPlanning";
            return View();
        }

        public ActionResult ProductionPlanningData(JqGridParam param)
        {
            var productionPlanningService = new ProductionPlanningService();

            var totalRows = productionPlanningService.GetProductionPlaningQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(CustomerRequirementView.Company))
                    {
                        totalRows = totalRows.Where(x => x.Company.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerSubmittedRequirement.SubmittedDate))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(q => q.SubmittedDate.Day == value.Day &&
                                                             q.SubmittedDate.Month == value.Month &&
                                                             q.SubmittedDate.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(CustomerSubmittedRequirement.Division))
                    {
                        totalRows = totalRows.Where(x => x.Division.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerSubmittedRequirement.SubmittedBy))
                    {
                        totalRows = totalRows.Where(x => x.SubmittedBy.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerSubmittedRequirement.PartKitNo))
                    {
                        totalRows = totalRows.Where(x => x.PartKitNo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerSubmittedRequirement.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerSubmittedRequirement.Respresentative))
                    {
                        totalRows = totalRows.Where(x => x.Respresentative.ToLower().Contains(rule.data.ToLower()));
                    }
                    ////else if (rule.field == nameof(CustomerSubmittedRequirement.Status))
                    ////{
                    ////    totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    ////}
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
            var currentUser = GetCurrentUser();
            var requirment =_quoteService.GetById(id);

            var viewModel = new RequirementStepsViewModel();
            viewModel.Read(_productionPlanService, requirment);

            var procedureSteps = _proceduresService.GetStepsData(viewModel.ProductProcedureId, currentUser.Id);
            viewModel.Setup(_productionPlanService, procedureSteps);

            if (!string.IsNullOrWhiteSpace(viewModel.ProductProcedureId) && !viewModel.Steps.Any())
            {
                foreach (var step in procedureSteps)
                {
                    viewModel.Steps.Add(new RequirementStepsDetailsViewModel
                    {
                        ObjectId = step.Id,
                        Process = step.StepTitle,
                        Step = (int)step.Print_Order
                    });
                }
            }

            viewModel.ProductName = requirment.ProductName;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(string id, RequirementStepsViewModel vm, string saveSubmit, string saveDraft)
        {
            var currentUser = GetCurrentUser();
            ModelState.Clear();

            if (vm.ProductProcedureId != vm.OldProductProcedureId)
            {
                vm.OldProductProcedureId = vm.ProductProcedureId;
                var procedureSteps = _proceduresService.GetStepsData(vm.ProductProcedureId, currentUser.Id);

                vm.Steps = new List<RequirementStepsDetailsViewModel>();

                foreach (var step in procedureSteps)
                {
                    vm.Steps.Add(new RequirementStepsDetailsViewModel
                    {
                        ObjectId = step.Id,
                        Process = step.StepTitle,
                        Step = (int)step.Print_Order
                    });
                }
                vm.Setup(_productionPlanService, procedureSteps);

                return View(vm);
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(saveDraft))
                {
                    var response = _productionPlanService.Save(vm);

                    if (!response.HasErrors())
                    {
                        TempData["SuccessMessage"] = response.SuccessMessage;

                        if (!string.IsNullOrWhiteSpace(saveSubmit))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {

                            vm.Setup(_productionPlanService, _proceduresService.GetStepsData(vm.ProductProcedureId, currentUser.Id));

                            return View(vm);

                        }
                    }

                    TempData["ErrorMessage"] = response.ErrorMessage;
                }
            }

            vm.Setup(_productionPlanService, _proceduresService.GetStepsData(vm.ProductProcedureId, currentUser.Id));

            return View(vm);
        }

        public ActionResult Status(string id, string currentStatus)
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

        [HttpPost]
        public ActionResult EditStatus(RequirementStatusViewModel model)
        {

            if (ModelState.IsValid)
            {
                var response = _productionPlanService.ChangeStatus(model.Id, model.Status);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;

            }

            model.Setup();

            return View(model);
        }
    }
}