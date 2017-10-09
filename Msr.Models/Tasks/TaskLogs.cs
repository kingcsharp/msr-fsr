using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Tasks
{
    public class TaskLog
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int StatusId  {get; set; }
        public string UserId { get; set; }
        public int FillId { get; set; }
    }
}
