using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.BaseClasses
{
    public abstract class DomainEntity
    {
        public virtual int Id { get; set; }
    }
}
