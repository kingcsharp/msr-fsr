using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.PurchaseOrder
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Domain.Models.PurchaseModel>> GetPurchasesAsync(GetPurchases command)
        {
            List<Purchase> purchases = null;
            if (command.Id.HasValue) {
                purchases = await _unitOfWork.Purchases.Query()
                    .Where(x => x.Id == command.Id.Value)
                    .Include(x => x.Status)
                    .Include(x => x.WorkOrders)
                    .Include(x => x.Location)
                    .Include(x => x.PurchaseOrder)
                    .Include(x => x.PurchaseOrderProduct)
                    .ThenInclude(s => s.Product)
                    .ToListAsync();
                if (purchases.Count == 0) {
                    throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
                }
            } else {
                purchases = await _unitOfWork.Purchases.Query()
                    .Include(x => x.Status)
                    .Include(x => x.Location)
                    .Include(x => x.PurchaseOrder)
                    .Include(x => x.PurchaseOrderProduct)
                    .ThenInclude(s => s.Product)
                    .ToListAsync();
            }
            var result = purchases.Select(x => _mapper.Map<Domain.Models.PurchaseModel>(x)).ToList();
            return result;
        }

        public async Task<Domain.Models.PurchaseModel> CreatePurchaseAsync(CreatePurchase command)
        {
            Domain.Models.PurchaseModel ret;

            if (CurrentUser.HasPrivilege(EnumMenuItem.Purchases, EnumPrivilege.CanCreate)) {
                var purchase = _mapper.Map<Purchase>(command);
                var created = _unitOfWork.Purchases.Add(purchase);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(purchase, purchase.Id);

                // load required navigation fields
                created.Context.Entry(purchase)
                    .Reference(x => x.Status).Load();
                created.Context.Entry(purchase)
                    .Reference(x => x.Location).Load();
                created.Context.Entry(purchase)
                    .Reference(x => x.PurchaseOrder).Load();
                created.Context.Entry(purchase)
                    .Reference(x => x.PurchaseOrderProduct).Load();
                created.Context.Entry(purchase.PurchaseOrderProduct)
                    .Reference(x => x.Product).Load();

                ret = _mapper.Map<Domain.Models.PurchaseModel>(purchase);
            }
            else
            {
                throw new DomainException($"Permission deined on {nameof(Purchase)} for user {CurrentUser.GetId()}");
            }

            return ret;
        }
    }
}
