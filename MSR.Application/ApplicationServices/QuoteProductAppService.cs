using AutoMapper;
using MSR.Domain.Abstractions.QuickBooks;
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
    public class QuoteProductAppService :
        ICommandHandler<GetQuotesProducts>
    {
        private readonly IQuoteService _quoteService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public QuoteProductAppService(IQuoteService quoteService, IProductService productService, IMapper mapper)
        {
            _quoteService = quoteService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> HandleAsync(GetQuotesProducts command, CancellationToken cancellationToken = default)
        {
            var retQuotes = await _quoteService.GetQuotesAsync();
            var retProducts = await _productService.GetProductsAsync();

            var ret = new List<QuotesProductsModel>();

            foreach (var product in retProducts)
            {
                var qpModel = _mapper.Map<QuotesProductsModel>(product);
                qpModel.IsProduct = true;
                ret.Add(qpModel);
            }

            foreach (var quote in retQuotes)
            {
                var qpModel = _mapper.Map<QuotesProductsModel>(quote);
                qpModel.IsProduct = false;
                ret.Add(qpModel);
            }

            return new CommandResponse<IEnumerable<QuotesProductsModel>>(ret);
        }

    }
}
