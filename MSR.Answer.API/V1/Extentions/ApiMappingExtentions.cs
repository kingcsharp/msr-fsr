using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;

namespace MSR.Answer.API.V1.Extentions
{
    public static class ApiMappingExtentions
    {
        public static SystemLogin ToSystemLoginCommand(this SystemLoginRequest request)
        {
            return new SystemLogin()
            {
                UserName = request.UserName,
                Password = request.Password
            };
        }

        public static ResetPassword ToResetPasswordCommand(this ResetPasswordRequest request)
        {
            return new ResetPassword()
            {
                Token = request.Token,
                Password = request.NewPassword
            };

        }

        public static ForgotPassword ToForgotPasswordCommand(this ForgotPasswordRequest request)
        {
            return new ForgotPassword()
            {
                UserName = request.UserName
            };
        }
        
        public static ForgotUserName ToForgotUserNameCommand(this ForgotUserNameRequest request)
        {
            return new ForgotUserName()
            {
                Email = request.Email
            };
        }

        public static GetUsers ToGetUsersCommand(this GetUsersRequest request)
        {
            return new GetUsers()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Title = request.Title,
                Supervisor = request.Supervisor,
                PrimaryPhone = request.PrimaryPhone,
                Email = request.Email
            };
        }
    }
}
