using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class ForgotPassword: Command
    {
        public string UserName { get; set; }
    }
}
