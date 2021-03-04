using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumHelpSortingFields
    {
        [Description("Id")]
        Id,
        [Description("Title")]
        Title,
        [Description("FriendlyURL")]
        FriendlyURL,
        [Description("Roles")]
        Roles
    }
}
