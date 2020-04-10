using MSR.Domain.Commanding.Abstractions;
using MSR.Infrastructure.Resources.Services.Account.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Account
{
    public class AccountService : IAccountService
    {
        public async Task<string> LoginAsync(ICommand command)
        {
            return "afa";
        }
    }
}
