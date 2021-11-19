using AutoMapper;
using MSR.Application.Abstractions;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
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
        private readonly IMapper _mapper;

        public ProductViewService(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<byte[]> DownloadFile(string format)
        {
            var products = await _productService.GetProductsAsync();
            var productDownloadViews = new List<ProductDownloadView>();

            foreach(var product in products)
            {
                var productDownloadView = _mapper.Map<ProductDownloadView>(product);
                productDownloadViews.Add(productDownloadView);
            }
            var data = CSVHelper.GenerateCSV(productDownloadViews);

            return data;
        }
    }
}
