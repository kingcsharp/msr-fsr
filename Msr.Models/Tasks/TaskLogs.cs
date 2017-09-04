using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Tasks
{
    public class TaskLog
    {
        [Key]
        public int Id { get; set; }
        public string TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double TotalTime { get; set; }
        public int StatusId  {get; set; }
        public string UserId { get; set; }
    }
}
