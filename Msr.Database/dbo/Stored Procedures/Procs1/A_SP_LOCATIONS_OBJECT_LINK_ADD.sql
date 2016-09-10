





CREATE    PROCEDURE A_SP_LOCATIONS_OBJECT_LINK_ADD
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
print 'executing procedure A_SP_LOCATIONS_OBJECT_LINK_ADD with obj = ' + @objID
declare @newID as nvarchar(50)
exec sp_GetUniqueID3 @newID OUTPUT
INSERT INTO A_LOCATIONS_OBJECT_LINK (ID,OBJECT_ID,DRCM,MODBY)
VALUES (@newID,@objID,getDate(),@strNTLogin)
print 'DONE executing procedure A_SP_LOCATIONS_OBJECT_LINK_ADD with obj = ' + @objID








