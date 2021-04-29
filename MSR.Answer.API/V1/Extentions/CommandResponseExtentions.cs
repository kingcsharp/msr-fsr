using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Domain.Models.BaseModels;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MSR.Answer.API.V1.Extentions
{
    public static class CommandResponseExtentions
    {
        public static T ToEntity<T>(this ICommandResponse commandResponse)
        {
            return ((ICommandResponse<T>)commandResponse).Data;
        }

        public static IActionResult ToOkObjectResponse<TResult>(this ICommandResponse commandResponse, string message = null)
        {
            IActionResult error = ValidateCommandResponse(commandResponse);
            if (error != null)
            {
                // error case
                return error;
            }
            var dataObject = ((ICommandResponse<TResult>)commandResponse).Data;
            int id = 0;

            if (dataObject is EntityModel)
            {
                id = (dataObject as EntityModel).Id;
            }

            if (commandResponse is IPagingCommandResponse<TResult>) 
            {

                var pagedCommandResponse = commandResponse as IPagingCommandResponse<TResult>;

                int? totalNumberOfRecords = pagedCommandResponse.TotalRows;
                int? pageNumber = pagedCommandResponse.PageNumber;
                int? pageSize = pagedCommandResponse.PageSize;
                bool? sortAscending = pagedCommandResponse.SortAscending;
                string term = pagedCommandResponse.Term;

                return new OkObjectResult(new AuditActionResult<TResult>()
                {
                    Object = dataObject,
                    SuccessMessage = message,
                    Id = id,
                    TotalNumberOfRecords = totalNumberOfRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    SortAscending = sortAscending,
                    Term = term
                });

            } 
            else 
            {

                return new OkObjectResult(new AuditActionResult<TResult>()
                {
                    Object = dataObject,
                    SuccessMessage = message,
                    Id = id
                });

            }

            
            
            
        }

        public static IActionResult ToOkObjectResponse(this ICommandResponse commandResponse, string message = null)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new OkObjectResult(new AuditActionResult()
            {
                SuccessMessage = message
            });
        }

        public static IActionResult ToAcceptedObjectResponse<TResult>(this ICommandResponse commandResponse, string message = null)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new AcceptedResult(commandResponse.GetType().Name, new AuditActionResult<TResult>()
            {
                Object = ((ICommandResponse<TResult>)commandResponse).Data,
                SuccessMessage = message
            });
        }

        public static IActionResult ToFileResponse<TResult>(this ICommandResponse commandResponse, ControllerBase controller, string fileName)
        {
            var result = ValidateCommandResponse(commandResponse);

            return result ?? controller.File(((ICommandResponse<TResult>)commandResponse).Data as MemoryStream, "application/octet-stream", fileName);
        }

        public static IActionResult ToNoContentResponse(this ICommandResponse commandResponse)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new NoContentResult();
        }
        public static IActionResult ToImportOkObjectResponse<TResult>(this ICommandResponse commandResponse, string message = null)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new OkObjectResult(new ImportAuditActionResult<TResult>
            {
                ImportErrors = ((ICommandResponse<IEnumerable<ImportError>>)commandResponse).Data,
                SuccessMessage = message
            });
        }

        public static IActionResult ToCreatedResponse<TResult>(this ICommandResponse commandResponse, string message = null)
        {
            var result = ValidateCommandResponse(commandResponse);
            return result ?? new CreatedResult("", new AuditActionResult<TResult>()
            {
                Object = ((ICommandResponse<TResult>)commandResponse).Data,
                SuccessMessage = message
            });
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
                if (commandResponse.Again)
                {
                    return RetryError(commandResponse.ResponseError.Exception.ToString());
                }

                return InternalServerError(commandResponse.ResponseError.Exception.ToString());
            }

            return null;
        }

        private static IActionResult InternalServerError(string message = null)
        {
            return new ObjectResult(new AuditActionResult(!string.IsNullOrWhiteSpace(message) ? message : "API Ran Into an error.  Kick it and try again."))
            { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        private static IActionResult RetryError(string message = null)
        {
            return new ObjectResult(new AuditActionResult(!string.IsNullOrWhiteSpace(message) ? message : "Retry Error"))
            { StatusCode = (int)HttpStatusCode.ServiceUnavailable };
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
