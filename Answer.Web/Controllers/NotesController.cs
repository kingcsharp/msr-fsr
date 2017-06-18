using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Answer.Web.ViewModel.Notes;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Notes;
using Msr.Services.Notes.Messages;
using Msr.Services.Orders;
using Msr.Services.Users;
using Msr.Web.Controllers;

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