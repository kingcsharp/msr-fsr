using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Commanding
{
    public class CommandResponse<T> : ICommandResponse<T>
    {
        public T Data { get; }

        public CommandResponse(T data)
        {
            Data = data;
        }
    }
}
