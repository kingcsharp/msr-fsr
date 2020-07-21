using AutoMapper.Configuration.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PartSubPartMap))]
    public partial class PartSubPartMap : TrackableEntity
    {
        public int ParentPartId { get; set; }
        [ForeignKey("ParentPartId")]
        public virtual Part ParentPart { get; set; }
        public int PartId { get; set; }
        [ForeignKey("PartId")]
        public virtual Part Part { get; set; }
        public int Qty { get; set; }

        // ignore non-existent fields
        [NotMapped]
        public override DateTime? LastUpdatedOn { get; set; }
        [NotMapped]
        public override int? LastUpdatedBy { get; set; }
    }
}
