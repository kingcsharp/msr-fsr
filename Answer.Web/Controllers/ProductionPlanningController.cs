using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using Msr.Services.ProductionPlanning;
using Msr.Models.ProductionPlanning;
using Msr.Models.CustomerRequirements;
using Msr.Services.CustomerRequirements;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Services.Quotes.ViewModels;

namespace Answer.Web.Controllers
{
    public class ProductionPlanningController : BaseController
    {
        private readonly ProductionPlanningService productionPlanService;

        public ProductionPlanningController()
        {
            productionPlanService = new ProductionPlanningService();
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

            var defaultStatusList = base.GetDefaultStatus();

            totalRows = totalRows.Where(x => x.Id.Length > 0);

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
                            totalRows = totalRows.Where(q => q.SubmittedDate.HasValue &&
                                                             q.SubmittedDate.Value.Day == value.Day &&
                                                             q.SubmittedDate.Value.Month == value.Month &&
                                                             q.SubmittedDate.Value.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(CustomerRequirementView.DivisionFab))
                    {
                        totalRows = totalRows.Where(x => x.DivisionFab.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.SubmittedBy))
                    {
                        totalRows = totalRows.Where(x => x.SubmittedBy.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.PartKitNo))
                    {
                        totalRows = totalRows.Where(x => x.PartKitNo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.ShortDescription))
                    {
                        totalRows = totalRows.Where(x => x.ShortDescription.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.Respresentative))
                    {
                        totalRows = totalRows.Where(x => x.Respresentative.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(CustomerRequirementView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
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

        public ActionResult Edit(string id)
        {
            RequirementStepsViewModel model = new RequirementStepsViewModel();

            var productionPlanningService = new ProductionPlanningService();

            var getSteps = productionPlanningService.GetStepsByObjectId(id);

            model.Setup(getSteps, id);

            model.ObjectId = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id, RequirementStepsViewModel model)
        {
            var productionPlanningService = new ProductionPlanningService();

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                var response = productionPlanningService.Edit(id: id, model: model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    if (model.postType == "Save & Submit")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        model = new RequirementStepsViewModel();

                        var getRequirementSteps = productionPlanningService.GetStepsByObjectId(id);

                        model.Setup(getRequirementSteps, id);

                        model.ObjectId = id;

                        return View(model);

                    }

                }

                TempData["ErrorMessage"] = response.ErrorMessage;

            }

            var getSteps = productionPlanningService.GetStepsByObjectId(id);

            model.Setup(getSteps, id);

            return View(model);
        }

        public ActionResult ViewRequirements(string id)
        {
            var customerRequirementService = new CustomerRequirementService();

            var model = customerRequirementService.GetById(id);

            var customerRequirementViewModel = new CustomerRequirementViewModel();

            customerRequirementViewModel = customerRequirementViewModel.MapToDto(model);

            customerRequirementViewModel.Setup(new CustomerRequirementService());

            return PartialView("_ViewRequirements", customerRequirementViewModel);
        }
        public ActionResult ViewQuote(string id)
        {
            var listitems = new List<QuoteItemsViewModel>();
            var item = new QuoteItemsViewModel
            {
                ItemNo = 1,
                Quantity = 1,
                Description = "Singulus Kit 101010011",
                Price = (decimal)137.00,
                Extension = "137.00"
            };

            listitems.Add(item);
            var freeFormQuoteViewModel = new FreeFormQuoteViewModel
            {
                Date = DateTime.Today.ToString(CultureInfo.InvariantCulture),
                Customer = "Intel",
                Address = "4500 S. Dobson Road",
                Contact = "Tracy Ridge",
                CityStateZip = "Chandler, AZ, 85248",
                FOB = DateTime.Today.ToString(CultureInfo.InvariantCulture),
                Title = "EMM Manager",
                Terms = "terms",
                Phone = "480-715-2008",
                Delivery = "Post",
                Email = "tracy.ridge@intel.com",
                QuoteItemsViewModels = listitems

            };

            return PartialView("_ViewQuote", freeFormQuoteViewModel);
        }

        public ActionResult Status(string id, string currentStatus)
        {

            var response = productionPlanService.ChangeStatus(id, currentStatus);

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
                var response = productionPlanService.ChangeStatus(model.Id, model.Status);

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