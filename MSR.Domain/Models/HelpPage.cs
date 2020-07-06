using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class HelpPage
    {
        public string Title { get; set; }
        public string FriendlyURL { get; set; }
        public string HelpContent { get; set; }
        public List<Role> Roles { get; set; }
    }
}
