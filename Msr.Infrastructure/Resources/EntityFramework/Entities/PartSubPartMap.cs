using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PartSubPartMap))]
    public class PartSubPartMap:Entity
    {
        public int PartId { get; set; }
        public int ParentPartId { get; set; }
        [ForeignKey("ParentPartId")]
        public virtual Part ParentPart { get; set; }
        [ForeignKey("PartId")]
        public virtual Part Part { get; set; }
        public int Qty { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual int? CreatedBy { get; set; }
    }
}
