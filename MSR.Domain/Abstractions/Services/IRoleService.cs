using MSR.Domain.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using MSR.Domain.Models;
namespace MSR.Domain.Abstractions.Services
{
    public interface IRoleService
    {
        Task<int> CreateMenuRoleMapAsync(CreateMenuRoleMap command);
        Task<ICollection<Role>> GetRolesMapAsync(GetRoles command);
        Task<bool> UpdateMenuRoleMapAsync(UpdateMenuRoleMap command);
        Task<bool> RemoveMenuRoleMap(int id);

    }
}
