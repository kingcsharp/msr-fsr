using Microsoft.AspNetCore.Mvc;
using MSR.Domain.Commanding.Abstractions;

namespace MSR.Answer.API.V1.Extentions
{
    public static class AccountControllerExtentions
    {
        public static ActionResult ToOkObjectResponse<TResult>(this ICommandResponse commandResponse)
        {
            if(commandResponse is null)
            {
                return new StatusCodeResult(500);
            }

            return new ObjectResult(((ICommandResponse<TResult>)commandResponse).Data);
        }
    }
}
