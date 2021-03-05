using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumEquipmentMaintenanceSoftFields
    {
        [Description("Id")]
        Id,
        [Description("LocationName")]
        LocationName,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("CreatedFullName")]
        CreatedFullName,
        [Description("AssignedToFullName")]
        AssignedToFullName,
        [Description("TroubleState")]
        TroubleState,
        [Description("MaintenanceTask")]
        MaintenanceTask,
        [Description("Comments")]
        Comments,
        [Description("PemLastCompletedDate")]
        PemLastCompletedDate,
        [Description("FrequencyField")]
        FrequencyField,
        [Description("StatusName")]
        StatusName
    }
}
