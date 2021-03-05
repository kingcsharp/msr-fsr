using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumApprovalWorkflowsSortingFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("MemberStages")]
        MemberStages,
        [Description("ActivityMaps")]
        ActivityMaps,
        [Description("IsActive")]
        IsActive,
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
