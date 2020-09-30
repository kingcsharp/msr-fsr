using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Report
{
    public class ReportService : IReportService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, ILogger<ReportService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ICollection<ReportModel>> GetReports(GetReport command, CancellationToken cancellationToken = default)
        {
            var reports = await _unitOfWork.Reports.Query().Include(i => i.ReportCategories).ThenInclude(j => j.Report)
                                                           .Include(i => i.ReportCategories).ThenInclude(j => j.ReportCategory).ToListAsync();

            var domReports = new List<ReportModel>();

            foreach(var report in reports)
            {
                var domReport = _mapper.Map<ReportModel>(report);

                foreach(var category in report.ReportCategories.Select(i => i.ReportCategory))
                {
                    domReport.Categories.Add(_mapper.Map<ReportCategoryModel>(category));
                }

                domReports.Add(domReport);
            }

            return domReports;
        }

        public async Task<ReportDashboardModel> GetDashboard(GetDashboard command, CancellationToken cancellationToken = default)
        {
            var dashboard = _unitOfWork.ReportDashboards.Query().Include(i => i.Reports).ThenInclude(j => j.Report).FirstOrDefault(i => i.Id == command.Id);

            var domDashboard = _mapper.Map<ReportDashboardModel>(dashboard);

            foreach(var report in dashboard.Reports.Select(i => i.Report))
            {
                domDashboard.Reports.Add(_mapper.Map<ReportModel>(report));
            }

            return domDashboard;
        }
    }
}
