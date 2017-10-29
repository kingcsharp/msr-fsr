using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            model.Setup(new CustomerRequirementService());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, List<ProcessInfoViewModel> process, List<PartInfoViewModel> part)
        {
            var model = new CustomerRequirementViewModel();

            var customerRequirementService = new CustomerRequirementService();

            model.SubmittedBy = GetCurrentUser().Id;
            model.SubmittedDate = DateTime.Now;

            if (form["postType"] == "Save & Submit")
            {
                model.Status = "APPROVED";
            }
            else
            {
                model.Status = "RECEIVED";
            }

            if (TryUpdateModel(model, form))
            {
                var response = customerRequirementService.Create(model, process, part);

                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = response.SuccessMessage;

                    return RedirectToAction("Index", "ProductionPlanning");
                }

                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            model.Setup(new CustomerRequirementService());

            return View(model);
        }
    }
}