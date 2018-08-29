using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;
using Msr.Services.CustomerRequirements;
using Msr.Services.CustomerRequirements.ViewModel;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Quotes)]
    public class CustomerRequirementController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var loggedUser = GetCurrentUser();

            var model = new CustomerRequirementViewModel();
            model.SubmittedBy = loggedUser.Name + " " + loggedUser.Last_Name;
            model.Setup();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomerRequirementViewModel model)
        {
            var customerRequirementService = new CustomerRequirementService();
            var loggedUser = GetCurrentUser();

            if (ModelState.IsValid)
            {
                var response = customerRequirementService.Create(model, loggedUser.Id);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    return RedirectToAction("Index", "ProductionPlanning");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            model.Setup();

            return View(model);
        }
    }
}