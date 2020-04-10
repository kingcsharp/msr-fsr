using MSR.Domain.Commanding.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Account.Abstractions
{
    public interface IAccountService
    {
        Task<string> LoginAsync(ICommand command);
    }
}
