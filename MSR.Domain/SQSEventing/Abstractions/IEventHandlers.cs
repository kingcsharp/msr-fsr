using System;

namespace MSR.Domain.SQSEventing.Abstractions
{
    public interface IEventHandlers
    {
        void AddReference(Type type);
        Type GetReference(string typeName);
    }
}
