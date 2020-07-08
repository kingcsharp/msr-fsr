using System.Collections.Generic;

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
