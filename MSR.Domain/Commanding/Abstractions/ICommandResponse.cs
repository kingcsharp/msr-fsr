namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandResponse
    {
        bool CanTryAgain { get; set; }
        Error ResponseError { get; }
        string DisplayString { get; }
        bool Success { get; }
    }

    public interface ICommandResponse<TResult> : ICommandResponse
    {
        TResult Data { get; }
    }

}
