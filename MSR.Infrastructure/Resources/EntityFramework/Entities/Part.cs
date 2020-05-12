using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Part))]
    public partial class Part: TrackableEntity
    {
        public Part()
        {
            Children = new HashSet<Part>();
            WorkOrderParts = new HashSet<WorkOrderPart>();
        }

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

        public virtual ICollection<Part> Children { get; set; }

        public virtual Part Parent { get; set; }

        public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; }
    }
}
