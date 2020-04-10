namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandResponse
    {
    }

    public interface ICommandResponse<TResult> : ICommandResponse
    { }
}
