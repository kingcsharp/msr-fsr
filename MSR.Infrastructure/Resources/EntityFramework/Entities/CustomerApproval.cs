using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(CustomerApproval))]
    public partial class CustomerApproval: ApprovalEntity
    {
        public int OldId { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public virtual int? PrimaryContactUserId { get; set; }
        public virtual User PrimaryContactUser { get; set; }
        public virtual int? SecondarContactUserId { get; set; }
        public virtual User SecondarContactUser { get; set; }
        public virtual int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string CustomerNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
