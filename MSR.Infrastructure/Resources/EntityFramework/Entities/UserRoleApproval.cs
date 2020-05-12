using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(UserRoleApproval))]
    public partial class UserRoleApproval: TrackableEntity
    {
        public int UserApprovalId { get; set; }

        public int UserId { get; set; }

        public int? RoleId { get; set; }

        public DateTime? CertificationFromDate { get; set; }

        public DateTime? CertificationToDate { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }

        public virtual UserApproval UserApproval { get; set; }
    }
}
