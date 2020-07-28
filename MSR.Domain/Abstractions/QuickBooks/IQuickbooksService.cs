using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Domain.Abstractions.QuickBooks
{
    public interface IQuickbooksService
    {
        Task<IEnumerable<QuickbooksFormatterModel>> FormatAsync(FormatQuickbooks command);
        Task<MemoryStream> ArchiveInvoicesAsync(ArchiveQuickbooksInvoices command);
    }
}
