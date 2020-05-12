using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PartApproval))]
    public partial class PartApproval: TrackableEntity
    {
        public int PartId { get; set; }

        public int StatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string PartNumber { get; set; }

        [StringLength(100)]
        public string OEMPartNumber { get; set; }

        public int Qty { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        public int? ParentId { get; set; }

        public int? MaximumCycles { get; set; }
    }
}
