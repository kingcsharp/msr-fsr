using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommand<out T> : ICommand
    {

    }

    public interface ICommand
    {
    }
}
