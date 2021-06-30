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
            var product = await _productService.CreateProductAsync(command);
            CommandResponse<ProductModel> response = new CommandResponse<ProductModel>(product);
            if (product.ApprovalStatus != null)
            {
                response.DisplayString = "Procedure added, pending approval: " + product.Id;
            }
            return response;
        }

        public async Task<ICommandResponse> HandleAsync(GetProduct command, CancellationToken cancellationToken = default)
        {
            var ret = await _productService.GetProductAsync(command);
            return new CommandResponse<IEnumerable<ProductModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateProduct command, CancellationToken cancellationToken = default)
        {
            var product = await _productService.UpdateProductAsync(command);
            CommandResponse<ProductModel> response = new CommandResponse<ProductModel>(product);
            if (product.ApprovalStatus != null)
            {
                response.DisplayString = "Procedure updated, pending approval: " + product.Id;
            }
            return response;
        }
    }
}
