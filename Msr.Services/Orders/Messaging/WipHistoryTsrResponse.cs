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
            ListDocument = new List<TaskDocument>();
        }

        public int FillId { get; set; }
        public List<TaskDocument> ListDocument { get; set; }
        public List<WipTaskResult> WipTaskResult { get; set; }
        public WipHistoryDetailResult WipHistoryDetailResult { get; set; }

        public List<TaskDocument> GetDocumentsOfTask(string taskId)
        {
            return ListDocument.FindAll(x => x.TaskId == taskId);
        }
    }
}
