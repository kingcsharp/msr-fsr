using MSR.Domain.Models;
using System.IO;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IUploadFiles
    {
        public Task<string> UploadFile(FileModel file, string entityName, int entityId);
        public Task<string> UploadHelpFile(FileModel file);
        public Task<string> UploadImportFile(FileModel file);
        public Task<string> UploadFile(MemoryStream stream, FileModel file, string entityName, int entityId);
    }
}
