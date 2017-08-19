using System;

namespace Msr.Models.Parts
{
    public class PartsView
    {
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
        public string ObjectId { get; set; }
        public string Id { get; set; }
        //public string PartId { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string PartType { get; set; }
        public string TrackFromStart { get; set; }
        public string Company { get; set; }
        public double? UnitShippingWeight { get; set; }
        public string CompanyPartNumber { get; set; }
        public Int16? SupplierSeeInstallBase { get; set; }
        public Int16? SupplierSeeAvailability { get; set; }
        public Int16? CustomerSeeAvailability { get; set; }
        public string CompanyName { get; set; }
        public string PartTypeName { get; set; }
        public string Spare { get; set; }
        public string Consumable { get; set; }
        public string WeightType { get; set; }
        public string ParentCoName { get; set; }
        public string WeightTypeName { get; set; }
        public byte? CreateProd { get; set; }
        public string SupplierCo { get; set; }
        public string ProductType { get; set; }
        public string ProcVerb { get; set; }
    }
}
