using System.Collections.Generic;

namespace Msr.Models.Users
{
    public partial class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
