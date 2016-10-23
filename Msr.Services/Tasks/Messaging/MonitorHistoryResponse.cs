
using System.Collections.Generic;

namespace Msr.Services.Tasks.Messaging
{
    public class MonitorHistoryResponse : BaseNotification
    {
        public MonitorHistoryResponse()
        {
            MonitorItem = new List<MonitorItem>();
        }
        public List<MonitorItem> MonitorItem { get; set; }
    }
}
