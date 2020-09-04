using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.PurchaseOrder
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
            throw new System.NotImplementedException();
        }
    }
}
