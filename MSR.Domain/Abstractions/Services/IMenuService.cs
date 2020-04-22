using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IMenuService
    {
        Task<ICollection<MenuGroup>> GetMenuAsync();
    }
}
