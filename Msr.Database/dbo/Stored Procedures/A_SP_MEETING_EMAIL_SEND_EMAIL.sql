



CREATE              PROCEDURE DBO.A_SP_MEETING_EMAIL_SEND_EMAIL
@msgs varchar(4000) OUTPUT,
@meetingID varchar(50),
@strNTLogin varchar(50)
AS
declare @linkPath varchar (1000), 
		@emailSubject varchar(1000),
		@emailBody varchar(8000),
		@sql varchar(1000),
		@meetingName varchar(1000),
		@locationName varchar(4000),
		@initiatorName varchar(100),
		@initiator varchar(100),
		@settingAndPurpose varchar(100),
		@comment varchar(100),
		@hostID varchar(50),
		@timeZoneName varchar(50),
		@startTime varchar(50),
		@stopTime varchar(50)

SELECT  @meetingName = MEETING_NAME,
		@locationName = LOCATION_NAME,
		@initiatorName = INITIATOR_NAME,
		@initiator = OWNER,
		@settingAndPurpose = SETTING,
		@comment = COMMENT,
		@hostID = HOST,
		@startTime = START_DATE,
		@stopTime = STOP_DATE
FROM A_V_MEETING_GET_BY_ID
WHERE ID = @meetingID

set @startTime = dbo.timeToLocal(@startTime,@hostID)
set @stopTime = dbo.timeToLocal(@stopTime,@hostID)
SELECT @timeZoneName = DESCRIPTION FROM A_TIME_ZONES WHERE ID = (SELECT TIME_ZONE FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @hostID)


set @emailSubject = 'Meeting You Are Invited To'
set @linkPath = dbo.xmlEncode('asp/meetings/viewMeeting.asp?ID=' + @meetingID)
set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Name" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@meetingName,500)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Location" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@locationName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Initiator:" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@initiatorName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="SettingPurpose" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@settingAndPurpose),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Comment:" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@comment,1000)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Start Time:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="dontUsePutText" value="t" /><attribute name="value" value="' + isNull(dbo.xmlEncode(convert(varchar(50),@startTime,100)),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Stop Time:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="dontUsePutText" value="t" /><attribute name="value" value="' + isNull(dbo.xmlEncode(convert(varchar(50),@stopTime,100)),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Time Zone:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@timeZoneName),'') + '" /><attribute name="dontUsePutText" value="t" /></obj>
		</obj>
	</obj>
</obj>
<obj type="link">
	<attribute name="url" value="' +  isNull(dbo.getEmailURL(),'NULLEMAILURL') + '/' + isNull(dbo.xmlEncode('asp/meetings/processMeeting.asp?action=acceptMeeting&ID=' + @meetingID + ''),'NULLPATH') + '" />
	<obj type="text"><attribute name="value" value="Accept Meeting" /></obj>
</obj>
<obj type="link">
	<attribute name="url" value="' +  isNull(dbo.getEmailURL(),'NULLEMAILURL') + '/' + isNull(dbo.xmlEncode('asp/meetings/processMeeting.asp?action=rejectMeeting&ID=' + @meetingID + ''),'NULLPATH') + '" />
	<obj type="text"><attribute name="value" value="Reject Meeting" /></obj>
</obj>
<obj type="link">
	<attribute name="url" value="' +  isNull(dbo.getEmailURL(),'NULLEMAILURL') + '/' + isNull(dbo.xmlEncode('asp/meetings/createVCalForMeeting.asp?ID=' + @meetingID + ''),'NULLPATH') + '" />
	<obj type="text"><attribute name="value" value="Add to Calendar Only" /></obj>
</obj>
'

print 'getting my list to send emails'

--EXEC A_SP_MEETING_GET_INVITED_PEOPLE @meetingID,@strNTLogin 

print 'inside A_SP_MEETING_EMAIL_GET_INVITED_PEOPLE'
print 'Create the temp table'
CREATE TABLE #T (
ID varchar (50)
)

print 'insert the host'
INSERT INTO #T SELECT HOST FROM A_MEETINGS WHERE ID = @meetingID
select 'AFTER HOST' ,* from #T
print 'insert the SCRIBE'
INSERT INTO #T SELECT SCRIBE FROM A_MEETINGS WHERE ID = @meetingID
select 'AFTER SCRIBE' ,* from #T
print 'insert the TIME_KEEP'
INSERT INTO #T SELECT TIME_KEEP FROM A_MEETINGS WHERE ID = @meetingID
select 'AFTER TIME_KEEP' ,* from #T
print 'insert the People invited'
INSERT INTO #T SELECT PEOPLE_ID FROM A_MEETING_INV_PEOPLE WHERE MEETING_ID = @meetingID
select 'AFTER People inivited' ,* from #T
print 'insert the People invited through their roles'
INSERT INTO #T SELECT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS 
	WHERE ROLE_ID IN (SELECT ROLE_ID FROM A_MEETING_INV_ROLE WHERE MEETING_ID = @meetingID)
select 'AFTER roles people' ,* from #T
print 'insert the People invited through their companies'
INSERT INTO #T SELECT ID FROM A_V_PEOPLE_APPROVED_DATA 
	WHERE COMPANY IN (SELECT COMPANY_ID FROM A_MEETING_REALLY_INIVTED_COMPANY WHERE MEETING_ID = @meetingID)
select 'AFTER people in companies' ,* from #T
set @sql = 'SELECT DISTINCT ID FROM #T WHERE ID IS NOT NULL'  
declare @so varchar (8000)
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql, @so OUTPUT
print 'MY LIST777777777' + isNull(@so,'NULL')
exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
NULL,@so,@linkPath,@emailSubject,@emailBody,@strNTLogin
UPDATE A_MEETINGS SET DATE_EMAIL_SENT = getDate() WHERE ID = @meetingID