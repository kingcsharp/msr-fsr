using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Models;

namespace MSR.Domain.Abstractions
{
    public interface IFileHandlerFactory
    {
        public IUploadFiles CreateUploader(FileProvider provider);
        public IDownloadFiles CreateDownloader(FileProvider provider);
    }
}
