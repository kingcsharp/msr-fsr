CREATE PROCEDURE dbo.A_SP_OBJECT_WF_FINISHED_SEND_EMAIL
@objID varchar(50)
AS
declare @objDesc varchar(500),@wfsID varchar(50),@wfsByID varchar(50),@wfsByName nvarchar(500),
@objStatus varchar(50),@rootID varchar(50),@startDate dateTime,@finDate dateTime,
@rev varchar(10),@revInfo nvarchar(2000)

SELECT @objDesc = OBJ_DESC, @wfsID = WFS_ID, @rootID = ROOT,@objStatus = STATUS,@rev = REV,
	@revInfo = REV_INFO
	FROM A_OBJECTS WHERE ID = @objID

SELECT @wfsByID = STARTED_BY,
	@startDate = dbo.A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON(STARTED_BY,START_DATE),
	@finDate = dbo.A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON(STARTED_BY,FINISHED_DATE)
FROM A_WORKFLOWS_STARTED 
WHERE ID = @wfsID

SELECT @wfsByName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @wfsByID


declare @peopleList as varchar(8000)
declare @linkPath varchar (1000)
declare @emailSubject varchar(1000) 
declare @emailBody varchar(8000)
set @peopleList = @wfsByID
set @linkPath = null
set @emailSubject = 'ANSWER Object Approved'

set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Object Description:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@objDesc,1100)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Revision Number:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull('Revision_' + dbo.xmlEncode(@rev),'') + '" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Rev Info" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull('' + dbo.xmlEncode(@revInfo),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Status:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull('' + dbo.xmlEncode(@objStatus),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Date Started:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@startDate),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Finish Date:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@finDate) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"><obj type="text"><attribute name="value" value="WF Started By:" /></obj></obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@wfsByName) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
</obj>'



exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@peopleList,@linkPath,@emailSubject,@emailBody,@wfsByID






