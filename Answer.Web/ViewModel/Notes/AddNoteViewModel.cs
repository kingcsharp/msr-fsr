using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Services.Notes.Messages;

namespace Answer.Web.ViewModel.Notes
{
    public class AddNoteViewModel
    {
        public string Comment { get; set; }

        public string Confidentiality { get; set; }

        public List<SelectListItem> ConfidentialityList { get; set; }

        public void Setup()
        {
            ConfidentialityList = new List<SelectListItem>
            {
                new SelectListItem {Text = "ALL", Value = "ALL"},
                new SelectListItem {Text = "Open", Value = "Open"},
                new SelectListItem {Text = "Personal", Value = "Personal"},
                new SelectListItem {Text = "Corporate", Value = "Corporate"}
            };
        }

        public AddNoteToTaskRequest MapToDto()
        {
            return new AddNoteToTaskRequest
            {
                Comment = Comment
            };
        }
    }
}