using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

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

        public async Task<ICollection<ProductModel>> GetProductsAsync()
        {
            var products = await _unitOfWork.Products
                .Query()
                .Include(x => x.Part)
                .Select(p => _mapper.Map<ProductModel>(p))
                .ToListAsync();

            return products;
        }

        public async Task<ProductModel> CreateProductAsync(CreateProduct command)
        {
            var product = _mapper.Map<Product>(command);

            // Save the new Product
            await _unitOfWork.Products.AddAndSaveChangesAsync(product);

            // Returning ProductModel from the inserted quote
            var retProduct = _mapper.Map<ProductModel>(product);

            return retProduct;
        }

        public async Task<ICollection<ProductModel>> GetProductAsync(int? id)
        {
            if (!id.HasValue)
            {
                return await GetProductsAsync();
            }

            var product = await _unitOfWork.Products
                        .Query()
                        .Include(x => x.Customer)
                        .Include(x => x.Part)
                        .Include(x=>x.Procedure)
                        .Where(x => x.Id == id)
                        .Select(x => _mapper.Map<ProductModel>(x))
                        .ToListAsync();

            return product;
        }


        public async Task<ProductModel> UpdateProductAsync(UpdateProduct command)
        {
            // Retreive product to update
            var product = await _unitOfWork.Products
                                .Query()
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (product is null)
            {
                throw new DomainException($"{nameof(Product)} not found with ID: {command.Id}");
            }

            // Update product details and items
            product.CustomerId = command.CustomerId ?? product.CustomerId;
            product.CycleTime = command.CycleTime ?? product.CycleTime;
            product.LaborCost = command.LaborCost ?? product.LaborCost;
            product.EquipmentCost = command.EquipmentCost ?? product.EquipmentCost;
            product.MaterialCost = command.MaterialCost ?? product.MaterialCost;
            product.Name = command.Name ?? product.Name;
            product.PartId = command.PartId ?? product.PartId;
            product.ProcedureId = command.ProcedureId ?? product.ProcedureId;
            product.Revision = command.Revision ?? product.Revision;
            product.SalesTax = command.SalesTax ?? product.SalesTax;
            product.QuoteId = command.QuoteId ?? product.QuoteId;

            // Save product changes
            await _unitOfWork.Products.UpdateAndSaveChangesAsync(product);

            var retProduct = _mapper.Map<ProductModel>(product);

            return retProduct;
        }

    }
}
