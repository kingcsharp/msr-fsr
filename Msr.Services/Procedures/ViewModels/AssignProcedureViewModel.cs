using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Users;

namespace Msr.Services.Procedures.ViewModels
{
    public class AssignProcedureViewModel
    {
        public AssignProcedureViewModel()
        {
            AssignToPeople = new List<string>();
            Peoples = new List<SelectListItem>();
        }

        public string Id { get; set; }

        public string ProcedureName { get; set; }

        [DisplayName("Assign to people")]

        public List<string> AssignToPeople { get; set; }

        public List<SelectListItem> Peoples { get; set; }

        [DisplayName("Date and Time to start")]
        public DateTime? DatetimeToStart { get; set; }

        public string LoginId { get; set; }

        public void Setup(UserService userService)
        {
            Peoples = userService.GetSearchUser().Select(x => new SelectListItem
            {
                Text = x.Full_Name,
                Value = x.Obj_Id
            }).OrderBy(o => o.Text).ToList();
        }
    }
}