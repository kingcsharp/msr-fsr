using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetHelpPage: Command
    {
        public int? Id { get; set; }
        public string FriendlyURL { get; set; }
    }
}
