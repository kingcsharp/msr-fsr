using System;

namespace Msr.Models.Workflows
{
    public class NotificationItemView
    {
        public Guid Id { get; set; }
        public int ItemCount { get; set; }
        public string ItemType { get; set; }
        public string Title { get; set; }
    }
}
