using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IUploadFiles
    {
        public Task<string> UploadFile(File file, string entityName, int entityId);
    }
}
