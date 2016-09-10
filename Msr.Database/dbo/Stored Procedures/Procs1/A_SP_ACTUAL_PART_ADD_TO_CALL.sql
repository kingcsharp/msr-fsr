




CREATE      PROCEDURE dbo.A_SP_ACTUAL_PART_ADD_TO_CALL
@objectID varchar(50),
@taskID varchar(50),
@reason varchar(50),
@strNTLogin varchar(50)
AS
print 'Making sure this part is in this call'
declare @callRootTaskID varchar(50),@purchHistID varchar(50)
exec A_SP_TASK_GET_CALL_ROOT_TASK_ID @callRootTaskID OUTPUT,@taskID
print 'The root task ID = ' + @callRootTaskID
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @callRootTaskID

if not (exists(SELECT * FROM A_ACTUAL_PARTS_CALL_DATA WHERE ACTUAL_PART_ID = @objectID AND ROOT_TASK = @callRootTaskID))
	begin
	INSERT INTO A_ACTUAL_PARTS_CALL_DATA (ID,PERSON_ID,ACTUAL_PART_ID,PURCHASE_HIST_ID,DRCM,MODBY,ROOT_TASK,REASON)
	VALUES(newID(),@strNTLogin,@objectID,@purchHistID,getDate(),@strNTLogin,@callRootTaskID,@reason)
	end







