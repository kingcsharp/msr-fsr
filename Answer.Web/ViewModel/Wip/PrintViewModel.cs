using System.Collections.Generic;
using System.Web.Mvc;

namespace Answer.Web.ViewModel.Wip
{
    public class PrintOtherViewModel
    {
        public int FillId { get; set; }
        public string PrintOtherId { get; set; }
        public string PurchaseItemId { get; set; }

        public List<SelectListItem> PrintOtherList { get; set; }

        public void Setup(string purchaseItemId)
        {
            PurchaseItemId = purchaseItemId;
            PrintOtherList = new List<SelectListItem>
            {
                new SelectListItem {Text = "-----Select-----", Value = ""},
                new SelectListItem {Text = "Work Report", Value = "WORK_REPORT"},
                new SelectListItem {Text = "Delivery Ticket", Value = "DELIVERY"},
                new SelectListItem {Text = "WIP History Report", Value = "WIP_HIST"},
                new SelectListItem {Text = "NCR", Value = "HISTORY_LABEL"},
                new SelectListItem {Text = "Technical Data Label", Value = "MONITOR_LABEL"},
                new SelectListItem {Text = "Part Label Sheet", Value = "PART_LABEL"},
                new SelectListItem {Text = "Part Label Roll", Value="PART_LABEL_ROLL"}
            };
        }
    }
}