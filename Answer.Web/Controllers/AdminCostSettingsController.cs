using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.AdminCostSettings;
using Msr.Models.Menus;
using Msr.Services.AdminCostSettings;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Administration)]
    public class AdminCostSettingsController : BaseController
    {
        private readonly AdminCostSettingService _adminCostSettingService;

        public AdminCostSettingsController()
        {
            _adminCostSettingService = new AdminCostSettingService();
        }

        public ActionResult Index()
        {
            var model = _adminCostSettingService.GetAdminCostSettings();

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(AdminCostSetting model)
        {
            if (ModelState.IsValid)
            {
                var response = _adminCostSettingService.Update(model);

                if (response.Entity)
                {
                    AddSuccessNotification("Admmin Cost Settings updated successfully.");
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}