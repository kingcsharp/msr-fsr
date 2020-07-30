using MSR.Domain.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using MSR.Domain.Models;
namespace MSR.Domain.Abstractions.Services
{
    public interface IRoleService
    {
        Task<ICollection<Role>> GetRolesMapAsync(GetRoles command);
    }
}
