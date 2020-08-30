using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderView>> GetPurchaseOrderAsync(GetPurchaseOrder command);
    }
}
