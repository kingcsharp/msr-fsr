using MSR.Answer.Domain.Models;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IDocumentService
    {
        Task<DocumentView> CreateDocumentAsync(CreateDocument command);
        Task<DocumentView> UpdateDocumentAsync(UpdateDocument command);
        Task<ICollection<DocumentView>> GetDocuments(int? id);
    }
}
