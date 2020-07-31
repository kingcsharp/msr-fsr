using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using MSR.Domain.Abstractions.QuickBooks;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Application.ApplicationServices
{
    public class QuickbooksAppService :
        ICommandHandler<FormatQuickbooks>,
        ICommandHandler<ArchiveQuickbooksInvoices>
    {

        private readonly IQuickbooksService _quickBooksService;

        public QuickbooksAppService(IQuickbooksService quickBooksService)
        {
            _quickBooksService = quickBooksService;
        }

        public async Task<ICommandResponse> HandleAsync(FormatQuickbooks command, CancellationToken cancellationToken = default)
        {
            var ret = await _quickBooksService.FormatAsync(command);
            return new CommandResponse<IEnumerable<QuickbooksFormatterModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(ArchiveQuickbooksInvoices command, CancellationToken cancellationToken = default)
        {
            var ret = await _quickBooksService.ArchiveInvoicesAsync(command);
            return new CommandResponse<MemoryStream>(ret);
        }
    }
}
