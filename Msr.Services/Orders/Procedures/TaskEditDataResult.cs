
namespace Msr.Services.Orders.Procedures
{
    public class TaskEditDataResult
    {
        public string Description { get; set; }

        public string Status { get; set; }

        public string Assignee { get; set; }

        public bool? HasMonitor { get; set; }

        public string SystemTask { get; set; }

        public string RequesteeId { get; set; }
    }
}
