using System;

namespace Msr.Models.Companies
{
    public class ApprovedCompaniesView
    {
        public string Id { get; set; }
        public string HistoryRefId { get; set; }
        public string Name { get; set; }
        public string CoType { get; set; }
        public string Parent { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string LocationName { get; set; }
        public DateTime? Drcm { get; set; }
        public string ModBy { get; set; }
        public string ObjectId { get; set; }
        public string LockedBy { get; set; }
        public string UnLockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string RevInfo { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string WfsId { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public string ApprovalActivity { get; set; }
        public string GeneralStatus { get; set; }
        public string ParentName { get; set; }
        public string RootCoName { get; set; }
        public string RootCoId { get; set; }
    }
}
