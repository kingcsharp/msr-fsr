using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class WorkflowLinkModel
    {
        public int WorkflowActivity { get; set; }
        public int WorkflowId { get; set; }
        public int WorkflowStageId { get; set; }
        public int WorkflowGroupId { get; set; }
        public int MenuItemId { get; set; }
        public string ApprovalTableName { get; set; }
        public ICollection<int> RoleIds { get; set; }
    }
}
