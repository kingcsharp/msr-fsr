using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Helpers;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        public int UserId => CurrentUser.GetId();

        public string Controller => this.ControllerContext.RouteData.Values["controller"].ToString();

        protected IActionResult GenerateOkViewResponse<T>(T responseData, int totalRows = 0)
        {
            return Ok(new AuditActionResult<T>()
            {
                TotalNumberOfRecords = totalRows,
                Object = responseData
            });
        }

        protected static string DetermineResponseMessage<T>(ICommandResponse commandResponse, string action, string actionObjectName)
        {
            if (commandResponse is CommandResponse &&
                !(((CommandResponse)commandResponse).Success))
            {
                return $"{actionObjectName} {action} Failed: " +
                    commandResponse.ResponseError?.Message;
            }

            var response = $"{actionObjectName} {action} Successful";
            var entity = commandResponse.ToEntity<T>();

            if (entity == null) {
                if (commandResponse.ResponseError != null)
                {
                    response = $"{actionObjectName} {action} Failed: " +
                        commandResponse.ResponseError.Message;
                }
                else
                {
                    response = $"{actionObjectName} {action} Failed";
                }
            }
            else if (!string.IsNullOrWhiteSpace(commandResponse.DisplayString))
            {
                response = commandResponse.DisplayString;
            }

            return response;
        }
    }
}
