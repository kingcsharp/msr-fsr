using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.AWS;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace MSR.Infrastructure.Factories
{
    public class FileHandlerFactory: IFileHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUploadFiles CreateUploader(FileProvider provider)
        {
            switch (provider)
            {
                case FileProvider.S3:
                    return _serviceProvider.GetService<S3FileHandler>();
                default:
                    throw new NotSupportedException($"Uploader not found for {nameof(FileProvider)} {provider}");
            }
        }

        public IDownloadFiles CreateDownloader(FileProvider provider)
        {
            switch (provider)
            {
                case FileProvider.S3:
                    return _serviceProvider.GetService<S3FileHandler>();
                default:
                    throw new NotSupportedException($"Downloader not found for {nameof(FileProvider)} {provider}");
            }
        }
    }
}
