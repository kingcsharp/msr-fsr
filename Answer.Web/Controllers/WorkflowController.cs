using System.Web.Mvc;
using Msr.Services.Workflows;
using Msr.Services.Workflows.ViewModels;

namespace Answer.Web.Controllers
{
    public class WorkflowController : BaseController
    {
        private readonly WorkflowService _workflowService;

        public WorkflowController()
        {
            _workflowService = new WorkflowService();
        }

        public ActionResult Submit(string objId, string returnUrl)
        {
            var user = GetCurrentUser();

            var vm = new SubmitWorkflowViewModel();

            var objectData = _workflowService.GetSpObjectGetData(objId, user.Id);

            vm.Name = objectData.Obj_Desc;
            vm.ObjectId = objectData.Id;
            vm.ReturnUrl = returnUrl;

            var workflows = _workflowService.GetSpObjectShowApplicableWorkflows(objId, user.Id);

            vm.SetUp(workflows);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Submit(SubmitWorkflowViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.LoggedUserIdResult = GetCurrentUser();
                vm.CompletionStart = "APPROVED";

                var result = _workflowService.SubmitWorkflow(vm);

                if (!result.HasErrors())
                {
                    TempData["SuccessMessage"] = result.SuccessMessage;

                    return RedirectPermanent(vm.ReturnUrl);
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            return View(vm);
        }

        public ActionResult Delete(string objId, string returnUrl)
        {
            var user = GetCurrentUser();

            var vm = new SubmitWorkflowViewModel();

            var objectData = _workflowService.GetSpObjectGetData(objId, user.Id);
            vm.Name = objectData.Obj_Desc;
            vm.ObjectId = objectData.Id;
            vm.ReturnUrl = returnUrl;

            var workflows = _workflowService.GetSpObjectShowApplicableWorkflows(objId, user.Id);

            vm.SetUp(workflows);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Delete(SubmitWorkflowViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.LoggedUserIdResult = GetCurrentUser();
                vm.CompletionStart = "DELETED";

                var result = _workflowService.SubmitWorkflow(vm);

                if (!result.HasErrors())
                {
                    TempData["SuccessMessage"] = result.SuccessMessage;

                    return RedirectPermanent(vm.ReturnUrl);
                }

                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            return View(vm);
        }

        public ActionResult UnLockAndDelete(string objId, string returnUrl)
        {
            var user = GetCurrentUser();

            var result = _workflowService.UnLockObjAndDelete(objId, user.Id);

            if (!result.HasErrors())
            {
                TempData["SuccessMessage"] = "Object has been deleted successfully";
            }
            else
            {
                TempData["ErrorMessage"] = result.ErrorMessage();
            }

            return RedirectPermanent(returnUrl);
        }

    }
}