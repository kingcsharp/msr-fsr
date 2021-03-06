using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetTrainingCertification: PagingCommand
    {
        public int? UserId { get; set; }
        public string EmployeeName { get; set; }
        public string CertificationName { get; set; }
        public DateTime? CertificationFromDate { get; set; }
        public DateTime? CertificationToDate { get; set; }
        public bool? Status { get; set; }
    }
}
