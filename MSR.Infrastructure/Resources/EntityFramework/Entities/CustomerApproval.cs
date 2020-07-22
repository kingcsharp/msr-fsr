using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(CustomerApproval))]
    public partial class CustomerApproval: ApprovalEntity
    {
        public int CustomerId { get; set; }

        public int OldId { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual User PrimaryContactUser { get; set; }

        public virtual User SecondarContactUser { get; set; }

        public int? LocationId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Location Location { get; set; }

        public bool IsActive { get; set; }
        public string CustomerNumber { get; set; }
    }
}
