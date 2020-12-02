using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Commands
{
    public class GetArchiveDocument : Command
    {
        public EnumAwsFolders Folder { get; set; }
    }
}
