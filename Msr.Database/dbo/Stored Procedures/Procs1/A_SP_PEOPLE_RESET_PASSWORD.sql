



/*
STORED PROCEDURE CALLED IN
Module: people/editPerson.asp
*/

CREATE           PROCEDURE A_SP_PEOPLE_RESET_PASSWORD
@objID nvarchar(50),
@PASSWORD nvarchar(100),
@strNTLogin nvarchar(50),
@realPass nvarchar(50)
AS
print 'checking to see if i am in table A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS'

declare @isNewUserTest varchar(50)
SELECT @isNewUserTest = ID 
FROM A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS
WHERE OBJECT_ID = @objID

if @isNewUserTest IS NOT NULL 
	begin
	print 'Update the table with the new password'
	UPDATE A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS
	SET PASSWORD = @realPass
	WHERE OBJECT_ID = @objID
	end 

declare @myRoot as varchar(50)
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @objID
UPDATE A_PEOPLE_HISTORY SET PASSWORD = @PASSWORD, CHANGE_PASS=1
WHERE ID in (SELECT OBJ_ID FROM A_OBJECTS WHERE ROOT = @myRoot) 


if @isNewUserTest IS NULL 
begin
declare @linkPath varchar (1000), @emailSubject varchar(1000),
	@emailBody varchar(8000)

set @linkPath = dbo.xmlEncode('asp/people/editMyAccount.asp')

set @emailSubject = 'ANSWER password reset'

declare 
	@setterName nvarchar(1000),
	@settieName nvarchar(1000)

SELECT @settieName = FULL_NAME 
	FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @myRoot
SELECT @setterName = FULL_NAME 
	FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

set @emailBody = '
<obj type="table">
	<obj type="row">
		<obj type="col"> 
			<obj type="text">
				<attribute name="value" value="password:" />
			</obj>
		</obj>
		<obj type="col">
			<obj type="text">
				<attribute name="value" value="' + isNull(@realPass,'NULL') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Set By:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(@setterName,'NULL') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"> 
			<obj type="text"><attribute name="value" value="Set For:" /></obj>
		</obj>
		<obj type="col">
			<obj type="text"><attribute name="value" value="' + isNull(@settieName,'NULL') + '" /><attribute name="dontUsePutText" value="true" /></obj>
		</obj>
	</obj>
	<obj type="row">
		<obj type="col"><attribute name="colspan" value="2" /><attribute name="style" value="vertical-align:top" />
			<obj type="text"><attribute name="value" value="Change your password message" /></obj>
		</obj>
	</obj>
</obj>
'
declare @strToList varchar(4000)
set @strToList = @myRoot
exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG_EXTENDED
	null,@strToList,@linkPath,@emailSubject,@emailBody,@strNTLogin
end





