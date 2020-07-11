using System.Collections.Generic;
namespace MSR.Domain.Models
{
    public class PendingApprovalNotification
    {
        public ICollection<PendingNotificationItem> Items { get; set; }
    }
}
