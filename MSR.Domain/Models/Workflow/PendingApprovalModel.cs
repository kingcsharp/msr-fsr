using System;
namespace MSR.Domain.Models
{
    public class PendingApprovalModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string WorkflowName { get; set; }
        public string ActivityType { get; set; }
        public string WorkflowId { get; set; }
        public string WorkflowCreatedByName { get; set; }
        public string WorkflowGroupName { get; set; }
        public string WorkflowGroupId { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}