using System;

namespace MSR.Domain.Models
{
    public class TimeZone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Single Offset { get; set; }
        public int Number { get; set; }
        public int UseDalightSavings { get; set; }
    }
}
