using System.Collections.Generic;

namespace Msr.Models.Users
{
    public partial class AspNetUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
