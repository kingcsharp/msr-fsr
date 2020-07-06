using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class CreateHelpPageRole: Command
    {
        public int HelpPageId { get; set; }
        public int RoleId { get; set; }
    }
}
