using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class QuoteAppService :
        ICommandHandler<GetQuote>,
        ICommandHandler<CreateQuote>,
        ICommandHandler<DeleteQuote>
    {
        private readonly IQuoteService _quoteService;
        private readonly IMapper _mapper;

        public QuoteAppService(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<ICommandResponse> HandleAsync(CreateQuote command, CancellationToken cancellationToken = default)
        {
            var ret = await _quoteService.CreateQuoteAsync(command);
            return new CommandResponse<QuoteModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetQuote command, CancellationToken cancellationToken = default)
        {
            var ret = await _quoteService.GetQuoteAsync(command.Id.Value);
            return new CommandResponse<IEnumerable<QuoteModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeleteQuote command, CancellationToken cancellationToken = default)
        {
            var ret = await _quoteService.DeleteQuoteAsync(command);
            return new CommandResponse<QuoteModel>(ret);
        }
    }
}
