using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(CreateProduct command);
        Task<ICollection<ProductModel>> GetProductsAsync();
        Task<ICollection<ProductModel>> GetProductAsync(GetProduct command);
        Task<ProductModel> UpdateProductAsync(UpdateProduct command);
    }
}
