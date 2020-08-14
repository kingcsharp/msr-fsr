using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Events
{
    public class ImportEvent: BaseImportEvent
    {
        public EnumMenuItem MenuItem { get; set; }
    }
}
