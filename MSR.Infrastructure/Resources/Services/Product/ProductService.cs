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
            var curCustomer = await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == command.CustomerId);

            if (curCustomer is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Customer)} not found with ID: {command.CustomerId}", DomainError.NotFound);
            }

            var curProcedure = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, i => i.Id == command.ProcedureId);

            if (curProcedure is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Procedure)} not found with ID: {command.ProcedureId}", DomainError.NotFound);
            }

            var curPart = await _unitOfWork.Parts.FirstOrDefaultAsync(false, i => i.Id == command.PartId);

            if (curPart is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Part)} not found with ID: {command.PartId}", DomainError.NotFound);
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProductApproval))
            {
                var product = _mapper.Map<Product>(command);

                if (product.ProductSteps != null)
                {
                    foreach (ProductStep ps in product.ProductSteps)
                    {
                        ps.Product = product;
                        // The ID copied from the command is the procedure step ID.  We're
                        // creating a new copy of that step, so blank the ID.
                        ps.Id = 0;
                    }
                }

                // Save the new Product
                await _unitOfWork.Products.AddAndSaveChangesAsync(product);
                await _unitOfWork.LogApprovalTransaction(product, product.Id, "Approved", command.Comment);

                var ret = _mapper.Map<ProductModel>(product);
                foreach (var ps in ret.ProductSteps)
                {
                    ps.Product = null;
                }
                return ret;
            }

            var approval = _mapper.Map<ProductApproval>(command);
            approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
            approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
            approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
            _unitOfWork.ProductApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();

            var steps = _mapper.Map<ICollection<ProductStepApproval>>(command.ProductSteps);

            foreach (ProductStepApproval step in steps)
            {
                step.ProductApprovalId = approval.Id;
                step.ProductStepId = null;
                _unitOfWork.ProductStepApprovals.Add(step);
            }
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductModel>(approval);
        }

        public async Task<ICollection<ProductModel>> GetProductAsync(GetProduct command)
        {
            IQueryable<Product> productQuery = _unitOfWork.Products
                        .Query()
                        .Include(x => x.Customer)
                        .Include(x => x.Part)
                        .Include(x => x.Procedure)
                        .Include(x => x.ProductSteps);

            if (command.Id.HasValue)
            {
                productQuery = productQuery.Where(x => x.Id == command.Id.Value);
            }

            if (command.CustomerId.HasValue)
            {
                productQuery = productQuery.Where(x => x.CustomerId == command.CustomerId.Value);
            }

            var products = await productQuery
                        .Select(x => _mapper.Map<ProductModel>(x))
                        .ToListAsync();

            // removing child product from the Model to avoid loop
            products.ForEach(p => p.ProductSteps?.ToList().ForEach(ps => ps.Product = null));

            // if ID is specified and ProductStep missing, we fall back to ProcedureStep
            if (command.Id.HasValue)
            {
                foreach (var p in products)
                {
                    if (!p.ProductSteps.Any())
                    {
                        var procedure = await _unitOfWork.Procedures.Query().Include(p => p.ProcedureSteps).FirstOrDefaultAsync(x => x.Id == p.ProcedureId);

                        if (procedure != null)
                        {
                            p.ProductSteps = _mapper.Map<ICollection<ProductStepModel>>(procedure.ProcedureSteps);

                            foreach (var ps in p.ProductSteps)
                            {
                                ps.ProductId = p.Id;
                            }
                        }
                    }
                    p.ProductSteps = p.ProductSteps.OrderBy(x => x.PrintOrder).ToList();
                }
            }

            return products;
        }


        public async Task<ProductModel> UpdateProductAsync(UpdateProduct command)
        {
            // Retreive product to update
            var product = await _unitOfWork.Products
                                .Query()
                                .Include(x => x.ProductSteps)
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (product is null)
            {
                throw new DomainException($"{nameof(Product)} not found with ID: {command.Id}");
            }

            if (command.QuoteId.HasValue)
            {
                // TODO: this isn't used in this function.
                var quote = await _unitOfWork.Quotes
                                    .Query()
                                    .FirstOrDefaultAsync(i => i.Id == command.QuoteId);

                if (quote is null)
                {
                    throw new DomainException($"{nameof(Quote)} not found with ID: {command.QuoteId}");
                }
            }

            var customer = await _unitOfWork.Customers
                                .Query()
                                .FirstOrDefaultAsync(i => i.Id == command.CustomerId);

            if (customer is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Customer)} not found with ID: {command.CustomerId}");
            }

            var procedure = await _unitOfWork.Procedures
                                .Query()
                                .FirstOrDefaultAsync(i => i.Id == command.ProcedureId);

            if (procedure is null)
            {
                throw new DomainException($"{nameof(Product)} not found with ID: {command.ProcedureId}");
            }

            var part = await _unitOfWork.Parts
                                .Query()
                                .FirstOrDefaultAsync(i => i.Id == command.PartId);

            if (part is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Part)} not found with ID: {command.PartId}");
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
                product.ProductSteps = _mapper.Map<ICollection<EntityFramework.Entities.ProductStep>>(command.ProductSteps);

                // Save product changes
                await _unitOfWork.Products.UpdateAndSaveChangesAsync(product);
                await _unitOfWork.LogApprovalTransaction(product, product.Id, "Approved", command.Comment);

                var model = _mapper.Map<ProductModel>(product);
                RemoveReferences(model);
                return model;
            }

            var approval = _mapper.Map<ProductApproval>(product);

            // Ignore changes made thus far to product, since we can't approve them.
            _unitOfWork.Products.Detach(product);

            var steps = _mapper.Map<ICollection<ProductStepApproval>>(command.ProductSteps);

            approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
            approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
            approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
            _unitOfWork.ProductApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();

            foreach (ProductStepApproval step in steps)
            {
                step.ProductApprovalId = approval.Id;
                _unitOfWork.ProductStepApprovals.Add(step);
            }
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductModel>(approval);
        }

        private void RemoveReferences(ProductModel product)
        {
            product.Quote = null;
            product.Procedure = null;
            product.Part = null;
            product.Customer = null;

            foreach (var ps in product.ProductSteps)
            {
                ps.Product = null;
                ps.ProductId = product.Id;
            }
        }
    }
}
