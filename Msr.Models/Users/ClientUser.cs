using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Msr.Models.Users
{
    public class ClientUser 
    {
        [Required]
        [Key]
        public virtual Guid Id { get; set; }

        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AspNetUser User { get; set; }

        public virtual string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual AspNetUser Client { get; set; }

        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual AspNetUser CreatedByUser { get; set; }
    }
}