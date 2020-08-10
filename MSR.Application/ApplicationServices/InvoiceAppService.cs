using AutoMapper;
using MSR.Domain.Abstractions.QuickBooks;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class InvoiceAppService :
        ICommandHandler<GetInvoice>,
        ICommandHandler<GetInvoices>,
        ICommandHandler<GetInvoicesGridView>,
        ICommandHandler<CreateOneInvoice>,
        ICommandHandler<CreateIndividualInvoices>,
        ICommandHandler<UpdateInvoice>,
        ICommandHandler<DownloadAsIIFInvoices>
    {

        private readonly IInvoiceService _invoiceService;
        private readonly IQuickbooksService _quickBooksService;
        private readonly IMapper _mapper;

        public InvoiceAppService(IInvoiceService invoiceService, IQuickbooksService quickBooksService, IMapper mapper)
        {
            _quickBooksService = quickBooksService;
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> HandleAsync(GetInvoice command, CancellationToken cancellationToken = default)
        {
            var ret = await _invoiceService.GetInvoiceAsync(command.Id);
            return new CommandResponse<InvoiceModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetInvoices command, CancellationToken cancellationToken = default)
        {
            var ret = await _invoiceService.GetInvoicesAsync(command);
            return new CommandResponse<IEnumerable<InvoiceModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetInvoicesGridView command, CancellationToken cancellationToken = default)
        {
            var ret = await _invoiceService.GetInvoicesAsync(command);
            return new CommandResponse<IEnumerable<InvoiceView>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateOneInvoice command, CancellationToken cancellationToken = default)
        {
            var ret = await _invoiceService.CreateInvoiceAsync(command);
            return new CommandResponse<InvoiceModel>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateIndividualInvoices command, CancellationToken cancellationToken = default)
        {
            var ret = await _invoiceService.CreateInvoicesAsync(command);
            return new CommandResponse<IEnumerable<InvoiceModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateInvoice command, CancellationToken cancellationToken = default)
        {
            var ret = await _invoiceService.UpdateInvoiceAsync(command);
            return new CommandResponse<InvoiceModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DownloadAsIIFInvoices command, CancellationToken cancellationToken = default)
        {
            var retInvoices = await _invoiceService.GetInvoicesAsync(_mapper.Map<GetInvoices>(command));

            var format = "iif";

            var retIifData = await _quickBooksService.FormatAsync(new FormatQuickbooks()
            {
                Invoices = retInvoices,
                FormatType = format
            });

            var retArchive = await _quickBooksService.ArchiveInvoicesAsync(new ArchiveQuickbooksInvoices()
            {
                FormattedInvoices = retIifData,
                FormatType = format
            });

            return new CommandResponse<System.IO.MemoryStream>(retArchive);
        }
    }
}
