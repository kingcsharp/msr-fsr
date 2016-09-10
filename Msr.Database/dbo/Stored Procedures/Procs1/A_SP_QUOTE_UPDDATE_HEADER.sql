


CREATE    PROCEDURE dbo.A_SP_QUOTE_UPDDATE_HEADER
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID varchar(50),
@objID varchar(50),
@DESCRIPTION nvarchar(2000),
@EXPIRATION_DATE dateTime,
@strNTLogin varchar(50)

AS

print 'Updating a Quote'
if @objID is null
	begin
		print 'ID is Null we need to create this Account'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_QUOTES_HISTORY(ID,MODBY,DRCM) VALUES(@newID,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_QUOTES_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Quote objectID='+@objID
		set @newID = @objID
	end

print'Updating the Account history Data'
UPDATE A_QUOTES_HISTORY SET
DESCRIPTION = @DESCRIPTION,
EXPIRATION_DATE = @EXPIRATION_DATE,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE OBJECT_ID = @newID


