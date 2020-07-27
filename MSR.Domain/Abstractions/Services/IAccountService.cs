using MSR.Domain.Commands;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Task<string> LoginAsync(SystemLogin command);
        Task ForgotPasswordAsync(ForgotPassword command);
        Task ForgotUserNameAsync(ForgotUserName command);
        Task ResetPasswordAsync(ResetPassword command);
        bool ValidateAccount(int accountId);
    }
}
