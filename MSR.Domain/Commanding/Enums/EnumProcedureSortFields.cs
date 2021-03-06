using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumProcedureSortFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("ProcedureTypeName")]
        ProcedureTypeName,
        [Description("Duration")]
        Duration,
        [Description("DurationType")]
        DurationType,
        [Description("Revision")]
        Revision,
        [Description("CreatedFullName")]
        CreatedFullName,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("LastUpdatedFullName")]
        LastUpdatedFullName,
        [Description("LastUpdatedOn")]
        LastUpdatedOn
    }
}
