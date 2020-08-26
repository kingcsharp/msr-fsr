using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class LocationModel: DeletableModel
    {
        public int OldId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int? ParentId { get; set; }
        public LocationModel Parent { get; set; }
        public string InternalAddress { get; set; }
        public string InvoiceClass { get; set; }
        public TimeZoneModel TimeZone { get; set; }
        public string Status { get; set; }
        public int? Site { get; set; }
    }
}
