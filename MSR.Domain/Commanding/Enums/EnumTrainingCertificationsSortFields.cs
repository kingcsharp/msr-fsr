using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumTrainingCertificationsSortFields
    {
        [Description("UserId")]
        UserId,
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
