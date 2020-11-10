using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Events
{
    public class BaseImportEvent : IEvent
    {
        public byte[] data { get; set; }
    }
}
