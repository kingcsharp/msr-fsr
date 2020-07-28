using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IUploadFiles
    {
        public Task<string> UploadFile(FileModel file, string entityName, int entityId);
    }
}
