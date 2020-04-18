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
        }

        public CommandResponse(Exception ex)
        : base(ex) { }
    }

    public class CommandResponse: ICommandResponse
    {
        public CommandResponse() { }
        public CommandResponse(Exception ex)
        {
            ResponseError = new Error() { Exception = ex, Message = ex.Message };
        }

        public Error ResponseError { get; }

        public static ICommandResponse Error(Exception ex) => new CommandResponse(ex);
    }
}
