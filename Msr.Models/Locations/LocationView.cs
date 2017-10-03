using System;

namespace Msr.Models.Locations
{
    public class LocationView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentLocation { get; set; }
        public string ParentLocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string InternalAddress { get; set; }
        public string ObjectId { get; set; }
        public DateTime? Drcm { get; set; }
        public string ParentPath { get; set; }
        public string CompleteName { get; set; }
        public string ObjId { get; set; }
        public string LockedBy { get; set; }
        public string UnlockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Root { get; set; }
        public string RevInfo { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public Int16? IsChildLocation { get; set; }
        public int? Revision { get; set; }
        public string WfsId { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
    }
}
