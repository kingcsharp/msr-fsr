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
        ICommandHandler<CreateUser>,
        ICommandHandler<DeactivateUser>,
        ICommandHandler<UpdateUser>,
        ICommandHandler<GetLoggedInUserData>,
        ICommandHandler<CreateUserRole>,
        ICommandHandler<UpdateUserRole>,
        ICommandHandler<DeleteUserRole>
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

        public async Task<ICommandResponse> HandleAsync(DeactivateUser command, CancellationToken cancellationToken = default)
        {
            await _userService.DeactivateUserAsync(command);
            return new CommandResponse();
        }

        public async Task<ICommandResponse> HandleAsync(UpdateUser command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.UpdateUserAsync(command);
            return new CommandResponse<User>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetLoggedInUserData command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.GetLoggedInUserData(command.UserId);
            return new CommandResponse<User>(ret);
        }

        public Task<ICommandResponse> HandleAsync(UpdateUserRole command, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResponse> HandleAsync(CreateUserRole command, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommandResponse> HandleAsync(DeleteUserRole command, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
