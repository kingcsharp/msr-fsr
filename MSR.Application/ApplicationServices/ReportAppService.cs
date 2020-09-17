using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class ReportAppService
        : ICommandHandler<GetReport>
    {
        private readonly IReportService _reportService;

        public ReportAppService(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<ICommandResponse> HandleAsync(GetReport command, CancellationToken cancellationToken = default)
        {
            var ret = await _reportService.GetReports(command, cancellationToken);
            return new CommandResponse<ICollection<ReportModel>>(ret);
        }
    }
}
