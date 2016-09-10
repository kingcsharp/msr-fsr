



CREATE     PROCEDURE DBO.A_SP_TASK_UPDATE_ACTION_OBJECTS 
@newID varchar(50) OUTPUT,
@msg varchar(2000) OUTPUT,
@ID varchar(50),
@taskID varchar(50),
@actObjectID varchar(2000),
@qty float,
@relationship varchar(50),
@strNTLogin varchar(50)
AS
if @ID is not null
	DELETE FROM A_TASK_ACTION_OBJECT_LINK WHERE ID = @ID
else
	set @ID = newID()
declare @objType varchar(50)
SELECT @objType = OBJ_TABLE FROM A_OBJECTS WHERE ID = @actObjectID
if @qty  is null
	begin
	SELECT @objType = OBJ_TABLE FROM A_OBJECTS WHERE ID = @actObjectID
	if @objType = 'A_ACTUAL_PARTS_HISTORY'
		SELECT @qty = QTY FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @actObjectID
	else
		set @qty = 1
	end
	
if @actObjectID is not null and @actObjectID <> ''
	begin
	INSERT INTO A_TASK_ACTION_OBJECT_LINK (ID,TASK_ID,OBJECT_ID,DRCM,MODBY,RELATIONSHIP,QTY)
		VALUES(@ID,@taskID,@actObjectID,getDate(),@strNTLogin,@relationship,@qty)
	end
print 'here'
if @objType = 'A_ACTUAL_PARTS_HISTORY'
	begin
	print 'This is working'
	exec A_SP_ACTUAL_PART_MANUALLY_ADD_TO_TASK 
		@actObjectID,@taskID,@strNTLogin


	end 
						



