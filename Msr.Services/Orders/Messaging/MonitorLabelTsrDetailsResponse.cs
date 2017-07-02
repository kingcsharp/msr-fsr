using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class MonitorLabelTsrDetailsResponse
    {
        public MonitorLabelTsrDetailsResponse()
        {
            GetMonitorLabelTsrDetailsResult = new List<GetMonitorLabelTsrDetailsResult>();
        }

        public int FillId { get; set; }
        public List<GetMonitorLabelTsrDetailsResult> GetMonitorLabelTsrDetailsResult { get; set; }
    }
}
