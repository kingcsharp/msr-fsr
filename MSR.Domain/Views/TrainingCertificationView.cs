using System;

namespace MSR.Domain.Views
{
    public class TrainingCertificationView
    {
        public int Id { get;set;}
        public int UserId { get;set;}
        public string EmployeeName { get; set; }
        public DateTime? CertificationFromDate { get; set; }
        public DateTime? CertificationToDate { get; set; }
        public string Status { get; set; }
        public string CertificationName { get; set; }
    }
}
