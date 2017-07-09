using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class WipHistoryTsrResponse
    {
        public WipHistoryTsrResponse()
        {
            WipTaskResult = new List<WipTaskResult>();
            WipHistoryDetailResult = new WipHistoryDetailResult();
        }

        public int FillId { get; set; }
        public List<WipTaskResult> WipTaskResult { get; set; }
        public WipHistoryDetailResult WipHistoryDetailResult { get; set; }
    }
}
