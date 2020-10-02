



using System.ComponentModel;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumStatusSteps
    {
        [Description("Approved")]
        Approved = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Complete")]
        Complete = 3,
        [Description("Cancelled")]
        Cancelled = 4,
        [Description("Pending")]
        Pending = 5,
        [Description("Rejected")]
        Rejected = 6,
        [Description("Open")]
        Open = 7,
        [Description("Closed")]
        Closed = 8,
        [Description("Requested")]
        Requested = 9,
        [Description("Assigned")]
        Assigned = 10,
        [Description("Waiting to Start")]
        WaitingtoStart = 11,
    }
}
