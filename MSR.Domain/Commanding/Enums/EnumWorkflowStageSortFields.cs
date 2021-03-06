using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumWorkflowStageSortFields
    {
        [Description("Id")]
        Id,
        [Description("IsActive")]
        IsActive,
        [Description("Name")]
        Name,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("CreatedByName")]
        CreatedByName,
        [Description("LastUpdatedOn")]
        LastUpdatedOn,
        [Description("LastUpdatedByName")]
        LastUpdatedByName
    }
}
