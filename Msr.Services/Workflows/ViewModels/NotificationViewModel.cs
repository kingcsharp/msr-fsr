using System.Collections.Generic;
using Msr.Models.Workflows;

namespace Msr.Services.Workflows.ViewModels
{
    public class NotificationViewModel
    {
        public int Total { get; set; }
        public IEnumerable<NotificationItemView> Items { get; set; }
        public Queue<string> IconCssList { get; private set; }

        public NotificationViewModel()
        {
            IconCssList = new Queue<string>();
            IconCssList.Enqueue("badge");
            IconCssList.Enqueue("badge success");
            IconCssList.Enqueue("badge danger");
        }
    }
}
