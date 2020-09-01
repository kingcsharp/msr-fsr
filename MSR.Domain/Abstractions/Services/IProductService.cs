using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(CreateProduct command);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task<IEnumerable<ProductModel>> GetProductAsync(int id);
        Task<ProductModel> UpdateProductAsync(UpdateProduct command);
    }
}
