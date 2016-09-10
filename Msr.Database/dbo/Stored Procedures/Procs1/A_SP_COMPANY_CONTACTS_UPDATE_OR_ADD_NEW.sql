

CREATE PROCEDURE dbo.A_SP_COMPANY_CONTACTS_UPDATE_OR_ADD_NEW
@newID varchar(50) OUTPUT,
@messages varchar(2000) OUTPUT,
@ID varchar(50),
@FIRST_NAME varchar(2000),
@LAST_NAME varchar(2000),
@PHONE varchar(50),
@CO_ID varchar(50),
@strNTLogin varchar(50)
AS
declare @creatingCo varchar(50)
SELECT @creatingCo = ROOT_COMPANY FROM A_V_PEOPLE_DATA_QUICK WHERE ID = @strNTlogin
if @ID is null
	begin
		print 'ID is Null we need to create this Customer Contact'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_COMPANY_CONTACTS(ID,FIRST_NAME,LAST_NAME,CREATING_CO,MODBY,DRCM) 
					VALUES(@newID,@FIRST_NAME,@LAST_NAME,@creatingCo,@strNTLogin,getDATE())
	end
else
	set @newID = @ID


UPDATE A_COMPANY_CONTACTS SET
FIRST_NAME = @FIRST_NAME,
LAST_NAME = @LAST_NAME,
COMPANY_ID = @CO_ID,
PHONE=@PHONE
WHERE ID = @newID



