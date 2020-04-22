using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Menu
{
    public class MenuService : IMenuService
    {
        public Task<ICollection<MenuGroup>> GetMenuAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
