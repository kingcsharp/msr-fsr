using MSR.Domain.Models.Query;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.Abstractions
{
    public interface IPartViewService
    {
        Task<FileContentView> ExportParts(PartExportQueryFilters filters);

    }
}
