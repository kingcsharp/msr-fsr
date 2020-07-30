using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Threading.Tasks;
using File = MSR.Infrastructure.Resources.EntityFramework.Entities.File;

namespace MSR.Infrastructure.Resources.Services
{
    public class FileService : IFileService
    {
        IUploadFiles _fileUploader;
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IFileHandlerFactory fileHanderFactory, IUnitOfWork unitOfWork)
        {
            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateDocumentAsync<T>(T entity, int entityId, Domain.Models.File file) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateFileAsync<T>(T entity, int entityId, Domain.Models.File file) where T : class
        {
            var url = await _fileUploader.UploadFile(file);

            var tableName = entity.GetType().Name.Replace("Model", "");

            var efFile = new File()
            {
                ContentType = file.ContentType,
                FileURL = url,
                Name = file.Name
            };

            _unitOfWork.Files.Add(efFile);
            await _unitOfWork.SaveChangesAsync();

            var fileEntityMap = new FileEntityMap()
            {
                EntityId = entityId,
                EntityTableName = tableName,
                FileId = efFile.Id
            };

            _unitOfWork.FileEntityMap.Add(fileEntityMap);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
