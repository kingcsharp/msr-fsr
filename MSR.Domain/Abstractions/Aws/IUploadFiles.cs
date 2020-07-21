using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IUploadFiles
    {
        public Task<bool> UploadFile(string content, string contentType, string fileName, string location);
    }
}
