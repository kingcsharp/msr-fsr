using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.BaseClasses
{
    public class DeletableEntity : DomainEntity
    {
        public virtual bool IsActive { get; set; }
    }
}
