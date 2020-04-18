using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class ForgotUserName: Command
    {
        public string Email { get; set; }
    }
}
