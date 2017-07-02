using System.Collections.Generic;
using System.Net.Mail;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class NcrTsrDetailsResponse
    {
        public NcrTsrDetailsResponse()
        {
            TasksFindForFillIdResult = new List<TasksFindForFillIdResult>();
            FillsSearchResult = new FillsSearchResult();
            AttachmentFileIds = new List<string>();
        }

        public int FillId { get; set; }
        public List<TasksFindForFillIdResult> TasksFindForFillIdResult { get; set; }
        public FillsSearchResult FillsSearchResult { get; set; }
        public List<string> AttachmentFileIds { get; set; }
    }
}
