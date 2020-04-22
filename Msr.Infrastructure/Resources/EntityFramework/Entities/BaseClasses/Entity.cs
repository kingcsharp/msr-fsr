using System.ComponentModel.DataAnnotations;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public abstract class Entity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
