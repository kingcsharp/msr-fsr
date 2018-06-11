using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.ProductionPlanning;
using Msr.Services.Quotes;
using Msr.Services.Quotes.ViewModels;

namespace Answer.Web.Controllers
{
    public class QuoteController : BaseController
    {
        private QuoteService _quoteService;
        private readonly ProductionPlanningService _productionPlanningService;

        public QuoteController()
        {
            _quoteService = new QuoteService();
            _productionPlanningService = new ProductionPlanningService();
        }

        public ActionResult Create()
        {
            var currentUser = GetCurrentUser();

            var model = new FreeFormQuoteViewModel();

            model.Setup(_productionPlanningService, currentUser);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FreeFormQuoteViewModel viewModel)
        {
            var currentUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                viewModel.CreatedBy = currentUser.Login;

                var response = _quoteService.Create(viewModel, currentUser);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    return RedirectToAction("Index", "ProductionPlanning");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            viewModel.Setup(_productionPlanningService, currentUser);

            return View(viewModel);
        }

        public ActionResult ViewQuote(int id)
        {
            var currentUser = GetCurrentUser();

            var requirment = _quoteService.GetById(id);

            var vm = new JavaScriptSerializer().Deserialize<FreeFormQuoteViewModel>(requirment.QuoteJson);

            vm.Setup(_productionPlanningService, currentUser);

            vm.CustomerName = vm.Customers.SingleOrDefault(x => x.Value == vm.CustomerId)?.Text;
            vm.SupplierName = vm.Suppliers.SingleOrDefault(x => x.Value == vm.Supplier)?.Text;

            foreach (var item in vm.QuoteItems)
            {
                item.Extension = (decimal) (item.Price * item.Quantity.Value);
            }

            return PartialView("_ViewQuote", vm);
        }

        public ActionResult ViewRequirements(int id)
        {
            var requirment = _quoteService.GetById(id);

            var vm = new JavaScriptSerializer().Deserialize<CustomerRequirementViewModel>(requirment.CustomerRequirementJson);

            vm.Setup();

            return PartialView("_ViewRequirements", vm);
        }

        public ActionResult ViewRequirementsQuote(string id)
        {
            var model = _productionPlanningService.GetProductionPlaningQueryable().SingleOrDefault(x => x.ObjectId == id);
            var csr = new JavaScriptSerializer().Deserialize<CustomerRequirementViewModel>(model.CustomerRequirementJson);

            var quote = new FreeFormQuoteViewModel();

            quote.Date = csr.SubmittedDate;
            quote.Email = csr.CommercialEmail;
            quote.PhoneCSR = csr.CommercialPhone;
            quote.PartKitNo = csr.PartKitNo;
            quote.Title = csr.CommercialTitle;
            quote.SupplierName = model.SupplierName;
            quote.CustomerName = model.CustomerName;
            quote.FOB = csr.DivisionFab;
            quote.ProcessDescription = model.ProcedureName;
            quote.ExistingProcess = model.ProcedureName;

            foreach (var partInfoViewModel in csr.Parts)
            {
                quote.QuoteItems.Add(new QuoteItemsViewModel
                {
                    Quantity = 1, ////There is no way to enter Qty when submitting CSR
                    Description = partInfoViewModel.PartDescription,
                    Price = model.TotalSalePrice,
                    Extension = (decimal)(model.TotalSalePrice * 1)
                });
            }

            quote.CreatedBy = model.SubmittedBy;
            quote.Title = csr.CommercialTitle;
            quote.AddressSubmit = csr.StreetAddress;

            return PartialView("_ViewQuote", quote);
        }
    }
}