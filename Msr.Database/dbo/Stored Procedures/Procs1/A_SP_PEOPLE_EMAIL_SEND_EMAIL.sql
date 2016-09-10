



CREATE             PROCEDURE DBO.A_SP_PEOPLE_EMAIL_SEND_EMAIL
@objID varchar(50),
@strNTLogin varchar(50)
AS
declare @linkPath varchar (1000), 
		@emailSubject varchar(1000),
		@emailBody varchar(8000),
		@sql varchar(1000),
		@personName varchar(100),
		@login varchar(100),
		@password varchar(100),
		@textToChangePassword varchar(4000)

print '@person id is' + isNull (@objID,'NULL')
SELECT 	@personName = FULL_NAME,	
		@login = LOGIN
FROM A_V_PEOPLE_APPROVED_DATA
WHERE ID = @objID

SELECT 	@password = [PASSWORD]	
FROM A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS
WHERE OBJECT_ID  = @objID

set @emailSubject = 'Welcome New Answer User'
set @linkPath = 'asp/people/editMyAccount.asp'
set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="New User Name:" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@personName),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="LOGIN:" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@login),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Password" /></obj>
		</obj>
		<obj type="col"> <attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="' + isNull(dbo.xmlEncode(@password),'') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"><attribute name="colspan" value="2" /><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Change your password message" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col">
			<obj type="text"><attribute name="value" value="" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="" /></obj>
		</obj>
	</obj>
</obj>
'

set @sql = 'SELECT ID FROM A_V_PEOPLE_APPROVED_DATA WHERE 
			ID = ''' + @objID + ''''  
		
declare @so varchar (8000)
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql, @so OUTPUT
exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
NULL,@so,@linkPath,@emailSubject,@emailBody,@strNTLogin

print 'sent email so now deleting this new person from A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS '

DELETE FROM A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS 
WHERE OBJECT_ID = @objID




