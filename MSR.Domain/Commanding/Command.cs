using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Commanding
{
    public abstract class Command<TResult> : Command, ICommand<TResult>, ICommand
    {
    }

    public abstract class Command: ICommand
    {

    }
}
