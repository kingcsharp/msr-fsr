using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetTrainingCertificationRequest:BaseApiModel
    {
        public int? UserId { get; set; }
        public string EmployeeName { get;set;}
        public string CertificationName { get;set;}
        public DateTime? CertificationFromDate { get;set;}
        public DateTime? CertificationToDate { get;set;}
        public string Status { get;set;}
    }
}
