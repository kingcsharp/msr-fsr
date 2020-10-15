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
            var purchaseOrders = _unitOfWork.PurchaseOrders.Query();
            if (command.Id.HasValue)
            {
                purchaseOrders = purchaseOrders.Where(i => i.Id == command.Id);
            }

            var purchaseOrderModelList = await purchaseOrders
                .Include(i => i.Status)
                .Include(i => i.PurchaseOrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(pt => pt.Part)
                .Include(i => i.PurchaseOrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(pt => pt.Procedure)
                .ToListAsync();

            var purchaseOrderList = new List<PurchaseOrderView>();
            foreach (var pom in purchaseOrderModelList)
            {
                var pov = _mapper.Map<PurchaseOrderView>(pom);
                pov.Products = pom.PurchaseOrderProducts
                    .Select(x => _mapper.Map<PurchaseOrderProductView>(x))
                    .ToList();
                purchaseOrderList.Add(pov);
            }

            var purchaseOrderIds = purchaseOrderList.Select(i => i.Id).ToList();
            var purchaseOrderCustomerIds = purchaseOrderList.Select(i => i.CustomerId).ToList();

            var customers = await _unitOfWork.Customers.Query()
                .Where(i => purchaseOrderCustomerIds.Contains(i.Id))
                .Select(i => new { i.Id, i.Name })
                .ToDictionaryAsync(i => i.Id, i => i.Name);

            var purchases = await _unitOfWork.Purchases.Query().Where(i => purchaseOrderIds.Contains(i.PurchaseOrderId))
                .Select(x => new { x.PurchaseOrderId, x.Id }).ToListAsync();

            foreach (var po in purchaseOrderList)
            {
                customers.TryGetValue(po.CustomerId, out var name);
                if (name != null)
                {
                    po.CustomerName = name;
                }
                po.IsDeletable = purchases.All(i => i.PurchaseOrderId != po.Id);
                po.InvoicedBalance = 0;
                po.Balance = 0;
                po.UninvoicedBalance = 0;
                po.UnusedAmount = 0;
            }

            return purchaseOrderList;
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
                if(command.ClosePurchaseOrder || command.CloseDate.HasValue)
                {
                    purchaseOrder.CloseDate = command.CloseDate;
                    purchaseOrder.StatusId = (int)PurchaseOrderStatusEnum.Closed;
                }
                _unitOfWork.PurchaseOrders.Update(purchaseOrder);

                var productIds = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == purchaseOrder.Id).Select(i => i.ProductId).ToListAsync();

                //Exists in DB but not in list: Remove
                var productsToRemove = productIds.Except(command.Products);
                //Does not Exist in DB: Add
                var productsToAdd = command.Products.Except(productIds);

                var removeProducts = await _unitOfWork.PurchaseOrderProducts.Query().Where(i => i.PurchaseOrderId == purchaseOrder.Id && productsToRemove.Contains(i.ProductId)).ToListAsync();
                foreach (var removeProduct in removeProducts)
                {
                    _unitOfWork.PurchaseOrderProducts.Delete(false, removeProduct);
                }

                foreach (var addProduct in productsToAdd)
                {
                    var map = new PurchaseOrderProduct()
                    {
                        PurchaseOrderId = purchaseOrder.Id,
                        ProductId = addProduct
                    };

                    await _unitOfWork.PurchaseOrderProducts.AddAsync(map);
                }

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
