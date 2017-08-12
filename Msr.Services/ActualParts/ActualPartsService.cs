using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msr.Repositories;
using System.Threading.Tasks;
using Msr.Models.ActualParts;
using System.Data.SqlClient;
using Msr.Services.ActualParts.ViewModels;
using EntityFrameworkExtras.EF6;
using Msr.Services.ActualParts.Procedures;
using Msr.Models.Comman;
using Dapper;
using System.Data;
using System.Configuration;

namespace Msr.Services.ActualParts
{
    public class ActualPartsService
    {
        private readonly MsrDbContext _dbContext;

        public ActualPartsService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ActualPartsView> GetActualPartsQueryable()
        {
            return _dbContext.ActualPartsViews;
        }
        public ActualPartsView GetActualPartById(string id)
        {
            return GetActualPartsQueryable().Where(x => x.ObjectId == id).SingleOrDefault();
        }
        public List<SelectFile> GetActualParts(string status)
        {
            var result = GetActualPartsQueryable().Where(x => x.Status.StartsWith(status) & !(x.SysName == null || x.SysName.Trim() == string.Empty))
                .Select(x => new SelectFile { Id = x.ObjectId, Name = x.SysName }).ToList();

            return result;
        }

        public List<string> GetActualpartProductsInstalled(string id)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<string>("Exec Portal_ActualPartProductInstalled @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public List<RootCompanyTree> GetActualPartCompanies()
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@PERSON_ID", "1618", DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@strNTLogin", "1618", DbType.String, ParameterDirection.Input, size: 50);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    var result = conn.Query<RootCompanyTree>("A_SP_COMPANIES_SHOW_PERSONS_ROOT_COMPANY_TREE", p, commandType: CommandType.StoredProcedure);

                    return result.ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Edit(SaveActualPartsViewModel model)
        {
            try
            {
                var saveActualPartsProcedure = new SaveActualPartsProcedure
                {
                    ObjId = model.ObjectId,
                    PartId = model.PartId,
                    Qty = model.Qty <= 0 ? 1 : model.Qty,
                    Serial = model.Serial,
                    NickName = model.NickName,
                    LocationId = model.LocationId,
                    CurOwner = model.CurOwner,
                    ApStatus = model.APStatus,
                    Products = model.Products != null ? string.Join(", ", model.Products) : DBNull.Value.ToString(),
                    ParentId = model.ParentId,
                    SubPartAction = model.SubpartAction,
                    ResponsiblePerson = model.ResponsiblePerson,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveActualPartsProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Create(SaveActualPartsViewModel model)
        {
            try
            {
                var saveActualPartsProcedure = new SaveActualPartsProcedure
                {
                    PartId = model.PartId,
                    Qty = model.Qty <= 0 ? 1 : model.Qty,
                    Serial = model.Serial,
                    NickName = model.NickName,
                    LocationId = model.LocationId,
                    CurOwner = model.CurOwner,
                    ApStatus = model.APStatus,
                    Products = model.Products != null ? string.Join(", ", model.Products) : DBNull.Value.ToString(),
                    ParentId = model.ParentId,
                    SubPartAction = model.SubpartAction,
                    ResponsiblePerson = model.ResponsiblePerson,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveActualPartsProcedure);

                return true;

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
    }
}
