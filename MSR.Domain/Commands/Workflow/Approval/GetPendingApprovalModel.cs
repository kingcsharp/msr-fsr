using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Domain.Commands
{
    public class GetPendingApprovalModel: PagingCommand
    {
        public EnumApprovalTables Table { get; set; }
        public int? Id { get; set; }
        public string ActivityType { get; set; }
        public string Name { get; set; }
        public string RequestedChanges { get; set; }
        public string WorkflowName { get; set; }
        public string WorkflowGroupName { get; set; }
        public string WorkflowCreatedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
    }
}