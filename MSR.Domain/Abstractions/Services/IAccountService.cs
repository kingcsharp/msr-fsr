using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Account.Abstractions
{
    public interface IAccountService
    {
        Task<User> LoginAsync(SystemLogin command);
    }
}
