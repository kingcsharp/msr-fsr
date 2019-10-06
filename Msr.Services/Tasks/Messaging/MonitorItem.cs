
using System.Collections.Generic;
using Msr.Models.Tasks;

namespace Msr.Services.Tasks.Messaging
{
    public class MonitorItem
    {
        public MonitorItem()
        {
            MonitorResults = new List<MonitorResult>();
        }

        public string TaskId { get; set; }

        public string TaskTitle { get; set; }

        public string Description { get; set; }

        public List<MonitorResult> MonitorResults { get; set; }

    }
}
