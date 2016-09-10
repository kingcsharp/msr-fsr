


/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/
CREATE                    PROCEDURE A_SP_TASK_COMMENT_UPDATE_ONE_COMMENT
@newID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@commentID varchar(50),
@taskID varchar(50),
@comment nvarchar(4000),
@allowedToView varchar(50),
@strNTlogin varchar(50)
AS
declare @myCo as varchar(50),@isNew tinyInt

set @isNew = 0

SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin
print 'my company is' + @myCo + ''

if @commentID is NULL
begin
	set @isNew = 1
	exec sp_getUniqueID3 @newID OUTPUT
	INSERT INTO A_TASK_COMMENT ([ID],TASK_ID,WRITER,WRITER_COMPANY,DATE_CREATED,DRCM,MODBY)
	VALUES
	(@newID,@taskID,@strNTlogin,@myCO,getDate(),getDate(),@strNTlogin)
	SET @commentID = @newID
end
UPDATE A_TASK_COMMENT 
SET COMMENT = @COMMENT,
	ALLOWED_TO_VIEW = @allowedToView,
	DRCM = getDate()
	WHERE ID = @commentID

set @newID = @commentID


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
	WHERE ID = @taskID
declare @linkPath varchar(300)
set @linkPath = dbo.xmlEncode('asp/ActualTasks/searchTasks.asp?ID=' + @taskID + '&ID_MATCH_EXACTLY=TRUE')
declare @peopleList as varchar(8000),@sql varchar(4000),@label nvarchar(100)
set @sql = 'SELECT PERSON_ID FROM A_TASK_EMAIL_PEOPLE_LINK WHERE TASK_ID = ''' + @taskID + ''' AND PERSON_ID <> ''' + @strNTLogin + ''''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@peopleList OUTPUT
if @requestor <> @strNTLogin
	set @peopleList = isNull(@requestor,'') + isNull(',' + @peopleList,'')

if @peopleList = ''
	goto fin 

declare @strSubj varchar(8000),@strBody varchar(8000)
if @isNew = 1
	begin
	set @strSubj = 'A new comment was added to a task'
	set @label = 'New Comment:'
	end
else
	begin
	set @strSubj = 'A comment was editted on a task'
	set @label = 'Editted Comment:'
	end

set @strBody = 
'<obj type="table">
	<obj type="row">
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Description first 50 characters" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@desc,1100)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
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
	<obj type="row">
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + @label + '" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@comment) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
</obj>'

exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@peopleList,@linkPath,@strSubj,@strBody,@strNTLogin


fin:

