using System.ComponentModel;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumMonitorUnitofMeasure
    {
        [Description("Equipment")]
        Equipment = 1,

        [Description("Number")]
        Number = 2,
        [Description("Yes or No")]
        YesorNo = 3,
        [Description("Text")]
        Text = 4,
        [Description("Pass or Fail")]
        PassorFail = 5,
        [Description("Select")]
        Select = 6
    }
}
