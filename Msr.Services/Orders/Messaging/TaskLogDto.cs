using System;
using Msr.Models.Tasks;

namespace Msr.Services.Orders.Messaging
{
    public class TaskLogDto
    {
        public TaskLogDto(TaskLog taskLog)
        {
            Id = taskLog.Id;
            StatusId = taskLog.StatusId;
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public string TotalTime { get; set; }
    }
}
