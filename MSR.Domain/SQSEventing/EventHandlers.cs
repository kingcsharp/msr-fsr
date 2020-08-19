using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Collections.Concurrent;

namespace MSR.Domain.SQSEventing
{
    public class EventHandlers : IEventHandlers
    {
        private ConcurrentDictionary<string, Type> _typeHandlers = new ConcurrentDictionary<string, Type>();

        public void AddReference(Type type)
        {
            _typeHandlers.TryAdd(type.Name, type);
        }

        public Type GetReference(string typeName)
        {
            return _typeHandlers[typeName];
        }
    }
}
