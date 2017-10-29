using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Services.Quotes;
using Msr.Services.Quotes.ViewModels;

namespace Answer.Web.Controllers
{
    public class QuoteController : Controller
    {
        // GET: Quote
        public ActionResult Index()
        {
            return View();
        }

        // GET: Quote
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(FreeFormQuoteViewModel model, List<QuoteItemsViewModel> quoteItemsViewModels)
        {
            var quoteService = new QuoteService();
            var response = quoteService.Create(model, quoteItemsViewModels);

            if (!response.HasErrors())
            {
                TempData["SuccessMessage"] = response.SuccessMessage;

                return RedirectToAction("Index", "Quote");
            }

            TempData["ErrorMessage"] = response.ErrorMessage;
            return View();
        }
    }
}