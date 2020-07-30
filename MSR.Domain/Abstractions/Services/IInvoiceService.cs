using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceModel> GetInvoiceAsync(int id);
        Task<IEnumerable<InvoiceModel>> GetInvoicesAsync(GetInvoices command);
        Task<InvoiceModel> CreateInvoiceAsync(CreateOneInvoice command);

        Task<IEnumerable<InvoiceModel>> CreateInvoicesAsync(CreateIndividualInvoices command);

        Task<InvoiceModel> UpdateInvoiceAsync(UpdateInvoice command);
    }
}
