using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkOrder : Command
    {
        public int? Id { get; set; }
    }
}
