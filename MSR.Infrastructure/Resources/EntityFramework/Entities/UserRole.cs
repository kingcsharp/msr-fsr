namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UserRole: TrackableEntity
    {
        public DateTime? CertificationFromDate { get; set; }

        public DateTime? CertificationToDate { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
