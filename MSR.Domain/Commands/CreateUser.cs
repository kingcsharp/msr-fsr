using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class CreateUser: Command
    {
        public int CurrentUser { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string SecurityStamp { get; set; }
        public string Phone { get; set; }
        public int? SupervisorId { get; set; }
        public int? LocationId { get; set; }
        public bool IsActive { get; set; }
        public bool? IsAnswerUser { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int? TimeZoneId { get; set; }
    }
}
