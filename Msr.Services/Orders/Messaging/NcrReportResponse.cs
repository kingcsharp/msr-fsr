using System.Collections.Generic;
using Msr.Models.Tasks;
using Msr.Services.Tasks.Messaging;

namespace Msr.Services.Orders.Messaging
{
    public class NcrReportResponse : BaseNotification
    {
        public NcrReportResponse()
        {
            Details = new NcrDetails();
        }

        public NcrDetails Details { get; set; }
        public List<string> Comments { get; set; }
        public List<DocumentView> StepPics { get; set; }
        public List<DocumentView> Photos { get; set; }
        public List<MonitorItem> MonitorItem { get; set; }
    }
}
