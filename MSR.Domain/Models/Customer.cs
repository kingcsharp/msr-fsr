namespace MSR.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int OldId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public User PrimaryContactUser { get; set; }
        public User SecondaryContactUser { get; set; }
        public Location Location { get; set; }
        public bool IsActive { get; set; }
    }
}
