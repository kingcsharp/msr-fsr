using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class BulkUpdateWorkOrderPartRequest
    {
        public List<int> WorkOrderPartIds { get; set; }
        public string PartData { get; set; }
    }
}
