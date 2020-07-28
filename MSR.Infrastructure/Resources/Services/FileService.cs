using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File = MSR.Infrastructure.Resources.EntityFramework.Entities.File;

namespace MSR.Infrastructure.Resources.Services
{
    public class FileService : IFileService
    {
        IUploadFiles _fileUploader;
        IDownloadFiles _fileDownloader;
        IUnitOfWork _unitOfWork;

        public FileService(IFileHandlerFactory fileHanderFactory, IUnitOfWork unitOfWork)
        {
            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _fileDownloader = fileHanderFactory.CreateDownloader(FileProvider.S3);
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateDocumentAsync<T>(T entity, int entityId, FileModel file) where T : class
        {
            throw new NotImplementedException();
        }

        public ICollection<FileModel> ListFiles<T>(T entity, int entityId) where T: class
        {
            var tableName = mapEntityToTable(entity.GetType().Name);
            var files = _unitOfWork.FileEntityMap.Query().Where(x =>
                x.EntityTableName == tableName &&
                x.EntityId == entityId
                ).Select(x =>
                    x.FileObject
                ).ToList();

            List<FileModel> ret = new List<FileModel>();
            foreach (var x in files) {
                var fileURL = _fileDownloader.GetURL(x.FileURL, 6000);
                ret.Add(new FileModel()
                {
                    Name = x.Name,
                    FileURL = fileURL
                });
            }

            return ret;
        }

        public ICollection<FileModel> ListFilesForEntitySet(string tableName, ICollection<int> entityIds)
        {
            var files = _unitOfWork.FileEntityMap.Query()
                .Include(x => x.FileObject)
                .Where(x => x.EntityTableName == tableName && entityIds.Contains(x.EntityId))
                .Select(x => new FileModel()
                {
                    FileId = x.Id,
                    Name = x.FileObject.Name,
                    FileURL = "", // Not available here because it requires a call to AWS
                    EntityId = x.EntityId,
                    ContentType = x.FileObject.ContentType
                }).ToList();

            return files;
        }

        public async Task<bool> CreateFileAsync<T>(T entity, int entityId, FileModel file) where T : class
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
