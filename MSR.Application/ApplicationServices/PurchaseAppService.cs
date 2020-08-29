using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class PurchaseAppService :
        ICommandHandler<GetPurchases>,
        ICommandHandler<CreatePurchase>,
        ICommandHandler<UpdatePurchase>
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseAppService(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<ICommandResponse> HandleAsync(GetPurchases command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseService.GetPurchasesAsync(command);
            return new CommandResponse<ICollection<PurchaseModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreatePurchase command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseService.CreatePurchaseAsync(command);
            return new CommandResponse<PurchaseModel>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdatePurchase command, CancellationToken cancellationToken = default)
        {
            var ret = await _purchaseService.UpdatePurchaseAsync(command);
            return new CommandResponse<PurchaseModel>(ret);
        }
    }
}
