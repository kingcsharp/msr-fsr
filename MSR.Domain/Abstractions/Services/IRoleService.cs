using MSR.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IRoleService
    {
        Task<int> CreateMenuRoleMapAsync(CreateMenuRoleMap command);
        Task<bool> UpdateMenuRoleMapAsync(UpdateMenuRoleMap command);
        Task<bool> RemoveMenuRoleMap(int id);
    }
}
