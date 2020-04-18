using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class ResetPassword: Command
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
