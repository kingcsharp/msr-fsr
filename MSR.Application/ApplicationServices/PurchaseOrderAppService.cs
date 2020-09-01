using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class PurchaseOrderAppService :
        ICommandHandler<GetPurchaseOrder>,
        ICommandHandler<CreatePurchaseOrder>,
        ICommandHandler<UpdatePurchaseOrder>,
        ICommandHandler<DeletePurchaseOrder>
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderAppService(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task<ICommandResponse> HandleAsync(GetPurchaseOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseOrderService.GetPurchaseOrderAsync(command);
            return new CommandResponse<IEnumerable<PurchaseOrderView>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreatePurchaseOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseOrderService.CreatePurchaseOrderAsync(command);
            return new CommandResponse<PurchaseOrderView>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdatePurchaseOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseOrderService.UpdatePurchaseOrderAsync(command);
            return new CommandResponse<PurchaseOrderView>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeletePurchaseOrder command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseOrderService.DeletePurchaseOrderAsync(command);
            return new CommandResponse<PurchaseOrderView>(ret);
        }
    }
}
