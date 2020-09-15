using System.ComponentModel;

namespace MSR.Domain.Models
{
    public enum ApprovalStatusEnum
    {
        [Description("Approved")]
        Approved = 1,
        [Description("InProgress")]
        InProgress = 2,
        [Description("Complete")]
        Complete = 3,
        [Description("Cancelled")]
        Cancelled = 4,
        [Description("Pending")]
        Pending = 5,
        [Description("Rejected")]
        Rejected = 6
    }
}
