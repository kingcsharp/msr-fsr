namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommand<out T> : ICommand
    {

    }

    public interface ICommand
    {
    }
}
