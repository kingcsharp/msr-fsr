using System.Collections.Generic;

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

    }
}
