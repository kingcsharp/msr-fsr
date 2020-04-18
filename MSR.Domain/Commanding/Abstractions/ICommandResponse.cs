namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandResponse
    {
        Error ResponseError { get; }
    }

    public interface ICommandResponse<TResult> : ICommandResponse
    {
        TResult Data { get; }
    }
}
