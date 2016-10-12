using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Users
{
    public partial class AspNetUserClaim
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
