using MSR.Domain.Commands;
using MSR.Domain.Commanding.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.Services.Account.Abstractions;
using MSR.Domain.Commanding;

namespace MSR.Application
{
    public class MsrAppService : ICommandHandler<SystemLogin>
    {
        private readonly IAccountService _accountService;
        public MsrAppService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        

        public Task<ICommandResponse> HandleAsync(SystemLogin command, CancellationToken cancellationToken = default)
        {
            var ret = _accountService.LoginAsync(command);
            return new CommandResponse(ret);
        }
    }

}
