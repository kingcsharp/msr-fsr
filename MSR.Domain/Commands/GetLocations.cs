using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetLocations : PagingCommand
    {
        public int? ParentId { get; set; }
        public int? Id { get; set; }
        public string? InternalAddress { get; set; }
        public string Name { get; set; }
        public string CreatedFullName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string ParentName { get; set; }
        public string TimezoneDescription { get; set; }
        public string Address2 { get; set; }
        public string Status { get; set; }
    }
}
