using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumWorkflowPendingSortFields
    {
        [Description("Id")]
        Id,
        [Description("ActivityType")]
        ActivityType,
        [Description("Name")]
        Name,
        [Description("RequestedChanges")]
        RequestedChanges,
        [Description("WorkflowName")]
        WorkflowName,
        [Description("WorkflowGroupName")]
        WorkflowGroupName,
        [Description("WorkflowCreatedByName")]
        WorkflowCreatedByName,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("CreatedByName")]
        CreatedByName
    }
}
