using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class CreatableEntity : Entity
    {
        public virtual DateTime CreatedOn { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User Created { get; set; }
        public virtual int CreatedBy { get; set; }
    }
}
