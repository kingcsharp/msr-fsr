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
            return GetPreProQueryable().Where(x => x.ObjectId == id).SingleOrDefault();
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

            var result = _dbContext.Database.SqlQuery<TheoryFile>("EXEC A_SP_PROCEDURE_STEP_GET_REF_PROCEDURES @ID,@strNTLogin",partId,NTLogin).ToList();

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
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.ID, DocID = file, Type = "PICTURE", NTLogin = model.strNTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                foreach (var file in model.REFERENCE_OBJECT)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.ID, DocID = file, Type = null, NTLogin = model.strNTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                foreach (var file in model.ReferenceProcs)
                {
                    var saveFileProcedure = new SaveFileProcedure() { ObjID = model.ID, DocID = file, Type = null, NTLogin = model.strNTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
                foreach (var file in model.REFERENCE_THEORIES)
                {
                    var saveFileProcedure = new SaveTheoryProcedure() { ObjID = model.ID, NTLogin = model.strNTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }

                
                var savePartProcedure = new SavePreProProcedure()
                {
                    ID = model.ID,
                     STEP_TEXT=model.STEP_TEXT,
                    COMMENTS = model.COMMENTS,
                    START_ON_COUNTER = model.START_ON_COUNTER,
                    COUNTER_VALUE = model.COUNTER_VALUE,
                    COUNTER_UNIT = model.COUNTER_UNIT,
                    FROM_START_OR_STOP = model.FROM_START_OR_STOP,
                    REL_OR_ABS = model.REL_OR_ABS,
                    SYSTEM_TASK = model.SYSTEM_TASK != null ? string.Join(", ", model.SYSTEM_TASK) : "",
                    DESTINATION = model.DESTINATION,
                    SPECIFIC_LOCATION = model.SPECIFIC_LOCATION,
                 //  REFERENCE_VERB = model.Reference_Verb != null ? string.Join(", ", model.Reference_Verb) : "",
                    REFERENCE_OBJECT = model.REFERENCE_OBJECT != null ? string.Join(", ", model.REFERENCE_OBJECT) : "",
                    REFERENCE_THEORIES = model.REFERENCE_THEORIES != null ? string.Join(", ", model.REFERENCE_OBJECT) : "",
                    GOTO_STEP = model.GOTO_STEP,
                    GOTO_STEP_ID = model.GOTO_STEP_ID,
                    CYCLES = model.CYCLES,
                    CYCLE_ON_COUNTER = model.CYCLE_ON_COUNTER,
                    CYCLE_COUNT = model.CYCLE_COUNT,
                    CYCLE_UNIT = model.CYCLE_UNIT,
                    ReferenceProcs = model.ReferenceProcs != null ? string.Join(", ", model.ReferenceProcs) : "",
                    precedingSteps = model.precedingSteps,
                    DURATION = model.DURATION,
                    DURATION_TYPE = model.DURATION_TYPE,
                    NTLogin = model.strNTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);
                var preproselectprocedure = new PreProSelectProcedure()
                {
                    objID = savePartProcedure.PROC_OBJ_ID,
                    procStepID = savePartProcedure.newID,
                    strNTLogin = model.strNTLogin

                };
                _dbContext.Database.ExecuteStoredProcedure(preproselectprocedure);

                var deletePictureFileProcedure = new DeletePreProProcedure() { id = savePartProcedure.newID, NTLogin = model.strNTLogin };

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
                  

                    ID = model.ID,
                 
                   PROC_OBJ_ID = model.PROC_OBJ_ID,
                    STEP_TEXT=model.STEP_TEXT,
                    COMMENTS=model.COMMENTS,
                    START_ON_COUNTER=model.START_ON_COUNTER,
                    COUNTER_VALUE=model.COUNTER_VALUE,
                    COUNTER_UNIT=model.COUNTER_UNIT,
                    FROM_START_OR_STOP=model.FROM_START_OR_STOP,
                    REL_OR_ABS=model.REL_OR_ABS,
                    SYSTEM_TASK= model.SYSTEM_TASK != null ? string.Join(", ", model.SYSTEM_TASK) : "",
                    DESTINATION=model.DESTINATION,
                    SPECIFIC_LOCATION=model.SPECIFIC_LOCATION,
                  //  REFERENCE_VERB= model.Reference_Verb != null ? string.Join(", ", model.Reference_Verb) : "",
                    REFERENCE_OBJECT= model.REFERENCE_OBJECT != null ? string.Join(", ", model.REFERENCE_OBJECT) : "",
                    REFERENCE_THEORIES =model.REFERENCE_THEORIES != null ? string.Join(", ", model.REFERENCE_THEORIES) :"",
                    GOTO_STEP=model.GOTO_STEP,
                    GOTO_STEP_ID=model.GOTO_STEP_ID,
                    CYCLES=model.CYCLES,
                    CYCLE_ON_COUNTER=model.CYCLE_ON_COUNTER,
                    CYCLE_COUNT=model.CYCLE_COUNT,
                    CYCLE_UNIT=model.CYCLE_UNIT,
                    ReferenceProcs = model.ReferenceProcs != null ? string.Join(", ", model.ReferenceProcs) : "",
                    precedingSteps =model.precedingSteps,
                    DURATION=model.DURATION,
                    DURATION_TYPE=model.DURATION_TYPE,
                      NTLogin = model.strNTLogin

                  
                };

                _dbContext.Database.ExecuteStoredProcedure(savePartProcedure);

                var preproselectprocedure = new PreProSelectProcedure()
                {
                    objID = savePartProcedure.PROC_OBJ_ID,
                    procStepID = savePartProcedure.newID,
                    strNTLogin = model.strNTLogin

                };
                _dbContext.Database.ExecuteStoredProcedure(preproselectprocedure);
                var deletePictureFileProcedure = new DeletePreProProcedure() { id = savePartProcedure.newID, NTLogin = model.strNTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deletePictureFileProcedure);


                foreach (var file in model.PictureFiles)
                {
                 
                    var saveFileProcedure = new SaveProcedurePreProFileProcedure() { ObjID = savePartProcedure.newID, DocID =file, NTLogin = model.strNTLogin };

                    _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                }
               
                //foreach (var file in model.ReferenceFiles)
                //{
                //    var saveFileProcedure = new SaveFileProcedure() { ObjID = singlePart.Id, DocID = file, Type = null, NTLogin = model.NTLogin };

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
