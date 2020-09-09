using MSR.Domain.Commanding.Abstractions;

namespace MSR.Domain.Events
{
    public class BaseImportEvent : IEvent
    {
        public string CsvData { get; set; }
    }
}
