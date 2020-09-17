using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
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
    }
}
