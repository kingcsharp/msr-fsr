using System;

namespace Msr.Models.PartTypes
{
    public class PartTypesApproved
    {
        public string Id { get; set; }
        public string HistoryRefId { get; set; }
        public string Name { get; set; }
        public string Spare { get; set; }
        public string Consumable { get; set; }
        public string ObjectId { get; set; }
        public string Unit { get; set; }
        public string UnitShippingWeight { get; set; }
        public string LockedBy { get; set; }
        public string UnLockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Root { get; set; }
        public string RevInfo { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string WfsId { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public string ApprovalActivity { get; set; }
    }
}
