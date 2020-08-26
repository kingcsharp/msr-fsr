using MSR.Domain.Commanding;
namespace MSR.Domain.Commands
{
    public class ResetMyPassword : Command
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}


