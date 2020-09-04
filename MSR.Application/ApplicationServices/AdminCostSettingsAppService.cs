using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class AdminCostSettingsAppService :
        ICommandHandler<GetAdminCostSettingsCommand>
    {
        private readonly IAdminCostSettingsService _service;

        public AdminCostSettingsAppService(IAdminCostSettingsService service)
        {
            _service = service;
        }

        public Task<ICommandResponse> HandleAsync(GetAdminCostSettingsCommand command, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
