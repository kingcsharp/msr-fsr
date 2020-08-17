using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UploadFile: Command
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
    }
}
