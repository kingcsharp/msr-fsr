using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace MSR.Infrastructure.Resources.Services.PurchaseOrder
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderAsync(GetPurchaseOrder command)
        {
            var purchaseOrderQuery = _unitOfWork.PurchaseOrders.Query();
            if (command.Id.HasValue)
            {
                purchaseOrderQuery = purchaseOrderQuery.Where(i => i.Id == command.Id);
            }

            var purchaseOrderEntities = await purchaseOrderQuery
                .Include(i => i.Status)
                .Include(i => i.PurchaseOrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(pt => pt.Part)
                .Include(i => i.PurchaseOrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(pt => pt.Procedure)
                .ToListAsync();

            var purchaseOrderViews = new List<PurchaseOrderView>();

            foreach (var purchaseOrderEntity in purchaseOrderEntities)
            {
                var purchaseOrderView = _mapper.Map<PurchaseOrderView>(purchaseOrderEntity);
                purchaseOrderView.Products = purchaseOrderEntity.PurchaseOrderProducts
                    .Select(x => _mapper.Map<PurchaseOrderProductView>(x))
                    .ToList();
                purchaseOrderViews.Add(purchaseOrderView);
            }

            var purchaseOrderIds = purchaseOrderViews.Select(i => i.Id).ToList();
            var purchaseOrderCustomerIds = purchaseOrderViews.Select(i => i.CustomerId).ToList();

            var customersDictionary = await _unitOfWork.Customers.Query()
                .Where(i => purchaseOrderCustomerIds.Contains(i.Id))
                .Select(i => new { i.Id, i.Name })
                .ToDictionaryAsync(i => i.Id, i => i.Name);

            var purchaseEntities = await _unitOfWork.Purchases.Query()
                .Where(i => purchaseOrderIds.Contains(i.PurchaseOrderId))
                .Select(i => i)
                .ToListAsync();

            var purchaseIds = purchaseEntities.Select(i => i.Id).ToList();

            var workOrderEntities = await _unitOfWork.WorkOrders.Query()
                .Where(i => i.ActualEndDate != null && purchaseIds.Contains(i.PurchaseId))
                .Include(i => i.Purchase)
                .Select(x => x)
                .ToListAsync();

            var invoicedWorkOrderIds = await _unitOfWork.InvoiceItems.Query()
                .Where(i => purchaseOrderIds.Contains(i.PurchaseOrderId))
                .Select(i => i.WorkOrderId)
                .Distinct()
                .ToListAsync();

            foreach (var purchaseOrderView in purchaseOrderViews)
            {
                customersDictionary.TryGetValue(purchaseOrderView.CustomerId, out var name);
                if (name != null)
                {
                    purchaseOrderView.CustomerName = name;
                }
                purchaseOrderView.IsDeletable = purchaseEntities.All(i => i.PurchaseOrderId != purchaseOrderView.Id);
                purchaseOrderView.InvoicedBalance = 0;
                purchaseOrderView.Balance = 0;
                purchaseOrderView.UninvoicedBalance = 0;

                var purchaseOrderWorkOrderEntities = workOrderEntities
                    .Where(i => i.Purchase.PurchaseOrderId == purchaseOrderView.Id)
                    .Select(x => x)
                    .ToList();

                foreach (var purchaseOrderWorkOrderEntity in purchaseOrderWorkOrderEntities)
                {
                    purchaseOrderView.Balance += purchaseOrderWorkOrderEntity.Price;
                    if (invoicedWorkOrderIds.Contains(purchaseOrderWorkOrderEntity.Id))
                    {
                        purchaseOrderView.InvoicedBalance += purchaseOrderWorkOrderEntity.Price;
                    } else
                    {
                        purchaseOrderView.UninvoicedBalance += purchaseOrderWorkOrderEntity.Price;
                    }
                }

                purchaseOrderView.UnusedAmount = purchaseOrderView.TotalPurchaseLimit - purchaseOrderView.Balance;
            }

            return purchaseOrderViews;
        }

        public async Task<PurchaseOrderView> CreatePurchaseOrderAsync(CreatePurchaseOrder command)
        {
            //First, Check if the user has CanApprove or not.
            if (CurrentUser.CanApproveActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                //They can approve so just put it in the tables
                var purchaseOrder = _mapper.Map<EntityFramework.Entities.PurchaseOrder>(command);
                purchaseOrder.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Name == "Open");
                purchaseOrder.Revision = 1;

                await _unitOfWork.PurchaseOrders.AddAsync(purchaseOrder);
                await _unitOfWork.SaveChangesAsync();

                var products = await _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id)).ToListAsync();

                foreach (var product in products)
                {
                    var map = new PurchaseOrderProduct()
                    {
                        PurchaseOrder = purchaseOrder,
                        Product = product,
                        ProductRevision = product.Revision
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(map);
                }

                await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, "Approved", "Auto Approved");
                var retPO = _mapper.Map<PurchaseOrderView>(purchaseOrder);
                retPO.Products = products.Select(i => _mapper.Map<PurchaseOrderProductView>(i)).ToList();
                retPO.CustomerName = (await _unitOfWork.Customers.FirstOrDefaultAsync(false, i => i.Id == command.CustomerId)).Name;
                return retPO;
            }
            else
            {
                var purchaseOrderApproval = _mapper.Map<PurchaseOrderApproval>(command);
                purchaseOrderApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(purchaseOrderApproval);
                purchaseOrderApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(purchaseOrderApproval.Workflow?.Id ?? 0);
                purchaseOrderApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.PurchaseOrderApprovals.AddAsync(purchaseOrderApproval);
                await _unitOfWork.SaveChangesAsync();

                var products = _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id));

                foreach (var product in products)
                {
                    var approvalMap = new PurchaseOrderProductApproval()
                    {
                        PurchaseOrderApprovalId = purchaseOrderApproval.Id,
                        ProductId = product.Id
                    };

                    await _unitOfWork.PurchaseOrderProductApprovals.AddAsync(approvalMap);
                }

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<PurchaseOrderView>(purchaseOrderApproval);
            }
        }

        public async Task<PurchaseOrderView> UpdatePurchaseOrderAsync(UpdatePurchaseOrder command)
        {
            var purchaseOrder = await _unitOfWork.PurchaseOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (purchaseOrder is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} with ID: {command.Id} not found", DomainError.NotFound);
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                _mapper.Map(command, purchaseOrder);
                purchaseOrder.Revision = purchaseOrder.Revision == null ? 1 : purchaseOrder.Revision + 1;
                if(command.ClosePurchaseOrder && command.CloseDate.HasValue)
                {
                    purchaseOrder.CloseDate = command.CloseDate;
                    purchaseOrder.StatusId = (int)PurchaseOrderStatusEnum.Closed;
                    purchaseOrder.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)PurchaseOrderStatusEnum.Closed);
                }
                _unitOfWork.PurchaseOrders.Update(purchaseOrder);
                await _unitOfWork.SaveChangesAsync();

                var productIds = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == purchaseOrder.Id).Select(i => i.ProductId).ToListAsync();

                //Exists in DB but not in list: Remove
                var productsToRemove = productIds.Except(command.Products);
                //Does not Exist in DB: Add
                var productsToAdd = command.Products.Except(productIds);
                var usedProducts = await _unitOfWork.Purchases.Query().Where(i => i.PurchaseOrderId == command.Id).Select(i => i.PurchaseOrderProductId).ToListAsync();
                var removeProducts = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == purchaseOrder.Id && productsToRemove.Contains(i.ProductId) && !usedProducts.Contains(i.Id)).ToListAsync();
                
                foreach (var removeProduct in removeProducts)
                {
                    _unitOfWork.PurchaseOrderProducts.Delete(false, removeProduct);
                }

                await _unitOfWork.SaveChangesAsync();
                foreach (var addProduct in productsToAdd)
                {
                    var map = new PurchaseOrderProduct()
                    {
                        PurchaseOrderId = purchaseOrder.Id,
                        ProductId = addProduct
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(map);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, "Approved", "Auto Approved");
                var result = await GetPurchaseOrderAsync(new GetPurchaseOrder() { Id = command.Id });
                return result.FirstOrDefault();
            }
            else
            {
                var poApproval = _mapper.Map<PurchaseOrderApproval>(purchaseOrder);
                _mapper.Map(command, poApproval);
                poApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(poApproval);
                poApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(poApproval.Workflow?.Id ?? 0);
                poApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.PurchaseOrderApprovals.AddAsync(poApproval);
                await _unitOfWork.SaveChangesAsync();

                var products = _unitOfWork.Products.Query().Where(i => command.Products.Contains(i.Id));

                foreach (var product in products)
                {
                    var approvalMap = new PurchaseOrderProductApproval()
                    {
                        PurchaseOrderApprovalId = poApproval.Id,
                        ProductId = product.Id
                    };

                    await _unitOfWork.PurchaseOrderProductApprovals.AddAsync(approvalMap);
                }

                await _unitOfWork.SaveChangesAsync();

                var result = await GetPurchaseOrderAsync(new GetPurchaseOrder() { Id = command.Id });
                return result.FirstOrDefault();
            }
        }

        public async Task<PurchaseOrderView> DeletePurchaseOrderAsync(DeletePurchaseOrder command)
        {
            var inUse = _unitOfWork.Purchases.Query().Any(i => i.PurchaseOrderId == command.Id);

            if (inUse)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} in use.", DomainError.Conflict);
            }

            var purchaseOrder = await _unitOfWork.PurchaseOrders.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (purchaseOrder is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.PurchaseOrder)} with ID: {command.Id} not found", DomainError.NotFound);
            }

            var poProductsToDelete = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == command.Id).ToListAsync();

            foreach (var poProduct in poProductsToDelete)
            {
                _unitOfWork.PurchaseOrderProducts.Delete(false, poProduct);
            }

            _unitOfWork.PurchaseOrders.Delete(false, purchaseOrder);
            await _unitOfWork.LogApprovalTransaction(purchaseOrder, purchaseOrder.Id, "Approved", "Auto Approved");

            return _mapper.Map<PurchaseOrderView>(purchaseOrder);
        }

        public async Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderProductAsync(GetPurchaseOrder command)
        {
            List<PurchaseOrderView> poList;

            if (command.Id.HasValue)
            {
                poList = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<PurchaseOrderView>(po))
                                            .Where(po => po.Id == command.Id)
                                            .ToListAsync();
                if (!poList.Any())
                {
                    throw new DomainException($"PurchaseOrder ID {command.Id} not found", DomainError.NotFound);
                }
            }
            else
            {
                poList = await _unitOfWork.PurchaseOrders
                                            .Query()
                                            .Include(po => po.Customer)
                                            .Select(po => _mapper.Map<PurchaseOrderView>(po))
                                            .ToListAsync();
            }

            var result = poList.Select(x => _mapper.Map<PurchaseOrderView>(x)).OrderBy(x => x.Name).AsEnumerable();

            return result;
        }
    }
}
