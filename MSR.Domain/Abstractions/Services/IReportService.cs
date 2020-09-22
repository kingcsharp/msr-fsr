using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IReportService
    {
        Task<ICollection<ReportModel>> GetReports(GetReport command, CancellationToken cancellationToken = default);
        Task<ReportDashboardModel> GetDashboard(GetDashboard command, CancellationToken cancellationToken = default);
    }
}
