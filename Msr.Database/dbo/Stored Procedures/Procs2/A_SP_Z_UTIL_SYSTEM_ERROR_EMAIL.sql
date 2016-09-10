CREATE PROCEDURE DBO.A_SP_Z_UTIL_SYSTEM_ERROR_EMAIL
@errorData varchar(8000),
@pageName varchar(200),
@strNTLogin varchar(50)
AS
declare @emailsToSendTo varchar(8000)
print 'Sending an Error Message Email'
SELECT @emailsToSendTo = VAL FROM A_ADMIN_CONFIGURATION WHERE NAME = 'ERROR_EMAIL'
print 'The Email list is = ' + @emailsToSendTo

declare @personName varchar(50)
SELECT @personName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

declare @emailBody varchar(8000)

set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Error Page" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@pageName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Date" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(getDate()),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Error Text" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@errorData) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="Person" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@personName) + ' ','') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
</obj>
'

print 'The Email body = ' + @emailBody


exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
null,
@emailsToSendTo,
null,
'Error in ANSWER Software',
@emailBody,
@strNTLogin






