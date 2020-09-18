using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class GetSearch: Command
    {
        public string SearchTerm { get; set; }
    }
}
