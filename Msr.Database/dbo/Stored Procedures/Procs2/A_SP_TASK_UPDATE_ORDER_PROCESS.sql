






CREATE        PROCEDURE DBO.A_SP_TASK_UPDATE_ORDER_PROCESS
@ID varchar(50),
@MODBY varchar(50)
AS
print 'Updating the order process due to a change or insert of a task with id = ' + @ID
declare @thisTaskStatus varchar(50),@sysID varchar(50),@parentID varchar(50)
SELECT @thisTaskStatus = STATUS, @sysID = SYSTEM_TASK, @parentID = PARENT_ID FROM A_TASKS WHERE ID = @ID
print 'The status = ' + @thisTaskStatus
if @thisTaskStatus = 'FINISHED'
	begin
	print 'This task was marked finished, so now we need to start all the other fills that were on this order'
	declare @purchItemID varchar(50),@fillID varchar(50),@purchaseHistID varchar(50)
	SELECT @purchItemID = PURCHASE_ITEM_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @ID
	SELECT @purchaseHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
	print 'Purchase ID = ' + isNull(@purchaseHistID,'NULL')
	print 'Now check to make sure all the tasks having to do with this quote Item are finished or closed'
	Declare @it nvarchar(50),@taskID varchar(50),@requestor varchar(50)
	Declare @curs Cursor
	set @curs = Cursor For SELECT FOL FROM A_ORDER_ITEM_PRECEDENTS WHERE PREV = @purchItemID
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'Going to start purchase item = ' + isnull(@it,'NULL')
		SELECT @taskID = ID,@requestor = REQUESTOR FROM A_V_TASK_WITH_ORDER_INFORMATION WHERE PURCHASE_ITEM_ID = @it
		print 'The task we are going to request is ' + @taskID
		exec A_SP_TASK_SUBMIT null,null,@taskID,@requestor
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end

if @sysID = 'SYS_PROVIDE_AND_STAY'
	begin
	print 'This is a provide and stay so if it is requested we need to go ahead and finish and close it'
	if @thisTaskStatus = 'REQUESTED'
		begin
		print 'It is requested'
		UPDATE A_TASKS SET STATUS = 'FINISHED' WHERE ID = @ID
		UPDATE A_TASK_ASSIGNEE SET STATUS = 'FINISHED' WHERE TASK_ID = @ID AND ACTIVE = 1
		UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @ID		
		end
	end

if @parentID is not null
	begin
	print 'There is a parent'
	declare @parentSysID varchar(50)
	SELECT @parentSysID = SYSTEM_TASK FROM A_TASKS WHERE ID = @parentID
	print 'The parent Sys ID = ' + @parentSysID
	if @parentSysID = 'SYS_PURCHASE'
		begin
		print 'This is a purchase'
		if not(Exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @parentID AND STATUS not in ('FINISHED','CLOSED')))
			begin
			print 'There are no Tasks that are not finished'
			UPDATE A_TASKS SET STATUS = 'FINISHED' WHERE ID = @parentID
			UPDATE A_TASK_ASSIGNEE SET STATUS = 'FINISHED' WHERE TASK_ID = @parentID AND ACTIVE = 1
			UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @parentID
			end
		end
	end



print 'Out of A_SP_TASK_UPDATE_ORDER_PROCESS'




