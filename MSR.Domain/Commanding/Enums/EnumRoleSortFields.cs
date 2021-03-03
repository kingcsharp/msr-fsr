using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumRoleSortFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("IsCertificationRole")]
        IsCertificationRole,
        [Description("ParentRoles")]
        ParentRoles,
        [Description("AssignedUsers")]
        AssignedUsers,
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
