using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Msr.Models.Users
{
    public partial class AspNetRole
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
