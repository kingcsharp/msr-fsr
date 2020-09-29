using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetDocument : Command
    {
        public int? Id { get; set; }
    }
}
