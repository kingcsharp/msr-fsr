using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Commands
{
    public class ImportFile: Command
    {
        public string Base64Data { get; set; }
        public EnumMenuItem MenuItem { get; set; }
    }
}
