using System;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class User : TrackableEntity
    {
        public int OldId { get; set; }
        public string UserRoleId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string Phone { get; set; }
        public int SupervisorId { get; set; }
        public int LocationId { get; set; }
        public bool IsAnswerUser { get; set; }
        public string CustomerId { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int TimeZoneId { get; set; }
    }
}
