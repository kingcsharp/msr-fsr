
using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetMultipleCustomersRequest: BaseApiModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public int? SecondaryContactUserId { get; set; }
        public int? LocationId { get; set; }
        public bool? IsActive { get; set; }
        public string PrimaryContactUserName { get;set;}
        public string SecondartContactUserName { get;set;}
        public string LocationName { get;set;}
        public string CustomerNumber { get;set;}
        public string CreatedByFullName { get;set;}
        public DateTime? CreatedOn { get;set;}
    }
}
