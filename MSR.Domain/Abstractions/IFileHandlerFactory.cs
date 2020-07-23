using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Abstractions
{
    public interface IFileHandlerFactory
    {
        public IUploadFiles CreateUploader(FileProvider provider);
        public IDownloadFiles CreateDownloader(FileProvider provider);
    }
}
