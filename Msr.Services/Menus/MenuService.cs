using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Msr.Models.Menus;
using Msr.Repositories;

namespace Msr.Services.Menus
{
    public class MenuService
    {
        private readonly MsrDbContext _dbContext;

        public MenuService()
        {
            _dbContext = new MsrDbContext();    
        }

        public List<MenuView> GetMenu(string loginId)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@strNTLogin", loginId, DbType.Int32, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("Portal_GetUserModulePermissions", p, commandType: CommandType.StoredProcedure))
                {
                    var menus = multi.Read<MenuView>().ToList();
                    
                    return menus;
                }
            }
        }

        public MenuView GetMenuByName(string name)
        {
            return _dbContext.MenuViews.SingleOrDefault(x => x.Name == name);
        }
    }
}
