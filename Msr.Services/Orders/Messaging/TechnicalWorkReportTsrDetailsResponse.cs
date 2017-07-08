using System.Collections.Generic;
using System.Net.Mail;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class TechnicalWorkReportTsrDetailsResponse
    {
        public TechnicalWorkReportTsrDetailsResponse()
        {
            TasksFindForFillIdResult = new List<TasksFindForFillIdResult>();
            WipHistoryDetailResult = new WipHistoryDetailResult();
        }

        public int FillId { get; set; }
        public List<TasksFindForFillIdResult> TasksFindForFillIdResult { get; set; }
        public WipHistoryDetailResult WipHistoryDetailResult { get; set; }
    }
}
