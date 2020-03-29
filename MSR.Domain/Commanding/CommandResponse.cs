using MSR.Domain.Commanding.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

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
