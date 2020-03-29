using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
    }
}
