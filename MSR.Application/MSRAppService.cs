using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.Services.Account.Abstractions;
using MSR.Domain.Commanding;
using MSR.Domain.Models;

namespace MSR.Application
{
    public class MsrAppService : 
        ICommandHandler<SystemLogin>,
        ICommandHandler<ForgotPassword>,
        ICommandHandler<ResetPassword>,
        ICommandHandler<ForgotUserName>
    {
        private readonly IAccountService _accountService;
        public MsrAppService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        

        public async Task<ICommandResponse> HandleAsync(SystemLogin command, CancellationToken cancellationToken = default)
        {
                var ret = await _accountService.LoginAsync(command);
                return new CommandResponse<User>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(ForgotPassword command, CancellationToken cancellationToken = default)
        {
            await _accountService.ForgotPasswordAsync(command);
            return new CommandResponse();

        }

        public async Task<ICommandResponse> HandleAsync(ResetPassword command, CancellationToken cancellationToken = default)
        {
            await _accountService.ResetPasswordAsync(command);
            return new CommandResponse();

        }

        public async Task<ICommandResponse> HandleAsync(ForgotUserName command, CancellationToken cancellationToken = default)
        {
            await _accountService.ForgotUserNameAsync(command);
            return new CommandResponse();
        }
    }

}

