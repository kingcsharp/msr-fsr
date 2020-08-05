using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(UserApproval))]
    public partial class UserApproval : TrackableEntity
    {
        public UserApproval()
        {
            UserRoleApprovals = new HashSet<UserRoleApproval>();
        }

        public int WorkflowId { get; set; }
        [ForeignKey("WorkflowId")]
        public virtual Workflow Workflow { get; set; }
        public int WorkflowGroupId { get; set; }
        [ForeignKey("WorkflowGroupId")]
        public virtual WorkflowGroup WorkflowGroup { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public int? OldId { get; set; }

        [StringLength(50)]
        public string UserRoleId { get; set; }

        [Required]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string Phone { get; set; }

        public int? SupervisorId { get; set; }

        public bool IsActive { get; set; }

        public bool? IsAnswerUser { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public int? TimeZoneId { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<UserRoleApproval> UserRoleApprovals { get; set; }

        [NotMapped]
        public string ActivityType
        {
            get
            {
                return this.GetType().Name.Replace("Proxy", "");
            }
        }

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
