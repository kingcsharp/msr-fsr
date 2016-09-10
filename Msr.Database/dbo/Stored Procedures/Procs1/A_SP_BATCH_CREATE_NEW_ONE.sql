


CREATE   PROCEDURE dbo.A_SP_BATCH_CREATE_NEW_ONE
@newBatchFillID varchar(50) OUTPUT,
@msgs varchar(2000) OUTPUT,
@fillID varchar(50),
@strNTLogin varchar(50)
AS
begin transaction
declare @custID varchar(50),@procID  varchar(50),@batchPartID  varchar(50),@myRootCo varchar(50)
declare @retVal int,@orderItemID varchar(50),@purchHistID varchar(50)
SELECT @orderItemID = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
if @@ERROR <> 0 goto problem
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @orderItemID
if @@ERROR <> 0 goto problem
SELECT @custID = PURCHASING_CO FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID
if @@ERROR <> 0 goto problem
SELECT @myRootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if @@ERROR <> 0 goto problem
if not exists(SELECT ID FROM A_PARTS WHERE IS_BATCH = 1 and CREATING_CO = @myRootCo)
	begin
	exec A_SP_PARTS_CREATE_BATCH_PART @batchPartID OUTPUT,@retVal OUTPUT,@strNTLogin
	if @retVal <> 0 goto problem
	end
print 'Found the batch part = ' + @batchPartID
print 'Create an actual part for the batch'
declare @batchActualPartID varchar(50)
exec A_SP_ACTUAL_PARTS_CREATE_BATCH_PART @batchActualPartID OUTPUT,@retVal OUTPUT,@batchPartID,@custID,@strNTLogin
if @retVal <> 0 goto problem
SELECT * FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @batchActualPartID
if @@ERROR <> 0 goto problem
print 'Making the fill for the batch'
exec sp_GetUniqueID3 @newBatchFillID OUTPUT
INSERT INTO A_FILLS (ID,FILL_OBJ_ID,FILL_QTY,FILLER,DRCM,MODBY,FILL_BY,BATCH_FILL)
	VALUES (@newBatchFillID,@batchActualPartID,1,@strNTLogin,getDate(),@strNTLogin,'BATCH',1)
if @@ERROR <> 0 goto problem
SELECT * FROM A_FILLS WHERE ID = @newBatchFillID
if @@ERROR <> 0 goto problem
print 'Now make a dummy order item and purchase'
declare @copyOrderItem varchar(50),@myOrderItemID varchar(50),@copyPurchHistID varchar(50),@myPurchHistID varchar(50)
SELECT @copyOrderItem = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
if @@ERROR <> 0 goto problem
SELECT @copyPurchHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @copyOrderItem
if @@ERROR <> 0 goto problem
exec sp_GetUniqueID3 @myPurchHistID OUTPUT
if @@ERROR <> 0 goto problem
INSERT INTO A_PURCHASES_HISTORY (ID,PURCHASER,PURCHASING_CO,ACCT_FOR_ALL,ORDER_ID)
SELECT @myPurchHistID,PURCHASER,PURCHASING_CO,ACCT_FOR_ALL,ORDER_ID FROM A_PURCHASES_HISTORY WHERE ID = @copyPurchHistID
if @@ERROR <> 0 goto problem
exec sp_GetUniqueID3 @myOrderItemID OUTPUT
INSERT INTO A_ORDER_ITEMS (ID,PRODUCT_ID,QTY,ACCOUNT_ID,SUPPLIER_ID,PURCHASE_HIST_ID)
	SELECT @myOrderItemID,PRODUCT_ID,1,ACCOUNT_ID,SUPPLIER_ID,@myPurchHistID FROM A_ORDER_ITEMS WHERE ID = @copyOrderItem
