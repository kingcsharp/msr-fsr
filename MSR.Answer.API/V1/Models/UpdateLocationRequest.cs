using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateLocationRequest
    {
        [Required]
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int? ParentId { get; set; }
        public string InternalAddress { get; set; }
        public string InvoiceClass { get; set; }
        public int? TimeZoneId { get; set; }
    }
}
