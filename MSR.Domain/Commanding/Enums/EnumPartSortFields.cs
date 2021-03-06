using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumPartSortFields
    {
        [Description("Id")]
        Id,
        [Description("Name")]
        Name,
        [Description("SegregationType")]
        SegregationType,
        [Description("PartNumber")]
        PartNumber,
        [Description("OemPartNumber")]
        OemPartNumber,
        [Description("IsKit")]
        IsKit,
        [Description("IsActive")]
        IsActive,
        [Description("MaximumCycles")]
        MaximumCycles,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("CreatedByName")]
        CreatedByName,
        [Description("LastUpdatedOn")]
        LastUpdateOn,
        [Description("LastUpdatedByName")]
        LastUpdatedByName
    }
}
