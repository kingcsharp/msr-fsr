using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private ICustomerService _customerService;
        private IMapper _mapper;
        private readonly IMessageHubClient _messageHub;
        private ILogger _logger;

        public ProductService(IUnitOfWork unitOfWork, ICustomerService customerService, IMapper mapper, IMessageHubClient messageHub, ILogger<QuoteService> logger)
        {
            _unitOfWork = unitOfWork;
            _customerService = customerService;
            _mapper = mapper;
            _messageHub = messageHub;
            _logger = logger;
        }

        public async Task<ICollection<ProductModel>> GetProductsAsync()
        {
            var products = await _unitOfWork.Products
                .Query()
                .Include(x => x.Part)
                .Include(x => x.Procedure)
                .Include(x => x.Customer)
                .ToListAsync();

            return products.Select(p => _mapper.Map<ProductModel>(p)).ToList();
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
                product.Revision = 1;
                
                if (product.ProductSteps != null)
                {
                    foreach (ProductStep productStep in product.ProductSteps)
                    {
                        productStep.Product = product;
                        // The ID copied from the command is the procedure step ID.  We're
                        // creating a new copy of that step, so blank the ID.
                        productStep.Id = 0;
                    }
                }

                // Save the new Product
                await _unitOfWork.Products.AddAndSaveChangesAsync(product);
                await _unitOfWork.LogApprovalTransaction(product, product.Id, "Approved", command.Comment);

                var productModel = _mapper.Map<ProductModel>(product);
                if (productModel.ProductSteps != null)
                {
                    foreach (var productStep in productModel.ProductSteps)
                    {
                        productStep.Product = null;
                    }
                }
                return productModel;
            }

            var approval = _mapper.Map<ProductApproval>(command);
            approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
            approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
            approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
            _unitOfWork.ProductApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();

            var productStepApprovals =
                _mapper.Map<ICollection<ProductStepApproval>>(
                    command.ProductSteps);

            foreach (ProductStepApproval productStepApproval
                     in productStepApprovals)
            {
                productStepApproval.ProductApprovalId = approval.Id;
                productStepApproval.ProductStepId = null;
                _unitOfWork.ProductStepApprovals.Add(productStepApproval);
            }
            await _unitOfWork.SaveChangesAsync();

            _messageHub.SendApprovalNotification(EnumApprovalTables.ProductApproval);
            return _mapper.Map<ProductModel>(approval);
        }

        public async Task<ICollection<ProductModel>> GetProductAsync(GetProduct command)
        {
            IQueryable<Product> productQuery = _unitOfWork.Products.Query();

            if (command.Id.HasValue)
            {
                productQuery = productQuery.Where(x => x.Id == command.Id.Value);
            }

            if (command.CustomerId.HasValue)
            {
                productQuery = productQuery.Where(x => x.CustomerId == command.CustomerId.Value);
            }

            var productEntities = await productQuery.ToListAsync();
            var productIds = productEntities.Select(s => s.Id).ToList();
            var partIds = productEntities.Select(s => s.PartId).ToList();
            var procedureIds = productEntities.Select(s => s.ProcedureId).ToList();
            var customerIds = productEntities.Select(s => s.CustomerId).ToList();
            _ = await _unitOfWork.Parts.Query().Where(s => partIds.Contains(s.Id)).ToListAsync();
            _ = await _unitOfWork.Procedures.Query().Where(s => procedureIds.Contains(s.Id)).ToListAsync();
            _ = await _unitOfWork.ProductSteps.Query().Where(s => productIds.Contains(s.ProductId)).ToListAsync();
            _ = await _unitOfWork.Customers.Query().Where(s => customerIds.Contains(s.Id)).ToListAsync();


            var productModels = _mapper.Map<ICollection<ProductModel>>(productEntities);

            // removing child product from the Model to avoid loop
            productModels.ToList().ForEach(p => p.ProductSteps?.ToList().ForEach(ps => ps.Product = null));

            // if ID is specified and ProductStep missing, we fall back to ProcedureStep
            if (command.Id.HasValue)
            {
                foreach (var product in productModels)
                {
                    if (product.ProductSteps != null && !product.ProductSteps.Any())
                    {
                        var procedure =
                            await _unitOfWork.Procedures
                                .Query()
                                .Include(product => product.ProcedureSteps)
                                .FirstOrDefaultAsync(x =>
                                    x.Id == product.ProcedureId);

                        if (procedure != null)
                        {
                            product.ProductSteps =
                                _mapper.Map<ICollection<ProductStepModel>>(procedure.ProcedureSteps);

                            foreach (var ps in product.ProductSteps)
                            {
                                ps.ProductId = product.Id;
                            }
                        }
                    }

                    if(product.ProductSteps != null) 
                    {
                        product.ProductSteps = product.ProductSteps
                       .OrderBy(x => x.PrintOrder).ToList();
                    }
                    
                }
            }

            return productModels;
        }

        public async Task<ICollection<PurchaseOrderProductModel>> GetPurchaseOrderProductAsync(GetPurchaseOrderProduct command)
        {
            IQueryable<Product> productQuery = _unitOfWork.Products.Query()
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

            var purchaseOrderProductModels = await productQuery.Select(p => _mapper.Map<PurchaseOrderProductModel>(p)).ToListAsync();

            return purchaseOrderProductModels;
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

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProductApproval))
            {
                int revision = product.Revision;
                _ = _mapper.Map(command, product);
                product.Revision = revision + 1;

                product.ProductSteps = _mapper.Map<ICollection<EntityFramework.Entities.ProductStep>>(command.ProductSteps);

                // Save product changes
                await _unitOfWork.Products.UpdateAndSaveChangesAsync(product);
                await _unitOfWork.LogApprovalTransaction(product, product.Id, "Approved", command.Comment);

                var productModel = _mapper.Map<ProductModel>(product);
                RemoveReferences(productModel);
                return productModel;
            }

            var approval = _mapper.Map<ProductApproval>(product);
            _ = _mapper.Map(command, approval);

            var steps = _mapper.Map<ICollection<ProductStepApproval>>(command.ProductSteps);

            approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
            approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
            approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
            _unitOfWork.ProductApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();

            foreach (ProductStepApproval step in steps)
            {
                step.ProductApprovalId = approval.Id;
                await _unitOfWork.ProductStepApprovals.AddAsync(step);
            }
            await _unitOfWork.SaveChangesAsync();

            _messageHub.SendApprovalNotification(EnumApprovalTables.ProductApproval);
            return _mapper.Map<ProductModel>(approval);
        }

        private void RemoveReferences(ProductModel product)
        {
            product.Quote = null;
            product.Procedure = null;
            product.Part = null;
            product.Customer = null;

            if (product.ProductSteps != null) {
                foreach (var ps in product.ProductSteps)
                {
                    ps.Product = null;
                    ps.ProductId = product.Id;
                }
            }

            if (product.WorkOrderParts != null)
            {
                foreach (var wop in product.WorkOrderParts)
                {
                    wop.WorkOrder = null;
                    wop.Parent = null;
                    wop.Children = null;
                }
            }

        }
        private void RemoveCycle(ProductModel product)
        {
            if(product.WorkOrders == null)
            {
                return;
            }

            foreach(var model in product.WorkOrders)
            {
                if (model.Purchase != null)
                {
                    model.Purchase.WorkOrders = null;
                }
                if (model.Product != null)
                {
                    model.Product.WorkOrders = null;
                }
                if (model.WorkOrderParts != null)
                {
                    foreach (var wop in model.WorkOrderParts)
                    {
                        wop.WorkOrder = null;
                        wop.Parent = null;
                        wop.Children = null;
                    }
                }
                if (model.WorkOrderTasks != null)
                {
                    foreach (var wot in model.WorkOrderTasks)
                    {
                        wot.WorkOrder = null;
                    }
                }
                if (model.WorkOrderProducts != null)
                {
                    foreach (var wop in model.WorkOrderProducts)
                    {
                        wop.WorkOrders = null;
                    }
                }
            }
        }

        private async Task GetWorkOrderParts(WorkOrderPart parentPart, ProductModel model, int workOrderId)
        {
            var childParts = await _unitOfWork.WorkOrderParts.Query().Include(i => i.Part).Where(i => i.ParentId == parentPart.Id).ToListAsync();
            var partList = new List<WorkOrderPart>
            {
                parentPart
            };

            partList.AddRange(childParts);
            model.WorkOrderParts = new List<WorkOrderPartModel>();
            foreach(var part in partList)
            {
                var partModel = _mapper.Map<WorkOrderPartModel>(part);
                partModel.PartNumber = part.Part.PartNumber;
                partModel.Name = part.Part.Name;

                model.WorkOrderParts.Add(partModel);
            }
            model.Qty = parentPart.Qty.GetValueOrDefault(1);
            model.SerialNumber = parentPart.SerialNumber;
        }


        public async Task<ICollection<ProductModel>> GetProductsByWorkOrder(int workOrderId)
        {
            var workOrder = _unitOfWork.WorkOrders.FirstOrDefault(false, i => i.Id == workOrderId);
            if (workOrder == null) 
            {
                throw new DomainException($"WorkOrder with Id {workOrderId} not found", DomainError.NotFound);
            }
            var purchaseProductMaps = await _unitOfWork.PurchaseProductMaps.Query().Include(i => i.PurchaseOrderProduct).Where(i => i.PurchaseId == workOrder.PurchaseId).ToListAsync();
            if (purchaseProductMaps == null || !purchaseProductMaps.Any()) 
            {
                return new List<ProductModel>();
            }
            var productIds = purchaseProductMaps.Select(i => i.PurchaseOrderProduct.ProductId).ToList();
            var products = await _unitOfWork.Products.Query().Where(i => productIds.Contains(i.Id)).ToListAsync();
            var productModels = products.Select(i => _mapper.Map<ProductModel>(i)).ToList();
            var parentParts = await _unitOfWork.WorkOrderParts.Query().Include(i => i.Part).Where(i => i.WorkOrderId == workOrderId && i.ParentId == null).ToListAsync();
            var returnedProductModels = new List<ProductModel>();
            foreach (var purchaseProduct in purchaseProductMaps)
            {
                for(int i = 0; i < purchaseProduct.Qty; i++)
                {
                    var product = productModels.FirstOrDefault(i => i.Id == purchaseProduct.PurchaseOrderProduct.ProductId).Clone();
                    if(product == null)
                    {
                        continue;
                    }
                    var parentPart = parentParts.FirstOrDefault(i => i.PartId == product.PartId && !returnedProductModels.Any(j => j.WorkOrderParts.Any(h => h.Id == i.Id)));
                    await GetWorkOrderParts(parentPart,product, workOrderId);
                    RemoveReferences(product);
                    RemoveCycle(product);
                    returnedProductModels.Add(product);
                }
            }

            return returnedProductModels;
        }

        public async Task<IEnumerable<ProductModel>> ImportProducts(string csvData)
        {
            var records = CSVHelper.ParseRecords<QuoteImportItem>(csvData);
            var productModels = new List<ProductModel>();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.QuotesProducts, EnumPrivilege.CanApprove))
            {
                throw new DomainException("Permission denied for import", DomainError.BadRequest);
            }

            foreach (var record in records)
            {
                try
                {
                    var customer = await _customerService.GetCustomerByNameAsync(record.Company);
                    var createProductModel = _mapper.Map<CreateProduct>(record);
                    createProductModel.CustomerId = customer.Id;
                    var productModel = await CreateProductAsync(createProductModel);
                    productModels.Add(productModel);
                  
                }
                catch (Exception exception)
                {
                    //If we get an error on a single import dump it and keep going. 
                    _logger.LogError(exception, exception.Message);
                }
            }

            return productModels;
        }
    }
}
