using System.ComponentModel.DataAnnotations.Schema;
namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Customer))]
    public partial class Customer: DeletableEntity
    {
        public Customer()
        {

        }
        public int OldId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int? PrimaryContactUserId { get; set; }

        [ForeignKey("PrimaryContactUserId")]
        public virtual User PrimaryContactUser { get; set; }

        public int? SecondaryContactUserId { get; set; }

        [ForeignKey("SecondaryContactUserId")]
        public virtual User SecondaryContactUser { get; set; }

        public virtual Location Location { get; set; }

        public int? LocationId { get; set; }
        public string CustomerNumber { get; set; }
    }
}
