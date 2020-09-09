
namespace MSR.Domain.Models
{
    public class TimeZoneModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Offset { get; set; }
        public int Number { get; set; }
        public int UseDalightSavings { get; set; }
    }
}
