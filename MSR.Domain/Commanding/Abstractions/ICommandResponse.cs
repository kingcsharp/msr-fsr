using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commanding.Abstractions
{
    public interface ICommandResponse
    {
    }

    public interface ICommandResponse<TResult> : ICommandResponse
    { }
}
