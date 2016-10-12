using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Orders
{
    public class UserSummary
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Login { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Phone { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsActive { get; set; }
        public int TimeZone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Phone2 { get; set; }
        public string FullName { get; set; }
    }
}
