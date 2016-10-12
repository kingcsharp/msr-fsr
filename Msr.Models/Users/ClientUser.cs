using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Users
{
    public class ClientUser 
    {
        [Required]
        [Key]
        public virtual Guid Id { get; set; }

        public virtual string UserId { get; set; }
        public virtual AspNetUser User { get; set; }

        public virtual string ClientId { get; set; }
        public virtual AspNetUser Client { get; set; }

        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual AspNetUser CreatedByUser { get; set; }
    }
}