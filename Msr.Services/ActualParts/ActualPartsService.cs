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

        public IQueryable<ActualPartViewHistoryView> GetActualPartViewHistoryQueryable()
        {
            return _dbContext.ActualPartViewHistoryViews;
        }
        public ActualPartsView GetActualPartById(string id)
        {
            return GetActualPartsQueryable().SingleOrDefault(x => x.ObjectId == id);
        }
        public List<SelectFile> GetActualParts(string status)
        {
            var result = GetActualPartsQueryable().Where(x => x.Status.StartsWith(status) & !(x.SysName == null || x.SysName.Trim() == string.Empty))
                .Select(x => new SelectFile { Id = x.ObjectId, Name = x.SysName }).ToList();

            return result;
        }

        public List<string> GetActualpartProductsInstalled(string id,string ntlogin)
        {
            var strID = new SqlParameter("@ID", id == null ? "0" : id);
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<string>("Exec Portal_ActualPartProductInstalled @ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }

        public List<RootCompanyTree> GetActualPartCompanies(string ntlogin)
        {
            var strID = new SqlParameter("@PERSON_ID", ntlogin);
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<RootCompanyTree>("Exec A_SP_COMPANIES_SHOW_PERSONS_ROOT_COMPANY_TREE @PERSON_ID, @strNTLogin", strID, NTLogin).ToList();

            return result;
        }
        public bool Edit(SaveActualPartsViewModel model)
        {
            try
            {
                var saveActualPartsProcedure = new SaveActualPartsProcedure
                {
                    ObjId = model.ObjectId,
                    PartId = model.PartId,
                    Qty = model.Qty.ToString(),
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
                    //Qty = model.Qty <= 0 ? 1 : model.Qty,
                    PartId = model.PartId,
                    Qty = model.Qty.ToString(),
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

        public bool Close(string id,string ntlogin)
        {
            try
            {
                var NTLogin = ntlogin;
                var closeViewHistoryTaskProcedure = new CloseViewHistoryTaskProcedure() { Id = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(closeViewHistoryTaskProcedure);

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
