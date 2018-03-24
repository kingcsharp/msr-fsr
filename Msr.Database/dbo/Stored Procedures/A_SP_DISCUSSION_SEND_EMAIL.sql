

CREATE PROCEDURE DBO.A_SP_DISCUSSION_SEND_EMAIL 
@ID varchar(50),
@alerteeOnly tinyInt,
@strNTLogin varchar(50)
AS
declare @peopleList as varchar(8000),@sql varchar(4000)

if @alerteeOnly = 1
	set @sql ='SELECT ALERTEE_ID AS ID FROM A_DISCUSSION_RESPONSE_ALERTS
	WHERE DISCUSSION_ID =''' + @ID +''' and ALERTEE_ID <> ''' + @strNTLogin + '''' 
else
	set @sql = 'EXEC A_SP_DISCUSSION_EMAIL_GET_INVITED_PEOPLE ''' + @ID + ''',''' + @strNTLogin + ''''

exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@peopleList OUTPUT
declare @linkPath varchar (1000)
declare @emailSubject varchar(1000) 
declare @emailBody varchar(8000)
set @linkPath = dbo.xmlEncode('asp/discussions/searchDiscussion.asp?DISCUSSION_NUMBER_MATCH_EXACTLY=TRUE&STATUS=ALL&DISCUSSION_NUMBER=' + @ID + '')
set @emailSubject = 'ANSWER Discussion Alert'
set @emailBody = ''

declare @subj nvarchar(200),@initName nvarchar(200),@initID varchar(50),@initComment nvarchar(1000)

SELECT @initID = INITIATOR FROM A_DISCUSSIONS WHERE ID = @ID

if @initID <> @strNTLogin
	set @peopleList = @peopleList + ',' + @initID 

SELECT @initName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @initID
SELECT @initComment = RESPONSE FROM A_DISCUSSION_RESPONSE WHERE DISCUSSION_ID = @ID AND PARENT_ID is NULL

SELECT @subj = SUBJECT FROM A_DISCUSSIONS WHERE ID = @ID

set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Discussion Subject:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@subj,1100)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Discussion Initiator:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@initName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Initial Comment:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@initComment),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>'



declare @curs as CURSOR, @drText varchar(50),@writerName nvarchar(50),@d datetime
set @curs = cursor for SELECT top 3 dr.RESPONSE,p.FULL_NAME,dr.DRCM FROM
	A_DISCUSSION_RESPONSE dr, A_V_PEOPLE_APPROVED_DATA p WHERE
		dr.writer = p.ID AND dr.DISCUSSION_ID = @ID AND dr.PARENT_ID IS NOT NULL
		ORDER BY dr.DRCM DESC
open @curs
fetch next from @curs INTO @drText,@writerName,@d
set @emailBody = @emailBody + '
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top"/>
			<obj type="text"><attribute name="value" value="Last Three Responses:" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top"/>
	'
while @@fetch_status = 0
	begin
	set @emailBody = @emailBody + '<obj type="text"><attribute name="value" value="(' +
		isNull(dbo.xmlEncode(@writerName),'') + ')[' + isNull(dbo.xmlEncode(convert(varchar(50),@d)),'') + '] ' + isNull(dbo.xmlEncode(@drText),'') + '" />
		<attribute name="dontUsePutText" value="true" /></obj>'
	fetch next from @curs INTO @drText,@writerName,@d
	end
set @emailBody = @emailBody + '</obj></obj>'



set @emailBody = @emailBody + '</obj>'






exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@peopleList,@linkPath,@emailSubject,@emailBody,@strNTLogin