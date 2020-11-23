using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Models
{
    public class GetArchiveDocumentRequest
    {
        public EnumAwsFolders Folder { get; set; }
    }
}
