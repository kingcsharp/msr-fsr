using System;

namespace Msr.Models.Orders
{
    public class UserView
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
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
        public string ParentId { get; set; }
    }
}
