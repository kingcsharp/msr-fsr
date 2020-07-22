using MSR.Domain.Commanding.Abstractions;
using System;

namespace MSR.Domain.Commanding
{
    public class CommandResponse<T> : CommandResponse, ICommandResponse<T>
    {
        public T Data { get; }

        public CommandResponse(T data)
        {
            Data = data;
            Success = true;
        }

        public CommandResponse(Exception ex)
        : base(ex) { }
    }

    public class CommandResponse: ICommandResponse
    {
        public CommandResponse() { }

        public bool Success { get; set; }

        public CommandResponse(Exception ex)
        {
            ResponseError = new Error() { Exception = ex, Message = ex.Message };
            Success = false;
        }

        public Error ResponseError { get; }

        public static ICommandResponse Error(Exception ex) => new CommandResponse(ex) { Success = false };

        public static ICommandResponse SuccessCommand => new CommandResponse() { Success = true };
    }
}
