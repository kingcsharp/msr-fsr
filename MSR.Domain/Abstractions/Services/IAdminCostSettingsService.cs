using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IAdminCostSettingsService
    {
        Task<AdminCostSettingsModel> GetAdminCostSettings();
    }
}
