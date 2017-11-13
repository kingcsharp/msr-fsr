using System.Web.Mvc;
using Answer.Web.ViewModel.Notes;
using Msr.Services.Notes;

namespace Answer.Web.Controllers
{
    [Authorize]
    public class NotesController : BaseController
    {
        public ActionResult AddNote(string id)
        {
            var vm = new AddNoteViewModel();
            vm.Setup();

            return PartialView("_AddNote", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddNote(AddNoteViewModel model)
        {
            var orderService = new NoteService();

            orderService.AddNoteToTask(model.MapToDto());

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}