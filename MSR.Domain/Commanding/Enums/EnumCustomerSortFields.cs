using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumCustomerSortFields
    {
        [Description("Id")]
        Id,
        [Description("Address")]
        Address,
        [Description("Phone")]
        Phone,
        [Description("IsActive")]
        IsActive,
        [Description("PrimaryContactUserName")]
        PrimaryContactUserName,
        [Description("SecondartContactUserName")]
        SecondartContactUserName,
        [Description("LocationName")]
        LocationName,
        [Description("CustomerNumber")]
        CustomerNumber,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("CreatedByFullName")]
        CreatedByFullName
    }
}
