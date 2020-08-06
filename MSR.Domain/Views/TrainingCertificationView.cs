using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class TrainingCertificationView
    {
        public string EmployeeName { get; set; }
        public DateTime? CertificationFromDate { get; set; }
        public DateTime? CertificationToDate { get; set; }
        public string Status { get; set; }
    }
}
