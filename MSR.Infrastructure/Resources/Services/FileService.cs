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

        public ICollection<FileModel> ListFiles(string entityName, int entityId, int? fileId = null)
        {
            var tableName = mapEntityToTable(entityName);
            List<File> files;
            if (fileId.HasValue) {
                files = _unitOfWork.FileEntityMap.Query().Where(x =>
                    x.EntityTableName == tableName &&
                    x.EntityId == entityId &&
                    x.FileId == fileId
                    ).Select(x =>
                        x.FileObject
                    ).ToList();
            } else {
                files = _unitOfWork.FileEntityMap.Query().Where(x =>
                    x.EntityTableName == tableName &&
                    x.EntityId == entityId
                    ).Select(x =>
                        x.FileObject
                    ).ToList();
            }

            List<FileModel> ret = new List<FileModel>();
            foreach (var x in files) {
                var fileURL = _fileDownloader.GetURL(x.FileURL, 6000);
                ret.Add(new FileModel()
                {
                    FileId = x.Id,
                    EntityId = entityId,
                    Base64String = "",
                    ContentType = x.ContentType,
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

        public async Task<FileModel> CreateFileAsync(string entityName, int entityId, FileModel file)
        {
            var tableName = mapEntityToTable(entityName);

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

            return new FileModel() {
                FileId = efFile.Id,
                EntityId = entityId,
                Name = file.Name,
                Base64String = "",
                ContentType = file.ContentType,
                FileURL = url
            };
        }
        public async Task<int> DetachFilesAsync(string entityName, int entityId, int? fileId = null)
        {
            string tableName = mapEntityToTable(entityName);
            int deleteCount = 0;

            if (fileId.HasValue) {
                var file = _unitOfWork.FileEntityMap.Query().FirstOrDefault(x =>
                        x.EntityTableName == tableName &&
                        x.EntityId == entityId &&
                        x.FileId == fileId.Value
                    );
                if (file != null){
                    _unitOfWork.FileEntityMap.Delete(false, file);
                    deleteCount++;
                }
            } else {
                foreach (var e in _unitOfWork.FileEntityMap.Query().Where(x =>
                        x.EntityTableName == tableName &&
                        x.EntityId == entityId
                    ).ToList())
                {
                    _unitOfWork.FileEntityMap.Delete(false, e);
                    deleteCount++;
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return deleteCount;
        }

        public async Task<int> AttachFilesAsync(string entityName, int entityId, ICollection<FileModel> files)
        {
            if (files == null) {
                return 0;
            }

            var remcount = await DetachFilesAsync(entityName, entityId);

            foreach (var file in files) {
                await CreateFileAsync(entityName, entityId, file);
                remcount += 1;
            }

            // returns the net number of files added
            return remcount;
        }

        public static string mapEntityToTable(string entityName)
        {
            return entityName.Replace("Model", "");
        }
    }
}
