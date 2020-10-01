using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepRoleMap))]
    public class ProcedureStepRoleMap: TrackableEntity
    {
        public int ProcedureStepId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
