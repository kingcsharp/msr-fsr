CREATE PROCEDURE dbo.A_SP_BATCH_ADD_FILL_TO_BATCH
@outID varchar(50) OUTPUT,
@msgs varchar(2000) OUTPUT,
@batchFillID varchar(50),
@subFillID varchar(50),
@strNTLogin varchar(50)
AS
print 'Adding Fill ' + @subFillID + ' to Batch Fill ' + @batchFillID
print 'First lets install our part in the batch part'
declare @subPartID varchar(50),@batchPartID varchar(50)
SELECT @batchPartID = FILL_OBJ_ID FROM A_FILLS WHERE ID = @batchFillID
SELECT @subPartID = FILL_OBJ_ID FROM A_FILLS WHERE ID = @subFillID
UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = @batchPartID 
	WHERE OBJECT_ID IN (SELECT ID FROM A_OBJECTS WHERE ROOT = @subPartID)
exec A_SP_ACTUAL_PARTS_CREATE_CHILD_LIST null,@batchPartID,0,null,@strNTLogin
print 'Now lets update our fill info'
UPDATE A_FILLS SET BATCHED = 1, BATCH_PARENT = @batchFillID WHERE ID = @subFillID
print 'Now We need to delete the children tasks and set the task to accepted since it is batched'
declare @subRootTask varchar(50)
SELECT @subRootTask = TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @subFillID
create TABLE #myTasks (ID VARCHAR(50))
INSERT INTO #myTasks SELECT ID FROM A_TASKS WHERE PARENT_ID = @subRootTask
DELETE FROM A_MONITOR_RESULTS 
	WHERE MONITOR_TEMPLATE_ID IN 
		(SELECT ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID IN (SELECT ID FROM #myTasks))
DELETE FROM A_MONITOR_TEMPLATES 
	WHERE TASK_ID IN (SELECT ID FROM #myTasks)
DELETE FROM A_TASKS
	WHERE ID IN (SELECT ID FROM #myTasks)
DELETE FROM A_TASK_ORDER_INFORMATION 
	WHERE TASK_ID IN (SELECT ID FROM #myTasks)
UPDATE A_TASKS SET HAS_CHILD = 0 WHERE ID = @subRootTask
print 'All sub tasks deleted and ready to roll'








