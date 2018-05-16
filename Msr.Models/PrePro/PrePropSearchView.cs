using System;

namespace Msr.Models.PrePro
{
    public class PrePropSearchView
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string LockedBy { get; set; }
        public string UnlockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Root { get; set; }
        public string RevInfo { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string WfsId { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public string ApprovalActivity { get; set; }

        public string ObjId { get; set; }
        public string ProcStepId { get; set; }
        public string StepText { get; set; }
        public string Comments { get; set; }

        public double? Duration { get; set; }
        public string DurationType { get; set; }
        public string ReferenceObject { get; set; }
        public string ReferenceVerb { get; set; }
        public string SystemTask { get; set; }
        public int? StartOnCounter { get; set; }
        public string ReferenceFiles { get; set; }
        public string Roles { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Utilization { get; set; }
        public int? UsefulLife { get; set; }
    }
}
