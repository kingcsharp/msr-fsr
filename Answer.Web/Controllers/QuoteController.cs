using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Services.CustomerRequirements;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.Quotes;
using Msr.Services.Quotes.ViewModels;

namespace Answer.Web.Controllers
{
    public class QuoteController : BaseController
    {
        private QuoteService _quoteService;

        public QuoteController()
        {
            _quoteService = new QuoteService();
        }

        public ActionResult Create()
        {
            var model = new FreeFormQuoteViewModel();

            model.Setup();

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FreeFormQuoteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               var currentUser = GetCurrentUser().Id;

                var response = _quoteService.Create(viewModel);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    return RedirectToAction("Create", "Quote");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return View(viewModel);
        }

        public ActionResult ViewQuote(int id)
        {
            var requirment = _quoteService.GetById(id);

            var vm = new JavaScriptSerializer().Deserialize<FreeFormQuoteViewModel>(requirment.QuoteJson);

            return PartialView("_ViewQuote", vm);
        }

        public ActionResult ViewRequirements(int id)
        {
            var requirment = _quoteService.GetById(id);

            var vm = new JavaScriptSerializer().Deserialize<CustomerRequirementViewModel>(requirment.CustomerRequirementJson);

            vm.Setup();

            return PartialView("_ViewRequirements", vm);
        }
    }
}