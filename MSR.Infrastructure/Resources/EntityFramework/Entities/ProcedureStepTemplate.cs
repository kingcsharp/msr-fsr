using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepTemplate))]
    public partial class ProcedureStepTemplate: TrackableEntity
    {
        public string Name { get; set; }
    }
}
