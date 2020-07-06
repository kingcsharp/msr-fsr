using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IHelpService
    {
        Task SendSupportRequest(CreateSupport command);

        Task CreateHelpPage(CreateHelpPage command);

        Task UpdateHelpPage(UpdateHelpPage command);

        Task DeleteHelpPage(DeleteHelpPage command);

        Task CreateHelpPageRole(CreateHelpPageRole command);

        Task DeleteHelpPageRole(DeleteHelpPageRole command);

        Task<IEnumerable<HelpPage>> GetHelpPages(GetHelpPage command);
    }
}
