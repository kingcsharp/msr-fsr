namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public partial class Customer: TrackableEntity
    {
        public int OldId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int? PrimaryContactUserId { get; set; }

        public User PrimaryContactUser { get; set; }

        public int? SecondaryContactUserId { get; set; }

        public User SecondaryContactUser { get; set; }

        public virtual Location Location { get; set; }


    }
}
