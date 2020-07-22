using System.IO;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IDownloadFiles
    {
        public Task<Stream> DownloadFile(string fileName);
        public string GetURL(string key, int expiresIn);
    }
}
