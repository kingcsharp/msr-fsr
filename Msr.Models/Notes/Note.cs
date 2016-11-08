using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Users;

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
    }
}
