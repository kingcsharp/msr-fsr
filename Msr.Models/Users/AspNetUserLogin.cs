using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Users
{
    public partial class AspNetUserLogin
    {
        [Key]
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
