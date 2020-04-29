using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Emums;
using MSR.Domain.Exceptions;
using System.Net;

namespace MSR.Answer.API.V1.Extentions
{
    public static class CommandResponseExtentions
    {
        public static IActionResult ToOkObjectResponse<TResult>(this ICommandResponse commandResponse)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new OkObjectResult(new AuditActionResult<TResult>() { Object = ((ICommandResponse<TResult>)commandResponse).Data });
        }

        public static IActionResult ToNoContentResponse(this ICommandResponse commandResponse)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new NoContentResult();
        }

        public static IActionResult ToCreatedResponse<TResult>(this ICommandResponse commandResponse)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new CreatedResult("", new AuditActionResult<TResult>() { Object = ((ICommandResponse<TResult>)commandResponse).Data });
        }

        private static IActionResult ValidateCommandResponse(ICommandResponse commandResponse)
        {
            if (commandResponse is null)
            {
                return InternalServerError();
            }

            if (commandResponse.ResponseError != null)
            {
                if (commandResponse.ResponseError.Exception is DomainException domainException)
                {
                    return HandleDomainException(domainException);
                }

                return InternalServerError(commandResponse.ResponseError.Exception.Message);
            }

            return null;
        }

        private static IActionResult InternalServerError(string message = null)
        {
            return new ObjectResult(new AuditActionResult(!string.IsNullOrWhiteSpace(message) ? message : "API Ran Into an error.  Kick it and try again."))
            { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        private static IActionResult HandleDomainException(DomainException ex)
        {
            switch (ex.ErrorType)
            {
                case DomainError.InternalServerError:
                    return InternalServerError(ex.Message);
                case DomainError.Conflict:
                    return new ConflictObjectResult(new AuditActionResult(ex.Message));
                case DomainError.NotFound:
                    return new NotFoundObjectResult(new AuditActionResult(ex.Message));
                case DomainError.Teapot:
                case DomainError.BadRequest:
                default:
                    return new ObjectResult(new AuditActionResult(ex.Message))
                    { StatusCode = (int)HttpStatusCode.BadRequest };
                   
            }
            
            
        }

    }
}
