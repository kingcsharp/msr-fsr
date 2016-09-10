





CREATE       PROCEDURE DBO.A_SP_MESSAGES_SEND_MARK_AS_READ_EMAIL
@messageID varchar(50),
@strNTLogin varchar(50)
AS
declare @linkPath varchar (1000), 
		@emailSubject varchar(1000),
		@emailBody varchar(8000),
		@sql varchar(1000),
		@recipientName varchar(100),
		@message varchar(4000),
		@importance varchar(50)

SELECT @message = MESSAGE,
		@importance = IMPORTANCE
FROM A_V_MESSAGES_SEARCH_DATA
WHERE ID = @messageID

SELECT @recipientName = RECIPIENT_NAME
FROM A_V_MESSAGES_PEOPLE_LINK_WITH_NAMES
WHERE MESSAGE_ID = @messageID
and PERSON_ID = @strNTlogin

set @emailSubject = 'Message you wrote was read'
set @linkPath = dbo.xmlEncode('asp/messages/searchMessages.asp?ID=' + @messageID + '&ID_MATCH_EXACTLY=TRUE')
set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Recipient Name:" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@recipientName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="IMPORTANCE:" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@importance),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Message" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(left(@message,50)),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
</obj>
'

set @sql = 'SELECT SENDER FROM A_MESSAGES WHERE 
			ID = ''' + @messageId + ''''  
		
declare @so varchar (8000)
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql, @so OUTPUT
declare @combinedRequesteeList varchar(2000)
exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
NULL,@so,@linkPath,@emailSubject,@emailBody,@strNTLogin

