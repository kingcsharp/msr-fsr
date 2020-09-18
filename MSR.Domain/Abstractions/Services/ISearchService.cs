using MSR.Domain.Commands;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface ISearchService
    {
        Task<ICollection<SearchView>> Search(GetSearch command, CancellationToken cancellationToken = default);
    }
}
