








CREATE          PROCEDURE dbo.A_SP_TASK_UPDATE_BASED_ON_PURCHASE_INFO 
@taskID varchar(50),
@strNTLogin varchar(50)
AS
print 'In A_SP_TASK_UPDATE_BASED_ON_PURCHASE_INFO'
declare @sysID varchar(50),@parentID varchar(50),
	@shippingID varchar(50),@purchItemID varchar(50),@purchHistID varchar(50),
	@origReq varchar(50),@purchaseRole varchar(50),@curStatus varchar(50)

SELECT @curStatus = STATUS,@sysID = SYSTEM_TASK,@parentID = PARENT_ID,@origReq = REQUESTOR FROM A_TASKS WHERE ID = @taskID
SELECT @purchaseRole = PURCHASE_ITEM_ROLE FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
if @sysID = 'SYS_PROVIDE_AND_STAY'
	begin
	SELECT @shippingID = ID FROM A_TASKS WHERE SYSTEM_TASK = 'SYS_SHIPPING' AND PARENT_ID = @taskID 
		AND STATUS not in ('FINISHED','CLOSED')
	if @shippingID is null
		begin
		exec A_SP_TASK_FINISH @taskID,@strNTLogin
		UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @taskID
		end
	else
		UPDATE A_TASKS SET STATUS = 'WAITING_ON_SHIPPING' WHERE ID = @taskID
	end
if @sysID = 'SYS_PURCHASE'
	begin
	SELECT @purchHistID = PURCHASE_HIST_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
	if exists(SELECT * FROM A_V_TASK_WITH_ORDER_INFORMATION 
				WHERE TASK_STATUS not in ('FINISHED','CLOSED') AND
					PURCHASE_HIST_ID = @purchHistID)
		begin
		print 'This is a purchase, but there are tasks still waiting to be finished.'
		UPDATE A_TASKS SET STATUS = 'PURCHASE_EXECUTING' WHERE ID = @taskID
		end
	else
		begin
		exec A_SP_TASK_FINISH @taskID,@strNTLogin
		print 'Setting the task to closed now'
		UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @taskID
		end
	end
if @sysID = 'SYS_PROVIDE_AND_CONSUMED'
	begin
	print 'This is a provide and consume'
	SELECT @shippingID = ID FROM A_TASKS WHERE SYSTEM_TASK = 'SYS_SHIPPING' AND PARENT_ID = @taskID 
		AND STATUS not in ('FINISHED','CLOSED')
	if @shippingID is null
		begin
		exec A_SP_TASK_FINISH @taskID,@strNTLogin
		UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @taskID
		end
	else
		UPDATE A_TASKS SET STATUS = 'WAITING_ON_SHIPPING' WHERE ID = @taskID
	goto fin
	end
if @sysID = 'SYS_SHIPPING'
	begin
	print 'This is a shipping task.'
	print 'Check to see if the parent is waiting on it'
	if 'WAITING_ON_SHIPPING' = (SELECT STATUS FROM A_TASKS WHERE ID = @parentID)
		exec A_SP_TASK_SUBMIT null,null,@taskID,@origReq
	end

print 'Does this one need to be requested??'
if @purchaseRole = 'GROUPER' and @curStatus = 'CREATING'
	UPDATE A_TASKS SET STATUS = 'REQUESTED' WHERE ID = @taskID


--if (not exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @taskID) OR ((SELECT SYSTEM_TASK FROM A_TASKS WHERE ID = @parentID) = 'SYS_PURCHASE'))
--	if exists(SELECT * FROM A_TASKS WHERE ID = @taskID and STATUS = 'CREATING')
--		UPDATE A_TASKS SET STATUS = 'REQUESTED' WHERE ID = @taskID


fin:
print 'Out of A_SP_TASK_UPDATE_BASED_ON_PURCHASE_INFO'








