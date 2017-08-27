using EntityFrameworkExtras.EF6;
using Msr.Models;
using Msr.Models.Comman;
using Msr.Repositories;
using Msr.Services.Parts.Procedures;
using Msr.Services.PrePro.Procedure;
using Msr.Services.PrePro.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.PrePro;

namespace Msr.Services.PrePro
{

    public class PreProServices
    {
        private readonly MsrDbContext _dbContext;

        public PreProServices()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<PrePropSearchView> GetPreProQueryable()
        {
            return _dbContext.PrePropSearchView;
        }
        public PrePropSearchView GetById(string id)
        {
            return GetPreProQueryable().SingleOrDefault(x => x.ObjectId == id);
        }

        public List<SelectFile> GetSelectedRefProcedures(string id,string ntlogin)
        {
            var objId = new SqlParameter("@ID", id ?? "0");

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefProcedures @ID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }


        public List<SelectFile> GetSelectedRefFiles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_ProcedureStepGetRefFiles @procStepID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }

        public List<SelectFile> GetSelectedRefTheories(string id)
        {
            if (id == null) return new List<SelectFile>();

            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT t.NAME AS Name,t.ID AS Id FROM A_PROCEDURE_STEP_THEORY_LINK l, A_V_THEORY_APPROVED_DATA t  where l.PROC_STEP_ID = " + id + " and t.ID = l.THEORY_ID").ToList();

            return result;
        }

        public bool Delete(string id,string ntlogin)
        {
            try
            {
                //need to be dynamic
                var NTLogin = ntlogin;
                var deletePreproProcedure = new DeleteProcedureStepProcedure() { Objid = id, NTLogin = NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePreproProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool Save(ProcedurePreProViewModel model)
        {
            try
            {
                var savePartProcedure = new SaveProcedureStepProcedure()
                {
                    Id = model.Id,
                    StepText = model.StepText,
                    ProcObjId = model.ProcObjId,
                    Comments = model.Comments,
                    StartOnCounter = model.StartOnCounter.ToString(),
                    CounterValue = null,
                    CounterUnit = null,
                    FromStartOrStop = null,
                    RelOrAbs = null,
                    SystemTask = model.SystemTask,
                    Destination = model.Destination,
                    SpecificLocation = null,
                    ReferenceVerb = model.ReferenceVerb,
                    ReferenceObject = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    ReferenceTheories = model.ReferenceTheories != null ? string.Join(", ", model.ReferenceTheories) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    GotoStep = null,
                    GotoStepId = null,
                    Cycles = null,
                    CycleOnCounter = null,
                    CycleCount = null,
                    CycleUnit = null,
                    ReferenceProcs = model.ReferenceProcedures != null ? string.Join(", ", model.ReferenceProcedures) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    PrecedingSteps = null,
                    Duration = model.Duration.ToString(),
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var savePrePopProcedure = new SavePrePopProcedure() { ObjId = model.ObjectId, ProcStepId = model.Id, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePrePopProcedure);

                var deletePictureFileProcedure = new DeleteProcedureStepFileLinkProcedure() { id = model.Id, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveProcedureStepFileLinkProcedure() { ObjId = model.Id, DocId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }


        public bool Create(ProcedurePreProViewModel model)
        {

            try
            {

                var savePartProcedure = new SaveProcedureStepProcedure()
                {
                    StepText = model.StepText,
                    ProcObjId = model.ProcObjId,
                    Comments = model.Comments,
                    StartOnCounter = model.StartOnCounter.ToString(),
                    CounterValue = null,
                    CounterUnit = null,
                    FromStartOrStop = null,
                    RelOrAbs = null,
                    SystemTask = model.SystemTask,
                    Destination = model.Destination,
                    SpecificLocation = null,
                    ReferenceVerb = model.ReferenceVerb,
                    ReferenceObject = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    ReferenceTheories = model.ReferenceTheories != null ? string.Join(", ", model.ReferenceTheories) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    GotoStep = null,
                    GotoStepId = null,
                    Cycles = null,
                    CycleOnCounter = null,
                    CycleCount = null,
                    CycleUnit = null,
                    ReferenceProcs = model.ReferenceProcedures != null ? string.Join(", ", model.ReferenceProcedures) : DBNull.Value.ToString(CultureInfo.InvariantCulture),
                    PrecedingSteps = null,
                    Duration = model.Duration.ToString(),
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var savePrePopProcedure = new SavePrePopProcedure() { ObjId = null, ProcStepId = savePartProcedure.NewId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(savePrePopProcedure);

                var deletePictureFileProcedure = new DeleteProcedureStepFileLinkProcedure() { id = savePartProcedure.NewId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);

                foreach (var file in model.ReferenceFiles)
                {
                    var saveFileProcedure = new SaveProcedureStepFileLinkProcedure() { ObjId = savePartProcedure.NewId, DocId = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public List<SelectFile> GetApprovedVerbsByCreatingCo(string CreatingCo)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SET QUOTED_IDENTIFIER OFF SELECT NAME AS Name, ID AS Id, CREATING_CO as CreatingCo FROM A_APPROVED_VERBS where CREATING_CO='" + CreatingCo + "'").ToList();

            return result;
        }
        public List<SelectFile> GetReferenceObjectsByCreatingCo(string CreatingCo)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT ID as Id, OBJ_TABLE as Name, CREATING_CO as CreatingCo FROM A_V_APPROVED_OBJECTS where CREATING_CO='" + CreatingCo + "'").ToList();

            return result;
        }
    }
}
