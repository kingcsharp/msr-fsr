using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderTask : CreateWorkOrderTask
    {
        public int Id { get; set; }
    }
}
