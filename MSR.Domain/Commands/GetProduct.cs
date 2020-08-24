using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetProduct : Command
    {
        public int? Id { get; set; }
    }
}
