using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Services.Users.Messages;

namespace Answer.Web.ViewModel.Wip
{
    public class WipStatusViewModel
    {
        public IList<WipStatusViewItem> WipStatusViewItems { get; set; }

        public List<WorkOrderView> WoItemsByProcedures { get; set; }

        public LoggedUserIdResult CurrentUser { get; set; }

        public string Location { get; set; }

        public List<SelectListItem> LocationList { get; set; }

        public void Setup()
        {
            LocationList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Arizona Service Center",
                    Value = "Arizona Service Center"
                },
                new SelectListItem
                {
                    Text = @"Oregon Service Center",
                    Value = "Oregon Service Center"
                },
                new SelectListItem
                {
                    Text = @"Ireland Service Center",
                    Value = "Ireland Service Center"
                },
                new SelectListItem
                {
                    Text = @"Israel Service Center",
                    Value = "Israel Service Center"
                },
            };

            LocationList.Insert(0, new SelectListItem { Text = "--Please Select--", Value = "" });
        }

    }
}