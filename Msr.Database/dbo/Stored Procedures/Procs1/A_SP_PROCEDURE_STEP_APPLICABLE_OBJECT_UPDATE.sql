

CREATE   PROCEDURE DBO.A_SP_PROCEDURE_STEP_APPLICABLE_OBJECT_UPDATE
@newID varchar(50) OUTPUT,
@msgs varchar(500) OUTPUT,
@stepID varchar(50),
@linkID varchar(50),
@QTY float,
@OBJ_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'IN A_SP_PROCEDURE_STEP_APPLICABLE_OBJECT_UPDATE'
if @OBJ_ID is NULL 
	begin
	print 'Deleting'
	DELETE FROM A_PROCEDURE_OBJECT_LINK WHERE ID = @linkID
	goto fin
	end
if left(@linkID,5) = 'NEW__'
	begin
	print 'Adding a new one'
	if @stepID is null goto fin
	declare @procID varchar(50)
	SELECT @procID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @stepID
	exec sp_GetUniqueID3 @newID OUTPUT
	INSERT INTO A_PROCEDURE_OBJECT_LINK (ID,PROCEDURE_ID,STEP_ID,RELATIONSHIP)
	VALUES(@newID,@procID,@stepID,'STEP_APPLICABLE_OBJECT')
	set @linkID = @newID
	end

UPDATE A_PROCEDURE_OBJECT_LINK SET 
APPROVED_OBJECT_ID = @OBJ_ID,
QTY = @QTY
WHERE ID = @linkID
set @newID = @linkID


fin:





