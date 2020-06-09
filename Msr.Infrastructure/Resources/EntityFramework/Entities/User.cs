using System;
using System.Collections.Generic;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class User : DeletableEntity
    {
        public User()
        {
            Roles = new HashSet<UserRole>();
        }

        public int? OldId { get; set; }
        public string UserRoleId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string SecurityStamp { get; set; }
        public string Phone { get; set; }
        public int? SupervisorId { get; set; }
        public User Supervisor { get; set; }
        public int? LocationId { get; set; }
        public bool? IsAnswerUser { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public int? TimeZoneId { get; set; }
        public ICollection<UserRole> Roles { get; set; }

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}

