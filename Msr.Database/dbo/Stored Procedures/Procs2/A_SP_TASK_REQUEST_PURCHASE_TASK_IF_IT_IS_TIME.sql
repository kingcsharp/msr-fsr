

CREATE   PROCEDURE dbo.A_SP_TASK_REQUEST_PURCHASE_TASK_IF_IT_IS_TIME
@taskID varchaR(50)
AS
declare @purchItemID varchar(50)
SELECT @purchItemID = PURCHASE_ITEM_ID FROM A_V_TASK_WITH_ORDER_INFORMATION 
	WHERE TASK_ID = @taskID

if @purchItemID is not null
	begin
	print 'We have a purchase Item ID of ' + @purchItemID
	print 'We need to find if any of the previous purchase Item Tasks are not finished'
	if exists (SELECT * FROM A_V_TASK_WITH_ORDER_INFORMATION 
		WHERE TASK_STATUS NOT IN ('FINISHED','CLOSED') AND
			PURCHASE_ITEM_ID IN (SELECT PREV FROM A_ORDER_ITEM_PRECEDENTS WHERE
				FOL = @purchItemID)
		)
		begin
		print 'This task has previous purchase items that need to be done'
		UPDATE A_TASKS SET STATUS = 'WAITING_ON_PREVIOUS_TASKS' WHERE ID = @taskID
		end
	else
		begin
		declare @req varchar(50)
		SELECT @req = REQUESTOR FROM A_TASKS WHERE ID = @taskID
		exec A_SP_TASK_SUBMIT null,nulll,@taskID,@req
		end
	end





