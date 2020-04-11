using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}
