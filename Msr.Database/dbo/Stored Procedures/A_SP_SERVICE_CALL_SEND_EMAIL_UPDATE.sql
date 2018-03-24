
CREATE PROCEDURE DBO.A_SP_SERVICE_CALL_SEND_EMAIL_UPDATE
@ID varchar(50),
@actionTaken varchar(50),
@strNTLogin varchar(50)
AS
print 'Sending an email about service call = ' + isnull(@ID,'NULL') + ' where action = ' + isnull(@actionTaken,'NULL')
declare @linkPath varchar (1000), @emailSubject varchar(1000),@emailBody varchar(8000),
	@worker varchar(50),@boss varchar(50),@workerName varchar(500),@workWeekDate varchar(50),
	@sentBackBy varchar(50)

if @actionTaken = 'sentBackward' set @emailSubject = 'Service Call Sent Backwards'
else goto fin

select @linkPath = dbo.xmlEncode('asp/serviceCalls/searchServiceCall2.asp?startDate=' 
	+ convert(varchar(50),START_MONTH) + '-' 
	+ convert(varchar(50),START_DAY) + '-' 
	+  convert(varchar(50),START_YEAR)),
	@workWeekDate = convert(varchar(50),START_MONTH) + '-' + convert(varchar(50),START_DAY) + '-' 
	+  convert(varchar(50),START_YEAR),
	@worker = WORKER_ID
	FROM A_SERVICE_CALLS_WEEKLY_REPORTS WHERE ID = @ID

SELECT @boss = BOSS FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @worker
SELECT @workerName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @worker
SELECT @sentBackBy = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Time Card Start Date:" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(@workWeekDate,'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Worker Name" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@workerName),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Sent Back By:" /></obj>
		</obj>
		<obj type="col"><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@sentBackBy),'') + '" /></obj>
		</obj>
	</obj>
</obj>
'
declare @toList varchar(2000)
set @toList = isNull(@boss + ',','') + isNull(@worker,'')
exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
Null,@toList,@linkPath,@emailSubject,@emailBody,@strNTLogin
print'finished sending email about a service call'
fin: