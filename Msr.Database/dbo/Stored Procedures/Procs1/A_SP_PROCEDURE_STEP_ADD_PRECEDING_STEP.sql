



CREATE   PROCEDURE A_SP_PROCEDURE_STEP_ADD_PRECEDING_STEP
@stepID nvarchar(50),
@precID nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @newID as nvarchar(50)
exec sp_GetUniqueID3 @newID OUTPUT
declare @proc_ID as nvarchar(50)
SELECT @proc_ID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @stepID
INSERT INTO A_PROCEDURE_STEP_PRECEDING_STEPS (ID,MY_STEP,PREV_STEP,PROCEDURE_ID,DRCM,MODBY)
VALUES (@newID,@stepID,@precID,@proc_ID,getDate(),@strNTLogin)

exec A_SP_PROCEDURE_SET_PRINT_ORDER @proc_ID,NULL,NULL,@strNTLogin




