using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.Comman;
using Msr.Models.Procedures;
using Msr.Models.ProcedureVerbs;
using Msr.Repositories;
using Msr.Services.ProcedureVerbs.Procedures;
using Msr.Services.ProcedureVerbs.ViewModels;

namespace Msr.Services.ProcedureVerbs
{
    public class ProcedureVerbsService
    {
        private readonly MsrDbContext _dbContext;

        public ProcedureVerbsService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<ProcedureVerbsView> GetProceduresVerbs()
        {
            return _dbContext.ProcedureVerbs;
        }
        public ProcedureVerbsView GetVerbTypeById(string id)
        {
            return GetProceduresVerbs().SingleOrDefault(x => x.ObjectId == id);
        }
        public IQueryable<VerbTypes> GetVerbTypes(string id)
        {
            var NTLogin = new SqlParameter("@strNTLogin", id);

            var result = _dbContext.Database.SqlQuery<VerbTypes>("EXEC A_SP_DD_TT_VERBS_TYPES @strNTLogin", NTLogin).AsQueryable();

            return result;
        }


        public IQueryable<ProcedureVerbsView> ProceduresVerbsList()
        {
            return _dbContext.ProcedureVerbs.Where(x => x.Status == "APPROVED");
        }

        public List<SelectFile> ProcVerbsList(string ntLogin)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"EXEC A_SP_TT_VERBS_SELECT ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL )',' ORDER BY NAME','{ntLogin}'").ToList();

            return result;
        }

        public bool Save(SaveProcedureVerbsViewModel model)
        {
            try
            {
                var saveProcedureVerbsProcedure = new SaveProcedureVerbsProcedure { Name = model.Name, VerbType = model.VerbType, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureVerbsProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Edit(SaveProcedureVerbsViewModel model)
        {

            try
            {
                var saveProcedureVerbsProcedure = new SaveProcedureVerbsProcedure { ObjId = model.ObjectId, Name = model.Name, VerbType = model.VerbType, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(saveProcedureVerbsProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Delete(string id,string ntlogin)
        {
            try
            {
                //need to be dynamic
                var NTLogin = ntlogin;
                var deleteProcedureVerbProcedure = new DeleteProcedureVerbProcedure() { ObjId = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteProcedureVerbProcedure);

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
