using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IFileService
    {
        Task<bool> CreateFileAsync<T>(T entity, int entityId, string fileContent, string fileContentType, string fileName, string fileLocation) where T : class;
        Task<bool> CreateDocumentAsync<T>(T entity, int entityId, string fileContent, string fileContentType, string fileName, string fileLocation) where T : class;
    }
}
