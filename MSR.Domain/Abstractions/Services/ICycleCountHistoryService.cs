using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ICycleCountHistoryService
    {
        Task<CycleCountHistoryModel> CreateCycleCountHistoryAsync(CreateCycleCountHistory command);
        Task<CycleCountHistoryModel> UpdateCycleCountHistoryAsync(UpdateCycleCountHistory command);
        Task<ICollection<CycleCountHistoryModel>> ImportCycleCountHistories(string csvData);
    }
}
