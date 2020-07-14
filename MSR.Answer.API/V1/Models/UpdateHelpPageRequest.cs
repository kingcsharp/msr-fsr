using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateHelpPageRequest
    {
        public int HelpPageId { get; set; }
        public string Title { get; set; }
        public string FriendlyURL { get; set; }
        public string HelpContent { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
