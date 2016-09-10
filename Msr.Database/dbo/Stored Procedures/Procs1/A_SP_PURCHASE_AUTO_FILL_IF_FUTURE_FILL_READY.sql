


CREATE    PROCEDURE dbo.A_SP_PURCHASE_AUTO_FILL_IF_FUTURE_FILL_READY
@purchID varchar(50),
@strNTLogin varchar(50)
AS
print 'Auto Filling if possible'
if not exists(SELECT * FROM A_PURCHASE_FUTURE_FILL_ITEM WHERE PURCHASE_ID = @purchID)
	goto fin

declare @fillObj varchar(50),@pHistID varchar(50),@taskID varchar(50),@creatorID varchar(50),
	@fillID varchar(50),@fillActPartID varchar(50),@fillActPartObjID varchar(50)
SELECT @fillObj = FILL_OBJ_ID, @creatorID = MODBY	FROM A_PURCHASE_FUTURE_FILL_ITEM WHERE PURCHASE_ID = @purchID
SELECT @fillActPartID = ROOT FROM A_OBJECTS WHERE ID = @fillObj
print 'We can auto fill this one with fill obj id  = ' + @fillObj
SELECT @pHistID = HISTORY_REF_ID FROM A_PURCHASES WHERE ID = @purchID
print 'get the task ID from phist = ' + @pHistID
SELECT @taskID = TASK_ID,@fillID = ID FROM A_V_FILLS_SEARCH WHERE PURCHASE_HIST_ID = @pHistID AND FILL_BY = 'CUSTOMER'
print 'Fill ID = ' + @fillID
declare @RET_STATUS as varchar(50),@MSGS as varchar(50)


exec A_SP_TASK_ACCEPT @RET_STATUS OUTPUT,@MSGS OUTPUT,@taskID,@creatorID
if @RET_STATUS is not NULL and @RET_STATUS <> ''
	begin
	print 'Error Accepting Task'
	goto fin
	end

if @fillObj is not null and @fillID is not null
	exec A_SP_FILLS_UPDATE_FILL @RET_STATUS OUTPUT,@MSGS OUTPUT,@fillID,@fillActPartID,@creatorID


fin:


