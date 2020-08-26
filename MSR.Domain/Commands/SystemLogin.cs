using MSR.Domain.Commanding;
using MSR.Domain.Models;

namespace MSR.Domain.Commands
{
    public class SystemLogin: Command<UserModel>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
