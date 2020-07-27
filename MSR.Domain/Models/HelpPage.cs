using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class HelpPage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FriendlyURL { get; set; }
        public string Content { get; set; }
        public List<Role> Roles { get; set; }
    }
}
