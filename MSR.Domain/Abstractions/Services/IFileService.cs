using MSR.Answer.Domain.Models;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IFileService
    {
        Task<FileModel> CreateFileAsync(string entityName, int entityId, FileModel file);
        ICollection<FileModel> ListFiles(string entityName, int entityId, int? fileId = null);
        ICollection<FileModel> ListFilesForEntitySet(string tableName, ICollection<int> entityIds);
        Task<int> DetachFilesAsync(string entityName, int entityId, int? fileId = null);
        Task<ICollection<FileModel>> AttachFilesAsync(string entityName, int entityId, ICollection<FileModel> files);
        Task<bool> CreateDocumentAsync<T>(T entity, int entityId, FileModel file) where T : class;
        Task<UploadResponse> UploadHelpFile(UploadFile command);
        Task<UploadResponse> UploadImportFile(UploadFile command);

        /// <summary>
        /// Map an uploaded file to an entity name + id
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityId"></param>
        /// <param name="uploadedFileId"></param>
        /// <returns></returns>
        Task<FileModel> MapUploadedFileAsync(string entityName, int entityId, int uploadedFileId);
        Task<bool> EditPdfFile(FileModel file, DocumentView document);
    }
}
