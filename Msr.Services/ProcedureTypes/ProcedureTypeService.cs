using EntityFrameworkExtras.EF6;
using Msr.Models.Procedures;
using Msr.Repositories;
using Msr.Services.Orders.Procedures;
using Msr.Services.ProcedureTypes.Procedures;
using Msr.Services.ProcedureTypes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Procedure;

namespace Msr.Services.ProcedureTypes
{
    public class ProcedureTypesService
    {
        private readonly MsrDbContext _dbContext;

        public ProcedureTypesService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ProcedureTypesView> GetProceduresTypes()
        {
            return _dbContext.ProcedureTypes;
        }
        public VerbType GetVerbTypeById(string Id)
        {
            return _dbContext.VerbTypes.Where(x=>x.ID == Id).SingleOrDefault();
        }
        public IQueryable<VerbTypes> GetVerbTypes(string Id)
        {
            
            var NTLogin = new SqlParameter("@strNTLogin", Id);


            var result = _dbContext.Database.SqlQuery<VerbTypes>("EXEC A_SP_DD_TT_VERBS_TYPES @strNTLogin", NTLogin).AsQueryable();

            return result;
        }

        public bool Save(SaveProcedureTypesViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureTypesProcedure { Name = model.Name,VerbType = model.VerbType,NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Edit(SaveProcedureTypesViewModel model)
        {

            try
            {
                var saveProcedureProcedure = new SaveProcedureTypesProcedure { ObjId = model.ObjectId, Name = model.Name, VerbType = model.VerbType, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureProcedure);

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
