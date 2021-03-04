using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumDocumentSortFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("Revision")]
        Revision,
        [Description("LastUpdatedOn")]
        LastUpdatedOn,
        [Description("LastUpdatedFullName")]
        LastUpdatedFullName
    }
}
