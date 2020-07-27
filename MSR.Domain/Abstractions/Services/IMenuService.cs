using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItem>> GetMenuAsync(GetMenu command);
        Task<int> CreateMenuRoleMapAsync(CreateMenuRoleMap command);
        Task<bool> UpdateMenuRoleMapAsync(UpdateMenuRoleMap command);
        Task<bool> RemoveMenuRoleMap(RemoveMenuRoleMap command);
    }
}
