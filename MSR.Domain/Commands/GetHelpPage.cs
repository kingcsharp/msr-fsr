using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetHelpPage: PagingCommand
    {
        public int? Id { get; set; }
        public string FriendlyURL { get; set; }
        public string Title { get; set; }
        public int[] Roles { get; set; }
    }
}
