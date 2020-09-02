using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IPurchaseService
    {
        Task<ICollection<PurchaseModel>> GetPurchasesAsync(GetPurchases command);
        Task<PurchaseModel> CreatePurchaseAsync(CreatePurchase command);
    }
}
