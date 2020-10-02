using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;

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
                .Include(x => x.Procedure)
                .Select(p => _mapper.Map<ProductModel>(p))
                .ToListAsync();

            return products;
        }

        public async Task<ProductModel> CreateProductAsync(CreateProduct command)
        {
            var product = _mapper.Map<Product>(command);

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProductApproval))
            {

                // Save the new Product
                await _unitOfWork.Products.AddAndSaveChangesAsync(product);
                await _unitOfWork.LogApprovalTransaction(product, product.Id, "Approved", command.Comment);

                return _mapper.Map<ProductModel>(product);
            }

            var approval = _mapper.Map<ProductApproval>(command);
            approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
            approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
            approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
            _unitOfWork.ProductApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductModel>(approval);
        }

        public async Task<ICollection<ProductModel>> GetProductAsync(GetProduct command)
        {
            IQueryable<Product> productQuery = _unitOfWork.Products
                        .Query()
                        .Include(x => x.Customer)
                        .Include(x => x.Part)
                        .Include(x => x.Procedure);

            if (command.Id.HasValue)
            {
                productQuery = productQuery.Where(x => x.Id == command.Id.Value);
            }

            if (command.CustomerId.HasValue)
            {
                productQuery = productQuery.Where(x => x.CustomerId == command.CustomerId.Value);
            }

            var product = await productQuery
                        .Select(x => _mapper.Map<ProductModel>(x))
                        .ToListAsync();
            }

            return await GetProductsAsync();
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
            product.DivisionFab = command.DivisionFab ?? product.DivisionFab;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProductApproval))
            {
                // Save product changes
                await _unitOfWork.Products.UpdateAndSaveChangesAsync(product);
                await _unitOfWork.LogApprovalTransaction(product, product.Id, "Approved", command.Comment);
                return _mapper.Map<ProductModel>(product);
            }

            var approval = _mapper.Map<ProductApproval>(product);
            approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
            approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
            approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
            _unitOfWork.ProductApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductModel>(approval);
        }

    }
}
