using System.Collections.Generic;
using Msr.Models.Orders;
using Msr.Services.Users.Messages;

namespace Answer.Web.ViewModel.Wip
{
    public class WipStatusViewModel
    {
        public IList<WipStatusViewItem> WipStatusViewItems { get; set; }

        public List<WorkOrderView> WoItemsByProcedures { get; set; }

        public LoggedUserIdResult CurrentUser { get; set; }
    }
}