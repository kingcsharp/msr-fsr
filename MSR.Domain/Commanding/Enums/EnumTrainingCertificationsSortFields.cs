using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumTrainingCertificationsSortFields
    {
        [Description("Id")]
        Id,
        [Description("EmployeeName")]
        EmployeeName,
        [Description("CertificationName")]
        CertificationName,
        [Description("CertificationFromDate")]
        CertificationFromDate,
        [Description("CertificationToDate")]
        CertificationToDate,
        [Description("Status")]
        Status
    }
}
