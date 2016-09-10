

CREATE   FUNCTION dbo.A_FN_TASK_CREATE_NAME (@ID varchar(50))
RETURNS nvarchar(3000)
as
BEGIN
declare @so nvarchar(3000),@procID varchar(50),@procStepID varchar(50),@objID varchar(50),@toID varchar(50),@fromID varchar(50)
SELECT @procID = PROCEDURE_ID,@procStepID = PROCEDURE_STEP_ID FROM A_TASKS WHERE ID = @ID
SELECT @so = isNull(NAME + '. ','No Procedure Name. ') FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @procID
SELECT @objID = OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @ID
--SELECT @so = @so + isNull('Part = ' + OBJ_DESC + '. ','') FROM A_OBJECTS WHERE ID = @objID
SELECT @toID = ACTUAL_TO_LOC,@fromID = ACTUAL_FROM_LOC FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @ID
SELECT @so = @so + isNull('Source Location = ' + NAME + '. ','') FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @fromID
SELECT @so = @so + isNull('Destination Location = ' + NAME + '. ','') FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @toID


return(@so) 
END


