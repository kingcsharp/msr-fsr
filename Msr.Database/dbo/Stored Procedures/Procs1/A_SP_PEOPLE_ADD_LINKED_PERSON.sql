CREATE PROCEDURE dbo.A_SP_PEOPLE_ADD_LINKED_PERSON
@USER_ID varchar(50),
@PASSWORD nvarchar(50),
@PASSWORD_NON_ENCRYPT nvarchar(50),
@NICK_NAME varchar(50),
@strNTLogin varchar(50)
AS
declare @msg varchar(100)
print 'Linking'

if Exists(SELECT ID FROM A_ADMIN_CONFIGURATION WHERE NAME = 'ALL_PASS' AND VAL = @PASSWORD_NON_ENCRYPT)
	if not exists(SELECT ID,NAME,LAST_NAME,LOGIN 
					FROM A_APPROVED_PEOPLE 
					WHERE SYSTEM_STATUS = 'ACTIVE' AND LOGIN = @USER_ID)
		goto secondCheck
	else
		goto verified
secondCheck:
if exists (SELECT ID,NAME,LAST_NAME,LOGIN 
				FROM A_APPROVED_PEOPLE 
				WHERE SYSTEM_STATUS = 'ACTIVE' AND LOGIN = @USER_ID and PASSWORD = @PASSWORD)
	goto verified
else
	begin
	set @msg = 'ERROR - FAILED TO VERIFY'
	goto fin
	end

verified:
declare @linkedID varchar(50)
SELECT @linkedID = ID FROM A_APPROVED_PEOPLE WHERE SYSTEM_STATUS = 'ACTIVE' AND LOGIN = @USER_ID 
if @linkedID = @strNTLogin
	begin
	set @msg = 'ERROR - USER AND LINK THE SAME'
	goto fin
	end
if exists(SELECT * FROM A_PEOPLE_LINKED_LOGINS WHERE ROOT_ID = @strNTLogin and LINKED_ID = @linkedID)
	begin
	set @msg = 'LINK ALREADY EXISTS - NICKNAME Updated'
	UPDATE A_PEOPLE_LINKED_LOGINS SET NICK_NAME = @NICK_NAME WHERE ROOT_ID = @strNTLogin and LINKED_ID = @linkedID
	goto fin
	end
set @msg = 'Inserting a new one'
INSERT INTO A_PEOPLE_LINKED_LOGINS (ID,ROOT_ID,LINKED_ID,NICK_NAME,DRCM,MODBY)
	VALUES (
		newID(),
		@strNTLogin,
		@linkedID,
		@NICK_NAME,
		getDate(),
		@strNTLogin
		)


fin:
SELECT @msg as MSG
