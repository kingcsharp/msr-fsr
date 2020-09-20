using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class SearchAppService :
        ICommandHandler<GetSearch>
    {
        private readonly ISearchService _searchService;

        public SearchAppService(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<ICommandResponse> HandleAsync(GetSearch command, CancellationToken cancellationToken = default)
        {
            var ret = await _searchService.Search(command, cancellationToken);
            return new CommandResponse<ICollection<SearchView>>(ret);
        }
    }
}
