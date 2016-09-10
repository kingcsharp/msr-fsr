








CREATE               PROCEDURE A_SP_TASK_SUBMIT
@SUBMIT_STATUS as varchar(50) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Submitting a task'
print 'Verifying the task is a valid one.'
declare @rPerson as varchar(50), @rRole as varchar(50), @requestor as varchar(50), @desc as nvarchar(4000),
	@noEmail tinyInt,@status varchar(50),@parentID varchar(50)
set @noEmail = 0

SELECT @status = STATUS,@rPerson = REQUESTEE_ID, @rRole = GROUP_REQUESTEE_ID, @requestor = REQUESTOR, @desc = DESCRIPTION,
	@parentID = PARENT_ID
	FROM A_TASKS WHERE ID = @ID

if @rPerson is null and @rRole is null
	begin
		set @SUBMIT_STATUS = 'ERROR - Neither a person or Group is assigned this task'
		goto fin
	end
if @rPerson is not null and @rRole is not null
	begin
		UPDATE A_TASKS SET REQUESTEE_ID = NULL WHERE ID = @ID
		--set @SUBMIT_STATUS = 'ERROR - Both a person and a Group are assigned this task'
		--goto fin
	end
if @requestor is null
	begin
		set @SUBMIT_STATUS = 'ERROR - No Requestor for this task'
		goto fin
	end
if @status in ('REQUESTED','FINISHED','CLOSED')
	begin
		set @SUBMIT_STATUS = 'ERROR - Status not correct for requesting'
		goto fin
	end
print 'It is a valid task for assigning'
print 'Checking now to see if this is a provide and stay and if it is we need to wait for the shipping child of it to be done.'
declare @sysID varchar(50),@shipTaskID varchar(50),@shipTaskStatus varchar(50),@purchItemID varchar(50),@shipPurchItemID varchar(50)
SELECT @sysID = SYSTEM_TASK FROM A_TASKS WHERE ID = @ID
if @sysID = 'SYS_PROVIDE_AND_STAY'
	begin
	print 'This is a provide and stay we need to check if there is a shipping and if it is done.'
	SELECT @purchITemID = PURCHASE_ITEM_ID FROM A_V_TASK_WITH_ORDER_INFORMATION
	SELECT @shipPurchItemID = SHIP_TO_ID FROM A_V_ORDER_ITEMS_DATA_WITH_SHIPPING WHERE ID = @purchItemID
	SELECT @shipTaskID = TASK_ID, @shipTaskStatus = TASK_STATUS FROM A_V_TASK_WITH_ORDER_INFORMATION WHERE PURCHASE_ITEM_ID = @shipPurchItemID
	if not(@shipTaskStatus is null or @shipTaskStatus in ('FINISHED','COMPLETE'))
		begin
		print 'This task is not ready to be submitted.  It is waiting on shipping.'
		UPDATE A_TASKS SET STATUS = 'WAITING_ON_SHIPPING' WHERE ID = @ID
		end

	set @noEmail = 1
	end
print 'set the Task status to requested'
UPDATE A_TASKS SET STATUS = 'REQUESTED',DRCM=getDate(),MODBY=@strNTLogin,LAST_REQUEST_DATE=getDate() WHERE ID = @ID
print 'Set the dates in the request table'
UPDATE A_TASK_ASSIGNEE SET REQUEST_DATE = getDate(),MODBY=@strNTLogin WHERE TASK_ID = @ID AND ACTIVE = 1
print 'Call Auto Accept to see if this one can move along'
exec A_SP_TASK_AUTO_ACCEPT @ID,@strNTLogin

if @parentID is not null 
	begin
	print 'This one has a parent.  Should we make it acceted if it is Accepted and Qued?'
	declare @parentStat varchar(50)
	SELECT @parentStat = STATUS FROM A_TASKS WHERE ID = @parentID
	if @parentStat = 'AccQued'
		UPDATE A_TASKS SET STATUS = 'ACCEPTED'  WHERE ID = @parentID
	end

exec A_SP_TASK_POPULATE_LINKED_ACTUAL_PARTS_WHEN_FINISHED @ID


if @noEmail = 0 exec A_SP_TASK_SEND_EMAIL_UPDATE @ID,'submitted',@strNTLogin

fin:

print @SUBMIT_STATUS








