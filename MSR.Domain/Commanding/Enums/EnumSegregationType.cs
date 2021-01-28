using System.ComponentModel;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumSegregationType
    {
        [Description("CU")]
        CU = 1,        
        [Description("Non-CU")]
        NONCU = 2,
        [Description("Deseg")]
        DESEG = 3,
    }
}
