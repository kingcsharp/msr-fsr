using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumLocationSortFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("InternalAddress")]
        InternalAddress,
        [Description("CreatedFullName")]
        CreatedFullName,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("Address1")]
        Address1,
        [Description("City")]
        City,
        [Description("State")]
        State,
        [Description("Postalcode")]
        Postalcode,
        [Description("Country")]
        Country,
        [Description("Phone")]
        Phone,
        [Description("ParentName")]
        ParentName,
        [Description("TimezoneDescription")]
        TimezoneDescription,
        [Description("Address2")]
        Address2,
        [Description("Status")]
        Status
    }
}
