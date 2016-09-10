












CREATE              PROCEDURE DBO.A_SP_TASK_SEND_EMAIL_UPDATE
@ID varchar(50),
@actionTaken varchar(50),
@strNTLogin varchar(50)
AS
if exists(SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @ID)
	goto fin
print 'Sending an email about task = ' + isnull(@ID,'NULL') + ' where action = ' + isnull(@actionTaken,'NULL')
declare @strStepID varchar(50)
SELECT @strStepID = PROCEDURE_STEP_ID FROM A_TASKS WHERE ID = @ID
if not(@strStepID is null)
	goto fin
declare @linkPath varchar (1000), @emailSubject varchar(1000),
	@emailBody varchar(8000)

set @linkPath = dbo.xmlEncode('asp/ActualTasks/searchTasks.asp?ID=' + @ID + '&ID_MATCH_EXACTLY=TRUE')

if @actionTaken = 'submitted' set @emailSubject = 'New Task Request Submitted for you'
if @actionTaken = 'accepted' set @emailSubject = 'Task status changed to accepted'
if @actionTaken = 'rejected' set @emailSubject = 'This Task was rejected'
if @actionTaken = 'forwarded' set @emailSubject = 'Task forwarded'
if @actionTaken = 'deleted' set @emailSubject = 'Task Deleted'
if @actionTaken = 'reassigned' set @emailSubject = 'Task Reassigned'
if @actionTaken = 'takenBack' set @emailSubject = 'This Task was Taken Back'
if @actionTaken = 'finished' set @emailSubject = 'This Task was Finished'


declare 
	@desc varchar(4000),
	@requestor varchar(50),
	@requestee varchar(50),
	@roleRequestee varchar(50),
	@requesteeName varchar(500),
	@roleRequesteeName varchar(500),
	@requestorName varchar(500),
	@osd dateTime,
	@ostd dateTime,
	@csd dateTime,
	@cstd dateTime,
	@asd dateTime,
	@astd dateTime,
	@priority varchar(5),
	@status varchar(50)

SELECT @desc = DESCRIPTION,
	@requestor = REQUESTOR,
	@requestorName = REQUESTOR_NAME,
	@requestee = REQUESTEE_ID,
	@roleRequestee = GROUP_REQUESTEE_ID,
	@requesteeName = REQUESTEE_NAME,
	@roleRequesteeName = GROUP_REQUESTEE_NAME,
	@osd = ORIG_PLANNED_START_DATE,
	@ostd = ORIG_PLANNED_STOP_DATE,
	@csd = CUR_PLANNED_START_DATE,
	@cstd = CUR_PLANNED_STOP_DATE,
	@asd = ACTUAL_START_DATE,
	@astd = ACTUAL_STOP_DATE,
	@priority = PRIORITY,
	@status = STATUS
	FROM A_V_TASK_EDIT_DATA
	WHERE ID = @ID

declare @peopleList as varchar(8000),@sql varchar(4000)
set @sql = 'SELECT PERSON_ID FROM A_TASK_EMAIL_PEOPLE_LINK WHERE TASK_ID = ' + @ID
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@peopleList OUTPUT
set @requestee = isNull(@requestee,'') + isNull(',' + @peopleList,'')

set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Description first 50 characters" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@desc,300)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Priority" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull('PRIORITY_' + dbo.xmlEncode(@priority),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Status" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull('' + dbo.xmlEncode(@status),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Requestor" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@requestorName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Requested of" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@roleRequesteeName) + ' ','') + isNull(dbo.xmlEncode(@requesteeName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
</obj>
'

declare @combinedRequesteeList varchar(2000)

if exists(SELECT * FROM A_V_TASK_IS_QUICK_PRICE_CHECK WHERE TASK_ID = @ID)
	begin
	set @roleRequestee = null
	set @requestee = '7'
	set @emailSubject = '**FIXED** ' + @emailSubject
	end

if @actionTaken = 'submitted' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@roleRequestee,@requestee,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @actionTaken = 'finished' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@requestor,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @actionTaken = 'takenBack' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@roleRequestee,@requestee,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @actionTaken = 'accepted' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@requestor,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @actionTaken = 'rejected' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@requestor,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @actionTaken = 'reassigned' 
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@roleRequestee,@requestor,@linkPath,@emailSubject,@emailBody,@strNTLogin
if @actionTaken = 'forwarded'
	begin
	set @combinedRequesteeList = @requestor + isNULL(',' + @requestee,'')
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@roleRequestee,@combinedRequesteeList,@linkPath,@emailSubject,@emailBody,@strNTLogin
	end
if @actionTaken = 'deleted' 
	begin
	set @combinedRequesteeList = @requestor + isNULL(',' + @requestee,'')
	exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	@roleRequestee,@combinedRequesteeList,@linkPath,@emailSubject,@emailBody,@strNTLogin
	end





print'finished sending email about a task'


fin:








