using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetQuote : Command
    {
        public int? Id { get; set; }
    }
}
