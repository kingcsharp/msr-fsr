using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IUploadFiles
    {
        public Task<string> UploadFile(FileModel file, string entityName, int entityId);
        public Task<string> UploadHelpFile(FileModel file);
        public Task<string> UploadImportFile(FileModel file);
    }
}
