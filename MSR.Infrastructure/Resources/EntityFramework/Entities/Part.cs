using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Part))]
    public partial class Part : TrackableEntity
    {
        public Part()
        {
            Subparts = new HashSet<PartSubPartMap>();
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
        public bool IsKit { get; set; }
        [StringLength(100)]
        public string NickName { get; set; }
        public int? MaximumCycles { get; set; }
        public virtual ICollection<PartSubPartMap> Subparts { get; set; }
        public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; }
    }
}
