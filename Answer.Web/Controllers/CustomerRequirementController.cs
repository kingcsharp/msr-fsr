using System.Web.Mvc;
using Msr.Services.CustomerRequirements;
using Msr.Services.CustomerRequirements.ViewModel;

namespace Answer.Web.Controllers
{
    public class CustomerRequirementController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new CustomerRequirementViewModel();
            model.Setup();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomerRequirementViewModel model)
        {
            var customerRequirementService = new CustomerRequirementService();

            if (ModelState.IsValid)
            {
                model.SubmittedBy = GetCurrentUser().Id;

                var response = customerRequirementService.Create(model);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    return RedirectToAction("Create", "CustomerRequirement");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            model.Setup();

            return View(model);
        }
    }
}