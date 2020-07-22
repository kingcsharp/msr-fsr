using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services
{
    public class FileService : IFileService
    {
        IUploadFiles _fileUploader;
        IUnitOfWork _unitOfWork;

        public FileService(IFileHandlerFactory fileHanderFactory, IUnitOfWork unitOfWork)
        {
            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
        }

        public async Task<bool> CreateFileAsync<T>(T entity, int entityId, string fileContent, string fileContentType, string fileName, string fileLocation) where T : class
        {
            var s3File = await _fileUploader.UploadFile(fileContent, fileContentType, fileName, fileLocation);


        }
    }
}
