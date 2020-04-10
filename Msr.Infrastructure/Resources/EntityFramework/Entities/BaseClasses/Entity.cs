using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.BaseClasses
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}
