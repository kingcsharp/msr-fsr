


CREATE   PROCEDURE DBO.A_SP_WF_GROUP_SEND_EMAILS_ABOUT_PENDING_APPROVAL
@gID varchar(50),
@strNTLogin varchar(50)
AS
declare @peopleList as varchar(8000),@sql varchar(4000)
set @sql = 'SELECT PERSON AS ID FROM A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS WHERE WFGS_ID = ' + @gID
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@peopleList OUTPUT
declare @linkPath varchar (1000)
declare @emailSubject varchar(1000) 
declare @emailBody varchar(8000)
set @linkPath = dbo.xmlEncode('asp/Approvals/approvalSearch.asp?searchMethod=person&people=11134&pageID=approvalListViewPage')
set @emailSubject = 'ANSWER Pending Approval Alert'
set @emailBody = ''

declare @objectID varchar(50),@wfsID varchar(50),
	@startedByID varchar(50),@startedByName nvarchar(500),
	@wfID varchar(50),@stageName varchar(50),@workflowName varchar(50),
	@OBJ_DESC varchar(50),@revInfo nvarchar(1000),@rev int,@statComplete varchar(50),
	@groupID varchar(50),@groupName nvarchar(200),@stageID varchar(50),@submitDate varchar(50)

SELECT @wfsID = WFS_ID, @groupID = WF_GROUP_ID,@stageID = WF_STAGE_ID FROM A_WORKFLOW_GROUP_STARTED WHERE ID = @gID
SELECT @groupName = NAME FROM A_WF_GROUPS WHERE ID = @groupID
SELECT @stageName = NAME FROM A_WF_STAGES WHERE ID = @stageID
SELECT @statComplete = STATUS_ON_COMPLETION,@startedByID = STARTED_BY, @wfID = WF_ID,@submitDate = START_DATE FROM A_WORKFLOWS_STARTED WHERE ID = @wfsID
SELECT @startedByName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @startedByID
SELECT @workflowName = NAME FROM A_WORKFLOWS WHERE ID = @wfID
SELECT @OBJ_DESC = OBJ_DESC,@objectID = ID,@REV = REV,@revInfo = REV_INFO FROM A_OBJECTS WHERE WFS_ID = @wfsID

set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Object to Approve:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@OBJ_DESC,1100)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Revision Number:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text">
				<attribute name="value" value="' + dbo.xmlEncode(isNull(@rev,'')) + '" />
				<attribute name="dontUsePutText" value="true" />
			</obj>
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
			<obj type="text"><attribute name="value" value="Status if approved all the way:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull('' + dbo.xmlEncode(@statComplete),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Submitted By:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@startedByName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="WorkFlow Name:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@workflowName) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"><obj type="text"><attribute name="value" value="WF Group Name:" /></obj></obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@groupName) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"><obj type="text"><attribute name="value" value="WF Stage Name:" /></obj></obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@stageName) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"><obj type="text"><attribute name="value" value="Submitted Date:" /></obj></obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@submitDate) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
</obj>'



exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@peopleList,@linkPath,@emailSubject,@emailBody,@strNTLogin





