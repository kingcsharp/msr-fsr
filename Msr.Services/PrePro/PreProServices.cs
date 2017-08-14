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

        public List<SelectFile> GetSelectedTheory(string id)
        {
            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_GetTheoryObjects  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;
        }


        public List<SelectFile> GetSelectedPicRefFile(string id)
        {
            var objID = new SqlParameter("@procStepID", id == null ? "0" : id);

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_PROCEDURE_STEP_GET_REFERENCE_FILES  @procStepID, @strNTLogin", objID, NTLogin).ToList();

            return result;
        }

        public List<TheoryFile> GetPreProTheoryExceptions(string id)
        {
            var partId = new SqlParameter("@ID", id == null ? "0" : id);
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<TheoryFile>("EXEC A_SP_PROCEDURE_STEP_GET_REF_PROCEDURES @ID,@strNTLogin", partId, NTLogin).ToList();

            return result;
        }


        public bool Delete(string id)
        {
            try
            {
                //need to be dynamic
                var NTLogin = "1618";
                var deletePreproProcedure = new DeletePreProItemProcedure() { objid = id, NTLogin = NTLogin };

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
                foreach (var file in model.PictureFiles)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = "PICTURE", NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.ReferenceObject)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                foreach (var file in model.ReferenceProcs)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                foreach (var file in model.ReferenceTheories)
                {
                    var saveFileProcedure = new SaveTheoryProcedure() { ObjID = model.Id, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }


                var savePartProcedure = new SavePreProProcedure()
                {
                    Id = model.Id,
                    StepText = model.StepText,
                    Comments = model.Comments,
                    StartOnCounter = model.StartOnCounter,
                    CounterValue = model.CounterValue,
                    CounterUnit = model.CounterUnit,
                    FromStartOrStop = model.FromStartOrStop,
                    RelOrAbs = model.RelOrAbs,
                    SystemTask = model.SystemTask,
                    Destination = model.Destination,
                    SpecificLocation = model.SpecificLocation,
                    ReferenceVerb = model.ReferenceVerb != null ? string.Join(", ", model.ReferenceVerb) : "",
                    ReferenceObject = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : "",
                    ReferenceTheories = model.ReferenceTheories != null ? string.Join(", ", model.ReferenceTheories) : "",
                    GotoStep = model.GotoStep,
                    GotoStepId = model.GotoStepId,
                    Cycles = model.Cycles,
                    CycleOnCounter = model.CycleOnCounter,
                    CycleCount = model.CycleCount,
                    CycleUnit = model.CycleUnit,
                    ReferenceProcs = model.ReferenceProcs != null ? string.Join(", ", model.ReferenceProcs) : "",
                    PrecedingSteps = model.PrecedingSteps,
                    Duration = model.Duration,
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var preproselectprocedure = new PreProSelectProcedure()
                {
                    objID = savePartProcedure.ProcObjId,
                    procStepID = savePartProcedure.NewId,
                    strNTLogin = model.NTLogin

                };

                _dbContext.Database.ExecuteStoredProcedure(preproselectprocedure);

                var deletePictureFileProcedure = new DeletePreProProcedure() { id = savePartProcedure.NewId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);
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

                var savePartProcedure = new SavePreProProcedure()
                {


                    Id = model.Id,
                    StepText = model.StepText,
                    Comments = model.Comments,
                    StartOnCounter = model.StartOnCounter,
                    CounterValue = model.CounterValue,
                    CounterUnit = model.CounterUnit,
                    FromStartOrStop = model.FromStartOrStop,
                    RelOrAbs = model.RelOrAbs,
                    SystemTask = model.SystemTask,
                    Destination = model.Destination,
                    SpecificLocation = model.SpecificLocation,
                    ReferenceVerb = model.ReferenceVerb != null ? string.Join(", ", model.ReferenceVerb) : "",
                    ReferenceObject = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : "",
                    ReferenceTheories = model.ReferenceTheories != null ? string.Join(", ", model.ReferenceTheories) : "",
                    GotoStep = model.GotoStep,
                    GotoStepId = model.GotoStepId,
                    Cycles = model.Cycles,
                    CycleOnCounter = model.CycleOnCounter,
                    CycleCount = model.CycleCount,
                    CycleUnit = model.CycleUnit,
                    ReferenceProcs = model.ReferenceProcs != null ? string.Join(", ", model.ReferenceProcs) : "",
                    PrecedingSteps = model.PrecedingSteps,
                    Duration = model.Duration,
                    DurationType = model.DurationType,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var preproselectprocedure = new PreProSelectProcedure()
                {
                    objID = savePartProcedure.ProcObjId,
                    procStepID = savePartProcedure.NewId,
                    strNTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(preproselectprocedure);

                var deletePictureFileProcedure = new DeletePreProProcedure() { id = savePartProcedure.NewId, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);


                foreach (var file in model.PictureFiles)
                {

                    var saveFileProcedure = new SaveProcedurePreProFileProcedure() { ObjID = savePartProcedure.NewId, DocID = file, NTLogin = model.NTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                //foreach (var file in model.ReferenceFiles)
                //{
                //    var saveFileProcedure = new SaveFileProcedure() { ObjID = savePartProcedure.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

                //    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                //}


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
