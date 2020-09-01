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
    public class ProductAppService :
        ICommandHandler<CreateProduct>,
        ICommandHandler<GetProduct>,
        ICommandHandler<UpdateProduct>
    {
        private readonly IProductService _productService;

        public ProductAppService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ICommandResponse> HandleAsync(CreateProduct command, CancellationToken cancellationToken = default)
        {
            var ret = await _productService.CreateProductAsync(command);
            return new CommandResponse<ProductModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetProduct command, CancellationToken cancellationToken = default)
        {
            var ret = await _productService.GetProductAsync(command.Id);
            return new CommandResponse<IEnumerable<ProductModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateProduct command, CancellationToken cancellationToken = default)
        {
            var ret = await _productService.UpdateProductAsync(command);
            return new CommandResponse<ProductModel>(ret);
        }
    }
}
