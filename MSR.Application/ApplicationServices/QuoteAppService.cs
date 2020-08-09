using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class QuoteAppService :
        ICommandHandler<CreateQuote>,
        ICommandHandler<DeleteQuote>
    {
        private readonly IQuoteService _quoteService;

        public QuoteAppService(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<ICommandResponse> HandleAsync(CreateQuote command, CancellationToken cancellationToken = default)
        {
            var ret = await _quoteService.CreateQuoteAsync(command);
            return new CommandResponse<QuoteModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeleteQuote command, CancellationToken cancellationToken = default)
        {
            var ret = await _quoteService.DeleteQuoteAsync(command);
            return new CommandResponse<QuoteModel>(ret);
        }
    }
}
