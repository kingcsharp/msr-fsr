







CREATE    PROCEDURE A_SP_REGIONS_UPDATE_ONE_REGION
	@newObjID nvarchar(50) OUTPUT,
	@messages nvarchar(50) OUTPUT,
	@objID nvarchar(50),
	@NAME nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @myID as nvarchar(50)
if @objID is null
	begin
		print 'This is a new Region so inserting it now'
		exec sp_GetUniqueID3 @myID OUTPUT
		INSERT INTO A_REGIONS_HISTORY (ID,NAME,DRCM,MODBY)
		VALUES (@myID,@NAME,getDate(),@strNTLogin)
		SELECT @newObjID = OBJECT_ID FROM A_REGIONS_HISTORY WHERE ID = @myID 
	end
else
	begin
		print 'This is an update to the existing Region ' + @myID
		set @myID = @objID
		UPDATE A_REGIONS_HISTORY set NAME = @NAME,DRCM = getDate(),MODBY = @strNTLogin
		WHERE OBJECT_ID = @objID
		set @newObjID = @objID
	end








