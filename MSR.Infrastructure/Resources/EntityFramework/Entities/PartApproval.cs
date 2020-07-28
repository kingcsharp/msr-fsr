using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PartApproval))]
    public partial class PartApproval: ApprovalEntity
    {
        public int? PartId { get; set; }

        [Required]
        [StringLength(100)]
        public string PartNumber { get; set; }

        [StringLength(100)]
        public string OEMPartNumber { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        public int? MaximumCycles { get; set; }

        public string Comments { get; set; }
    }
}
