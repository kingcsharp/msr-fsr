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

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var productList = new List<ProductModel>();
            var products = _unitOfWork.Products
                                .Query()
                                .Include(p => p.Procedure)
                                .Include( p => p.Customer)
                                .Include(p => p.Created)
                                .Include(p => p.LastUpdated);

            foreach (var product in await products.ToListAsync())
            {
                productList.Add(_mapper.Map<ProductModel>(product));
            }

            return productList.AsEnumerable();
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

        public async Task<IEnumerable<ProductModel>> GetProductAsync(int id)
        {
            var productList = new List<ProductModel>();

            var products = _unitOfWork.Products
                            .Query()
                            .Include(q => q.Procedure)
                            .Include(q => q.Customer)
                            .Include(q => q.Part)
                            .Where(q => q.Id == id)
                            .AsQueryable();

            foreach (var product in await products.ToListAsync())
            {
                productList.Add(_mapper.Map<ProductModel>(product));
            }

            return productList.AsEnumerable();
        }


        public async Task<ProductModel> UpdateProductAsync(UpdateProduct command)
        {
            // Retreive product to update
            var product = await _unitOfWork.Products
                                .Query()
                                //.Include(i => i.ProductStepItems)
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (product is null)
            {
                throw new DomainException($"{nameof(Product)} not found with ID: {command.Id}");
            }

            //product.ProductStepItems.Clear();

            // Update product details and items
            product.CustomerId = command.CustomerId ?? product.CustomerId;
            product.CycleTime = command.CycleTime ?? product.CycleTime;
            product.EquipmentCost = command.EquipmentCost ?? product.EquipmentCost;
            product.MaterialCost = command.MaterialCost ?? product.MaterialCost;
            product.Name = command.Name ?? product.Name;
            product.PartId = command.PartId ?? product.PartId;
            product.ProcedureId = command.ProcedureId ?? product.ProcedureId;
            product.Revision = command.Revision ?? product.Revision;
            product.SalesTax = command.SalesTax ?? product.SalesTax;
            //product.ProductStepItems = command.ProductStepItems.Select(x =>
            //        _unitOfWork.ProductStepItems.Query()
            //                                .Include(ii => ii.WorkOrder)
            //                                .Include(ii => ii.PurchaseOrder)
            //                                .First(ii => ii.Id == x.Id)
            //).ToList();

            // Save product changes
            await _unitOfWork.Products.UpdateAndSaveChangesAsync(product);

            var retProduct = _mapper.Map<ProductModel>(product);

            return retProduct;
        }
        
    }
}
