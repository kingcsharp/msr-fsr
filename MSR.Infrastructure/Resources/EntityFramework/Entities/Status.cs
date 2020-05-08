using System.ComponentModel.DataAnnotations;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{ 

    public partial class Status: Entity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
