using AutoMapper;
using MSR.Application.Abstractions;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.QueryFilters;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.ViewServices
{
    internal class ProductViewService : IProductViewService
    {
        private readonly IProductService _productService;
        private readonly IQuoteService _quoteService;   
        private readonly IMapper _mapper;

        public ProductViewService(IProductService productService, IMapper mapper, IQuoteService quoteService)
        {
            _productService = productService;
            _mapper = mapper;
            _quoteService = quoteService;
        }

        public async Task<byte[]> DownloadFile(string format, ProductDownloadFilter filters)
        {
            var products = await _productService.GetProductsAsync();
            var quoteIds = products.Select(i => i.QuoteId).ToList();

            var quotes = (await _quoteService.GetQuotesAsync()).Where(i => quoteIds.Contains(i.Id));

            var productDownloadViews = new List<ProductDownloadView>();
            var filterItems = new List<ProductDownloadViewFilter>();

            foreach(var product in products)
            {
                var quote = quotes.FirstOrDefault(i => i.Id == product.QuoteId);
                var filterItem = new ProductDownloadViewFilter()
                {
                    ProductId = product.Id,
                    Company = quote?.Customer?.Name,
                    CycleTime = product.CycleTime,
                    DivisionFab = product.DivisionFab,
                    EquipmentCost = product.EquipmentCost,
                    LastUpdatedBy = product.LastUpdated.FullName,
                    LastUpdateOn = product.LastUpdatedOn,
                    MaterialCost = product.MaterialCost,
                    PartKitNo = product.Part.PartNumber,
                    ProcedureName = product.Procedure.Name,
                    ProductName = product.Name,
                    Representative = quote?.Representative,
                    Revision = product.Revision,
                    SalesTax = product.SalesTax,
                    SegregationType = product.Part.SegregationType,
                    SubmittedBy = product.Created.FullName,
                    SubmittedByFullName = product.Created.FullName,
                    SubmittedDate = product.CreatedOn,
                    TotalPrice = product.TotalSalePrice
                };

                filterItems.Add(filterItem);
            }

            var filteredProductIds = filterItems.AsQueryable().CreateQuotesProductsQuery(filters, true).Select(i => i.ProductId).ToList();

            foreach(var product in products.Where(i => filteredProductIds.Contains(i.Id)))
            {
                var productDownloadView = _mapper.Map<ProductDownloadView>(product);
                productDownloadViews.Add(productDownloadView);
            }

            var data = CSVHelper.GenerateCSV(productDownloadViews);

            return data;
        }

        public async Task<ICollection<ProductModel>> GetProductsByWorkOrderAsync(int workOrderId)
        {
            return await _productService.GetProductsByWorkOrder(workOrderId);
        }
    }
}
