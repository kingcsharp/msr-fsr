namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public partial class Customer: DeletableEntity
    {
        public int OldId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int? PrimaryContactUserId { get; set; }

        public virtual User PrimaryContactUser { get; set; }

        public int? SecondaryContactUserId { get; set; }

        public virtual User SecondaryContactUser { get; set; }

        public virtual Location Location { get; set; }

        public int? LocationId { get; set; }
        public string CustomerNumber { get; set; }
    }
}
