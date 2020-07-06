using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdateHelpPage: Command
    {
        public int HelpPageId { get; set; }
        public string Title { get; set; }
        public string FriendlyURL { get; set; }
        public string HelpContent { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
