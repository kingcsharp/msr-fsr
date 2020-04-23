using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class UserAppService :
         ICommandHandler<GetUsers>
    {
        private IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ICommandResponse> HandleAsync(GetUsers command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.GetUsers(command);
            return new CommandResponse<ICollection<User>>(ret);
        }
    }
}
