
CREATE  PROCEDURE dbo.A_SP_ACTUAL_PART_REPLACE_PART_IN_CALL 
@taskID varchar(50),
@oldPartID varchar(50),
@newPartID varchar(50),
@strNTLogin varchar(50)
AS
begin transaction
print 'Replacing ' + @oldPartID + ' with ' + @newPartID
declare @parentTaskID varchar(50)
SELECT @parentTaskID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
declare @purchHistID varchar(50)
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
declare @purchItemID varchar(50)
SELECT @purchItemID = PURCHASE_ITEM_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @parentTaskID
UPDATE A_FILLS SET FILL_OBJ_ID = @newPartID WHERE
	FILL_OBJ_ID = @oldPartID AND PURCH_ITEM_ID = @purchItemID

exec A_SP_ACTUAL_PART_ADD_TO_CALL @newPartID,@taskID,'workingOn',@strNTLogin
if @@error <> 0 goto problem
DELETE FROM A_ACTUAL_PARTS_CALL_DATA 
	WHERE ACTUAL_PART_ID = @oldPartID and PURCHASE_HIST_ID = @purchHistID
if @@error <> 0 goto problem
INSERT INTO A_TASK_OBJECT_LINK (ID,TASK_ID,OBJECT_ID,DRCM,MODBY)
SELECT newID(),TASK_ID,@newPartID,DRCM,MODBY
	FROM A_TASK_OBJECT_LINK 
		WHERE TASK_ID IN 
			(
			SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @purchHistID
				AND 
				TASK_ID IN 
				(
				SELECT TASK_ID FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = @oldPartID
				)
			)
if @@error <> 0 goto problem
INSERT INTO A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED (ID,TASK_ID,ACTUAL_PART_ID,DRCM,MODBY)
SELECT newID(),TASK_ID,@newPartID,DRCM,MODBY
	FROM A_TASK_OBJECT_LINK 
		WHERE TASK_ID IN 
			(
			SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @purchHistID
				AND 
				TASK_ID IN 
				(
				SELECT TASK_ID FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = @oldPartID
				)
			)
if @@error <> 0 goto problem

DELETE FROM A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED WHERE ACTUAL_PART_ID = @oldPartID AND 
		TASK_ID IN 
			(
			SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @purchHistID
				AND 
				TASK_ID IN 
				(
				SELECT TASK_ID FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = @oldPartID
				)
			)
if @@error <> 0 goto problem


DELETE FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = @oldPartID AND 
		TASK_ID IN 
			(
			SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @purchHistID
				AND 
				TASK_ID IN 
				(
				SELECT TASK_ID FROM A_TASK_OBJECT_LINK WHERE OBJECT_ID = @oldPartID
				)
			)
if @@error <> 0 goto problem







fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PART_REPLACE_PART_IN_CALL with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PART_REPLACE_PART_IN_CALL and we will terminate and not finish anything '
return 1


