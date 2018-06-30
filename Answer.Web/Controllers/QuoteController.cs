using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.ProductionPlanning;
using Msr.Services.Quotes;
using Msr.Services.Quotes.ViewModels;
using Msr.Services.Users;

namespace Answer.Web.Controllers
{
    public class QuoteController : BaseController
    {
        private QuoteService _quoteService;
        private readonly ProductionPlanningService _productionPlanningService;
        private readonly UserService _userService;

        public QuoteController()
        {
            _quoteService = new QuoteService();
            _productionPlanningService = new ProductionPlanningService();
            _userService = new UserService();
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

        public ActionResult ViewQuote(int id, string objectId)
        {
            var currentUser = GetCurrentUser();

            var requirment = _quoteService.GetById(id);

            var vm = new JavaScriptSerializer().Deserialize<FreeFormQuoteViewModel>(requirment.QuoteJson);

            vm.Setup(_productionPlanningService, currentUser);

            vm.CustomerName = vm.Customers.SingleOrDefault(x => x.Value == vm.CustomerId)?.Text;
            vm.SupplierName = vm.Suppliers.SingleOrDefault(x => x.Value == vm.Supplier)?.Text;
            vm.ProductId = requirment.ProductId;


            var model = _productionPlanningService.GetProductionPlaningQueryable().SingleOrDefault(x => x.ObjectId == objectId);
            vm.CycleTime = model.CycleTime;

            foreach (var item in vm.QuoteItems)
            {
                item.Extension = (decimal)(item.Price * item.Quantity.Value);
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
            var currentUser = GetCurrentUser();

            var currentUserDetail = _userService.GetAnserByUserName(currentUser.Login);

            var model = _productionPlanningService.GetProductionPlaningQueryable().SingleOrDefault(x => x.ObjectId == id);

            var csr = new JavaScriptSerializer().Deserialize<CustomerRequirementViewModel>(model.CustomerRequirementJson);

            var quote = new FreeFormQuoteViewModel();
            quote.ProductId = model.ObjectId;
            quote.CustomerName = model.Company;
            quote.Date = model.SubmittedDate;
            quote.Email = csr.CommercialEmail;
            quote.PhoneCSR = csr.CommercialPhone;
            quote.PartKitNo = csr.PartKitNo;
            quote.Title = csr.CommercialTitle;
            quote.SupplierName = model.SupplierName;
            quote.Contact = csr.CommercialName;
            quote.ProcessDescription = model.ProcedureName;
            quote.ExistingProcess = model.ProcedureName;
            quote.Delivery = csr.ShippingMethod;
            quote.CycleTime = model.CycleTime;

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

            quote.CreatedBy = currentUserDetail.FullName;
            quote.Title = currentUserDetail.Title;

            return PartialView("_ViewQuote", quote);
        }
    }
}