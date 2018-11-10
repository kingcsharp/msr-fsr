using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Workflows
{
    public class PendingApprovalView
    {
        [Key]
        public string ObjectId { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string WfName { get; set; }
        public string StageName { get; set; }
        public string GroupName { get; set; }
        public string Initiator { get; set; }
        public string ApprovalName { get; set; }
        public DateTime? DateStarted { get; set; }
        public string Status { get; set; }
        public int? Revision { get; set; }
        public string PersonId { get; set; }
        public string WfsId { get; set; }
        public string WfStageId { get; set; }
        public string WfGroupId { get; set; }
        public string Approver { get; set; }
        public string RevInfo { get; set; }
        public string NotificationType { get; set; }
    }
}
