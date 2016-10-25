
using System.Collections.Generic;

namespace Msr.Services.Tasks.Messaging
{
    public class MonitorHistoryResponse : BaseNotification
    {
        public string SupName { get; set; }

        public string FillObjDesc { get; set; }

        public string PurchItemId { get; set; }

        public MonitorHistoryResponse()
        {
            MonitorItem = new List<MonitorItem>();
        }
        public List<MonitorItem> MonitorItem { get; set; }
    }
}
