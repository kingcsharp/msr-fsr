using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using File = MSR.Infrastructure.Resources.EntityFramework.Entities.File;

namespace MSR.Infrastructure.Resources.Services
{
    public class FileService : IFileService
    {
        IUploadFiles _fileUploader;
        IUnitOfWork _unitOfWork;

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
            var tableName = mapEntityToTable(entity.GetType().Name);
            var url = await _fileUploader.UploadFile(file, tableName, entityId);

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
        public async Task<bool> DeleteFilesAsync<T>(T entity, int entityId) where T : class
        {
            string tableName = mapEntityToTable(entity.GetType().Name);

            foreach (var e in _unitOfWork.FileEntityMap.Query().Where(x =>
                    x.EntityTableName == tableName &&
                    x.EntityId == entityId
                ).ToList())
            {
                _unitOfWork.FileEntityMap.Delete(false, e);
            }

            await _unitOfWork.SaveChangesAsync();
            return false;
        }

        public static string mapEntityToTable(string entityName)
        {
            return entityName.Replace("Model", "");
        }
    }
}
