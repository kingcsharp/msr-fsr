using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public enum ApprovalStatus
    {
        Approved = 1,
        InProgress = 2,
        Complete = 3,
        Cancelled = 4,
        Pending = 5,
        Rejected = 6
    }
}
