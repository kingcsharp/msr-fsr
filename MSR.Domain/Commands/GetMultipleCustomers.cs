using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Domain.Commands
{
    public class GetMultipleCustomers: PagingCommand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public int? SecondaryContactUserId { get; set; }
        public int? LocationId { get; set; }
        public bool? IsActive { get; set; }
        public string PrimaryContactUserFullName { get; set; }
        public string SecondaryContactUserFullName { get; set; }
        public string LocationName { get; set; }
        public string CustomerNumber { get; set; }
        public string CreatedFullName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
