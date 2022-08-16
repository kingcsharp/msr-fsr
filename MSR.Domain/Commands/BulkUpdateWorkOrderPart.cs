using MSR.Domain.Commanding;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class BulkUpdateWorkOrderPart: Command
    {
        public List<int> WorkOrderPartIds { get; }
        public string PartData { get; }

        public BulkUpdateWorkOrderPart(List<int> workOrderPartIds, string partData)
        {
            WorkOrderPartIds = workOrderPartIds;
            PartData = partData;
        }
    }
}
