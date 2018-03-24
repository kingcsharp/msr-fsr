using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Users
{
    public partial class AspNetUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string TimeZone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CompanyId { get; set; }
        public string AnswerId { get; set; }
        public string ParentId { get; set; }
        public bool PortalUser { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
