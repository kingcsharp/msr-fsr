using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceModel> GetInvoiceAsync(int id);
        Task<IEnumerable<InvoiceModel>> GetInvoicesAsync(GetInvoices command);
        Task<IEnumerable<InvoiceView>> GetInvoicesAsync(GetInvoicesGridView command);
        Task<InvoiceView> CreateInvoiceAsync(CreateOneInvoice command);

        Task<IEnumerable<InvoiceView>> CreateInvoicesAsync(CreateIndividualInvoices command);

        Task<InvoiceView> UpdateInvoiceAsync(UpdateInvoice command);
    }
}
