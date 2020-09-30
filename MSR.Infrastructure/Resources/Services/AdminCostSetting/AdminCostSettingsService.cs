using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.AdminCostSetting
{
    public class AdminCostSettingsService : IAdminCostSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminCostSettingsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AdminCostSettingsModel> GetAdminCostSettings()
        {
            var ret = await _unitOfWork.AdminCostSettings.Query().FirstOrDefaultAsync();
            return _mapper.Map<AdminCostSettingsModel>(ret);
        }

        public async Task<AdminCostSettingsModel> UpdateAdminCostSettingAsync(UpdateAdminCostSetting command)
        {
            // Retreive the single AdminCostSetting 
            var adminCostSetting = await _unitOfWork.AdminCostSettings
                                .Query()
                                .SingleOrDefaultAsync();

            if (adminCostSetting is null)
            {
                throw new DomainException($"{nameof(AdminCostSetting)} not found");
            }

            // Update AdminCostSetting values
            adminCostSetting.RMAnnualRate = command.RMAnnualRate ?? adminCostSetting.RMAnnualRate;
            adminCostSetting.LaborRateMinute = command.LaborRateMinute ?? adminCostSetting.LaborRateMinute;
            adminCostSetting.HourMinutes = command.HourMinutes ?? adminCostSetting.HourMinutes;
            adminCostSetting.YearsHours = command.YearsHours ?? adminCostSetting.YearsHours;

            // Save AdminCostSetting changes
            await _unitOfWork.AdminCostSettings.UpdateAndSaveChangesAsync(adminCostSetting);

            var retadminCostSetting = _mapper.Map<AdminCostSettingsModel>(adminCostSetting);

            return retadminCostSetting;
        }
    }
}
