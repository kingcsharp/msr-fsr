using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IHelpService
    {
        Task<HelpPage> CreateHelpPage(CreateHelpPage command);

        Task UpdateHelpPage(UpdateHelpPage command);

        Task DeleteHelpPage(DeleteHelpPage command);

        Task<HelpPage> CreateHelpPageRole(CreateHelpPageRole command);

        Task DeleteHelpPageRole(DeleteHelpPageRole command);

        Task<IEnumerable<HelpPage>> GetHelpPages(GetHelpPage command);
    }
}
