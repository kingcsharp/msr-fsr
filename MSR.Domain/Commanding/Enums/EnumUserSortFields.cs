using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumUserSortFields
    {
        [Description("Id")]
        Id,
        [Description("IsActive")]
        IsActive,
        [Description("IsAnswerUser")]
        isAnswerUser,
        [Description("FirstName")]
        FirstName,
        [Description("LastName")]
        LastName,
        [Description("UserName")]
        UserName,
        [Description("Email")]
        Email,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("Roles")]
        Roles,
        [Description("LocationName")]
        LocationName,
        [Description("SupervisorName")]
        SupervisorName
    }
}
