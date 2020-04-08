using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Commands
{
    public class SystemLogin: ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
