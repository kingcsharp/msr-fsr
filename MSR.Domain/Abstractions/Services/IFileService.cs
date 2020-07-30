using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IFileService
    {
        Task<bool> CreateFileAsync<T>(T entity, int entityId,File file) where T : class;
        Task<bool> CreateDocumentAsync<T>(T entity, int entityId, File file) where T : class;
    }
}
