using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Resources.EntityFramework.Application;

namespace MSR.Infrastructure.Resources.Services.Invoices
{
    public class ProductService : IProductService

    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.Models.ProductModel>> GetProductsAsync()
        {
            var productList = new List<Domain.Models.ProductModel>();
            var products = _unitOfWork.Products
                                .Query()
                                .Include(p => p.Procedure)
                                .Include( p => p.Customer)
                                .Include(p => p.Created)
                                .Include(p => p.LastUpdated);

            foreach (var product in await products.ToListAsync())
            {
                productList.Add(_mapper.Map<Domain.Models.ProductModel>(product));
            }

            return productList.AsEnumerable();
        }
    }
}
