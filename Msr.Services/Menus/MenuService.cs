using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Msr.Services.Menus
{
    public class MenuService
    {
        public List<string> GetMenu(string loginId)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@strNTLogin", loginId, DbType.Int32, ParameterDirection.Input);

                using (var multi = conn.QueryMultiple("Portal_GetUserModulePermissions", p, commandType: CommandType.StoredProcedure))
                {
                    var menus = multi.Read<string>().ToList();
                    return menus;
                }
            }
        }
    }
}