if @@ERROR <> 0 goto problem
UPDATE A_FILLS SET PURCH_ITEM_ID = @myOrderItemID WHERE ID = @newBatchFillID
if @@ERROR <> 0 goto problem
print 'Now make the procedure assigned to this fill and this object by copying the task of the one we already got'
declare @copyTaskID varchar(50),@myTaskID varchar(50)
SELECT @copyTaskID = TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @fillID
print 'TASK ID = ' + @copyTaskID
exec sp_GetUniqueID3 @myTaskID OUTPUT
INSERT INTO A_TASKS (ID,DESCRIPTION,STATUS,COMMENT,REQUESTOR,CREATED_BY,CREATE_DATE,PROCEDURE_ID,SECURITY_LEVEL,
			GROUP_REQUESTEE_ID,HAS_CHILD,HAS_REF_PROC,ORIG_REQUESTOR_ID,PRIORITY,CHILD_STATUS,RECURSION_NUMBER)
	SELECT @myTaskID,DESCRIPTION,STATUS,'Batch Fill ID = ' + @newBatchFillID,REQUESTOR,CREATED_BY,
	CREATE_DATE,PROCEDURE_ID,SECURITY_LEVEL,GROUP_REQUESTEE_ID,HAS_CHILD,
	HAS_REF_PROC,ORIG_REQUESTOR_ID,PRIORITY,CHILD_STATUS,RECURSION_NUMBER 
	FROM A_TASKS 
	WHERE ID = @copyTaskID
if @@ERROR <> 0 goto problem
INSERT INTO A_TASK_ASSIGNEE (ID,TASK_ID,PERSON_ASSIGNED,ROLE_ASSIGNED,REQUESTOR,REQUEST_DATE,ACTIVE,STATUS)
	SELECT newID(),@myTaskID,PERSON_ASSIGNED,ROLE_ASSIGNED,REQUESTOR,REQUEST_DATE,ACTIVE,STATUS
	FROM A_TASK_ASSIGNEE 
	WHERE TASK_ID = @copyTaskID
if @@ERROR <> 0 goto problem
exec A_SP_TASKS_UPDATE_DATA @myTaskID,@strNTLogin
print 'Created the Task now to copy or create all the other things'
print 'We need to link in the batch'
INSERT INTO A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED (ID,TASK_ID,ACTUAL_PART_ID)
		VALUES (newID(),@myTaskID,@batchActualPartID)
if @@ERROR <> 0 goto problem
INSERT INTO A_TASK_OBJECT_LINK (ID,TASK_ID,OBJECT_ID)
		VALUES (newID(),@myTaskID,@batchActualPartID)
if @@ERROR <> 0 goto problem
INSERT INTO A_TASK_ORDER_INFORMATION (TASK_ID,PURCHASE_ITEM_ROLE,FILL_ITEM_ID)
		VALUES (@myTAskID,'BATCHER',@newBatchFillID)
if @@ERROR <> 0 goto problem
INSERT INTO A_TASK_REFERENCE_PROCEDURES (ID,TASK_ID,REF_PROCEDURE)
	SELECT newID(),@myTAskID,REF_PROCEDURE FROM A_TASK_REFERENCE_PROCEDURES WHERE TASK_ID = @copyTaskID
if @@ERROR <> 0 goto problem
print 'Now we have a batch task and we need to steal all the steps from the other one'
UPDATE A_TASKS SET PARENT_ID = @myTaskID WHERE PARENT_ID = @copyTaskID
if @@ERROR <> 0 goto problem
UPDATE A_TASK_OBJECT_LINK SET OBJECT_ID = @batchActualPartID WHERE TASK_ID IN (SELECT ID FROM A_TASKS WHERE PARENT_ID = @myTaskID)
if @@ERROR <> 0 goto problem
UPDATE A_TASK_ORDER_INFORMATION SET PURCHASE_HIST_ID = null WHERE TASK_ID IN (SELECT ID FROM A_TASKS WHERE PARENT_ID = @myTaskID)
if @@ERROR <> 0 goto problem
UPDATE A_TASKS_RELATION_TABLE SET PARENT_ID = @myTaskID WHERE PARENT_ID = @copyTaskID
if @@ERROR <> 0 goto problem
print 'Stole all the steps'

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished creating a batch with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print 'There was a problem vreating a batch so we rolled everything back '
return 1


