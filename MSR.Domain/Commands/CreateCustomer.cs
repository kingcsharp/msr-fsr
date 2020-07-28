using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateCustomer: Command
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? LocationId { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public int? SecondaryContactUserId { get; set; }
        public bool IsActive { get; set; }
        public string CustomerNumber { get; set; }
    }
}
