using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;

namespace MSR.Answer.API.Extentions
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
    }
}
