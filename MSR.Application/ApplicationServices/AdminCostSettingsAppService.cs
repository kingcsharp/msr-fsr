using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class AdminCostSettingsAppService :
        ICommandHandler<GetAdminCostSettings>,
        ICommandHandler<UpdateAdminCostSetting>
    {
        private readonly IAdminCostSettingsService _adminCostSettingsService;

        public AdminCostSettingsAppService(IAdminCostSettingsService adminCostSettingsService)
        {
            _adminCostSettingsService = adminCostSettingsService;
        }

        public async Task<ICommandResponse> HandleAsync(GetAdminCostSettings command, CancellationToken cancellationToken = default)
        {
            return new CommandResponse<AdminCostSettingsModel>(await _adminCostSettingsService.GetAdminCostSettings());
        }

        public async Task<ICommandResponse> HandleAsync(UpdateAdminCostSetting command, CancellationToken cancellationToken = default)
        {
            var ret = await _adminCostSettingsService.UpdateAdminCostSettingAsync(command);
            return new CommandResponse<AdminCostSettingsModel>(ret);
        }
    }
}
