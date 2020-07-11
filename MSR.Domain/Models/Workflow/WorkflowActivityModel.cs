using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class WorkflowActivityModel : DeletableModel
    {
        public string Name { get; set; }
        public string ApprovalTableName { get; set; }
        public bool? CreateRevision { get; set; }
    }
}
