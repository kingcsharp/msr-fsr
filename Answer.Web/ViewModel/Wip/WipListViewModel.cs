using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msr.Models.Orders;
using Msr.Services.Users.Messages;

namespace Answer.Web.ViewModel.Wip
{
    public class WipListViewModel
    {
        public List<WorkOrderView> WoItemsInprogress { get; set; }

        public List<WorkOrderView> WoItemsByProcedures { get; set; }
        public LoggedUserIdResult CurrentUser { get; set; }
    }
}