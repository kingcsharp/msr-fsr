using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Notes
{
    public class Note
    {
        [Required]
        [Key]
        public virtual Guid Id { get; set; }

        public virtual string EntityId { get; set; }
        public virtual int EntityTypeId { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
    }
}
