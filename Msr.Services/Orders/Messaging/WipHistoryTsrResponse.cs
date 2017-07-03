using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class WipHistoryTsrResponse
    {
        public WipHistoryTsrResponse()
        {
            WipTaskResult = new List<WipTaskResult>();
            WipSubTaskResult = new List<WipSubTaskResult>();
            WipHistoryDetailResult = new WipHistoryDetailResult();
        }

        public int FillId { get; set; }
        public List<WipTaskResult> WipTaskResult { get; set; }
        public List<WipSubTaskResult> WipSubTaskResult { get; set; }
        public WipHistoryDetailResult WipHistoryDetailResult { get; set; }
    }
}
