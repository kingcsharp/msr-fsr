using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Account.Abstractions
{
    public interface IAccountService
    {
        Task<User> LoginAsync(SystemLogin command);
        Task ForgotPasswordAsync(ForgotPassword command);
        Task ForgotUserNameAsync(ForgotUserName command);
        Task ResetPasswordAsync(ResetPassword command);
    }
}
