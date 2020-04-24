using MSR.Domain.Abstractions.Email;
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
    public class UserAppService:
        ICommandHandler<GetUsers>,
        ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ICommandResponse> HandleAsync(GetUsers command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.GetUsersAsync(command);
            return new CommandResponse<ICollection<User>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateUser command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.CreateUserAsync(command);
            return new CommandResponse<User>(ret);
        }
    }
}
