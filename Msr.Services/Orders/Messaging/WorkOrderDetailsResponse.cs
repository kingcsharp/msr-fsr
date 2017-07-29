using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class WorkOrderDetailsResponse
    {
        public WorkOrderDetailsResponse()
        {
            FileSearchResult = new FileSearchResult();
            Parts = new List<string>();
            TaskStepResults = new List<TaskStepResult>();
            WoItems = new List<WorkOrderView>();
        }

        public int FillId { get; set; }
        public FileSearchResult FileSearchResult { get; set; }
        public List<string> Parts { get; set; }
        public List<TaskStepResult> TaskStepResults { get; set; }
        public List<WorkOrderView> WoItems { get; set; }
        public IEnumerable<SelectListItem> WorkItems { get; set; }
    }
}
