using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MSR.Infrastructure.Resources.Queries;

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
            var quotes = await _quoteService.GetQuotesAsync();
            var products = await _productService.GetProductsAsync();

            // Merged view of Quotes and Products
            var quotesProductsViewModels= new List<QuotesProductsView>();

            foreach (var product in products)
            {
                var qpModel = _mapper.Map<QuotesProductsView>(product);
                qpModel.IsProduct = true;
                qpModel.IsDeletable = false;
                qpModel.Representative = product.Quote?.Representative;
                qpModel.SubmittedDate = product.Quote == null ? product.CreatedOn : product.Quote.SubmittedDate;
                qpModel.SegregationType = product.Part.SegregationType;
                qpModel.LastUpdateBy = product.LastUpdated.FullName;
                qpModel.LastUpdateOn = product.LastUpdatedOn;
                quotesProductsViewModels.Add(qpModel);
            }

            var productList = new List<ProductModel>(products);

            foreach (var quote in quotes)
            {
                if (!productList.Any(p => p.QuoteId == quote.Id)) {
                    var qpModel = _mapper.Map<QuotesProductsView>(quote);
                    qpModel.IsProduct = false;
                    qpModel.IsDeletable = true;

                    if (quote.PartKitNo == null)
                    {
                        if (quote.QuoteJson != null)
                        {
                            var quoteJson = JsonConvert.DeserializeObject<dynamic>(quote.QuoteJson);
                            qpModel.PartKitNo = quoteJson?.quoteItems?[0]?.customerPartNo;

                        }
                        else if (quote.CustomerRequirementJson != null)
                        {
                            var customerRequirementJson = JsonConvert.DeserializeObject<dynamic>(quote.CustomerRequirementJson);
                            qpModel.PartKitNo = customerRequirementJson?.PartKitNo;
                        }
                    }
                    qpModel.LastUpdateBy = quote.SubmittedBy.FullName;
                    qpModel.LastUpdateOn = quote.SubmittedDate;
                    quotesProductsViewModels.Add(qpModel);
                }               
            }

            var totalRows = quotesProductsViewModels.AsQueryable().CreateQuotesProductsQuery(command, true).Count();
            quotesProductsViewModels = quotesProductsViewModels.AsQueryable().CreateQuotesProductsQuery(command).ToList();
            
            return new PagingCommandResponse<IEnumerable<QuotesProductsView>>(quotesProductsViewModels, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
        }

    }
}
