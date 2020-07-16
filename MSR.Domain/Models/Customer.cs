using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class Customer: TrackableModel
    {
        public int OldId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public User PrimaryContactUser { get; set; }
        public User SecondaryContactUser { get; set; }
        public LocationModel Location { get; set; }
        public string CustomerNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
