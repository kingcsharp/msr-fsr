using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Models;

namespace MSR.Domain.Commands
{
    public class SystemLogin: ICommand<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
