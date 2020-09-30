using MSR.Domain.Commands;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderAsync(GetPurchaseOrder command);
        Task<PurchaseOrderView> CreatePurchaseOrderAsync(CreatePurchaseOrder command);
        Task<PurchaseOrderView> UpdatePurchaseOrderAsync(UpdatePurchaseOrder command);
        Task<PurchaseOrderView> DeletePurchaseOrderAsync(DeletePurchaseOrder command);
    }
}
