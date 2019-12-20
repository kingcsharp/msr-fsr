CREATE       procedure [dbo].[sp_GetUniqueID3]
@ID numeric OUTPUT
as
print 'Getting a unique ID'
DECLARE @NEWID INT;
SET @NEWID=(select max(ID) from A_UNIQUE_ID)+1
UPDATE A_UNIQUE_ID WITH (UPDLOCK, ROWLOCK)
SET ID = @NEWID;
SET @ID = @NEWID;
print @ID
return @ID






