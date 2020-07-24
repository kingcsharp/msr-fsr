using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IFileService
    {
        Task<bool> CreateFileAsync<T>(T entity, int entityId, FileModel file) where T : class;
        ICollection<FileModel> ListFiles<T>(T entity, int entityId) where T : class;
        ICollection<FileModel> ListFiles2(string tableName, ICollection<int> entityIds);
        Task<bool> DeleteFilesAsync<T>(T entity, int entityId) where T : class;
        Task<bool> CreateDocumentAsync<T>(T entity, int entityId, FileModel file) where T : class;
    }
}
