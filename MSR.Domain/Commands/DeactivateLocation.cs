using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class DeactivateLocation: Command
    {
        public int LocationId { get; set; }
    }
}
