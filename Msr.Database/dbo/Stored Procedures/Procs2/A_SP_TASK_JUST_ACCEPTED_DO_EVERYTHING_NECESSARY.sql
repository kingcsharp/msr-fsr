









CREATE           PROCEDURE dbo.A_SP_TASK_JUST_ACCEPTED_DO_EVERYTHING_NECESSARY
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @curs cursor,@it varchar(50),@taskRoot varchar(50)
exec A_SP_TASK_GET_CALL_ROOT_TASK_ID @taskRoot OUTPUT,@taskID
print 'Checking to see if we have any shipping tasks which need to be started'
if exists(SELECT * FROM A_V_TASK_WITH_ORDER_INFORMATION 
	WHERE PARENT_ID = @taskID AND PURCHASE_ITEM_ROLE = 'TO_SHIP' AND TASK_STATUS = 'PENDING_PARENT_ACCEPTANCE')
	begin
	print 'We have a shipping task so we need to submit it.'
	set @curs = Cursor For 
		SELECT ID FROM A_V_TASK_WITH_ORDER_INFORMATION 
			WHERE PARENT_ID = @taskID AND PURCHASE_ITEM_ROLE = 'TO_SHIP' 
					AND TASK_STATUS = 'PENDING_PARENT_ACCEPTANCE'
	open @curs
		Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
			Begin
			print 'Found a task with number = ' + @it
			exec A_SP_TASK_SUBMIT null,null,@it,@strNTLogin
			Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	GOTO FIN
	end
	
print 'First we are going to start the first step children tasks that were waiting for the parent to be accepted'
set @curs = Cursor For 
SELECT STEP_ID FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA 
	WHERE PARENT_ID = @taskID AND (PRINT_ORDER IS NULL OR PRINT_ORDER = 1 OR PREV_STEP is NULL)
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'Found a task with number = ' + @it
	if (SELECT STATUS FROM A_TASKS WHERE ID = @it) = 'PENDING_PARENT_ACCEPTANCE' 
		exec A_SP_TASK_SUBMIT null,null,@it,@strNTLogin
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs

declare @myPurchRole varchar(50),@toShipTask varchar(50)
SELECT @myPurchRole = PURCHASE_ITEM_ROLE FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
if @myPurchRole = 'GROUPER'
	begin
	SELECT @toShipTask = ID FROM A_TASKS t, A_TASK_ORDER_INFORMATION oi 
		WHERE t.PARENT_ID = @taskID AND t.ID = oi.TASK_ID AND oi.PURCHASE_ITEM_ROLE = 'TO_SHIP'
	if @toShipTask is not null
		exec A_SP_TASK_SUBMIT null,null,@toShipTask,@strNTLogin 
	end

declare @actStartID varchar(50),@myStartDate datetime
set @myStartDate = getDate()
SELECT @actStartID = ID FROM A_TASK_DATES 
	WHERE TASK_ID = @taskID AND DATE_TYPE = 'ACTUAL_START'
if @actStartID is null
	exec A_SP_TASK_DATES_UPDATE @taskID,'ACTUAL_START',@myStartDate,@strNTLogin

declare @parentID varchar(50)
SELECT @parentID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
set @actStartID = NULL
SELECT @actStartID = ID FROM A_TASK_DATES 
	WHERE TASK_ID = @parentID AND DATE_TYPE = 'ACTUAL_START'
if @actStartID is null
	exec A_SP_TASK_DATES_UPDATE @parentID,'ACTUAL_START',@myStartDate,@strNTLogin



declare @CrequesteeID varchar(50),@groupRID varchar(50),@requestor varchar(50),@pRequesteeID varchar(50)
declare @REQUESTEE_ID varchar(50),@GROUP_REQUESTEE_ID varchar(2000),@LATEST_REQUESTEE_NAME varchar(2000),@childID varchar(50)

set @curs = CURSOR FOR SELECT STEP_ID FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA 
	WHERE PARENT_ID = @taskID AND (PH_STEP_ID is NULL) AND STATUS = 'PENDING_PARENT_ACCEPTANCE'
open @curs
fetch next from @curs into @childID
while @@fetch_status = 0
	begin
	SELECT @CrequesteeID = REQUESTEE_ID,
		@groupRID = GROUP_REQUESTEE_ID,
		@requestor = REQUESTOR
		FROM A_TASKS WHERE ID = @childID

	if (@CrequesteeID is NULL and @groupRID is null)
		begin
		SELECT 
			@REQUESTEE_ID = REQUESTEE_ID,
			@GROUP_REQUESTEE_ID = GROUP_REQUESTEE_ID,
			@LATEST_REQUESTEE_NAME = LATEST_REQUESTEE_NAME
		FROM A_TASKS WHERE ID = @taskID
		UPDATE A_TASKS SET 
			REQUESTEE_ID = @REQUESTEE_ID,
			GROUP_REQUESTEE_ID = @GROUP_REQUESTEE_ID,
			LATEST_REQUESTEE_NAME = @LATEST_REQUESTEE_NAME
			WHERE ID = @childID
		exec A_SP_TASK_UPDATE_TASK_ASSIGNEE @childID,@requestor
	end
	exec A_SP_TASK_SUBMIT null,null,@childID,@requestor
	exec A_SP_TASK_ACCEPT null,null,@childID,@REQUESTEE_ID
	end





FIN:












