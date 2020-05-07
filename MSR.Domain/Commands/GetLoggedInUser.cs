using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetLoggedInUserData: Command
    {
        public int UserId { get; set; }
    }
}
