




CREATE   procedure A_SP_LOCATIONS_OBJECT_LINK_UPDATE
	@LOCATION_ID nvarchar(50),
	@LOCATION_TYPE nvarchar(50),
	@ID nvarchar(50),
	@strNTLogin  nvarchar(50)
as
print 'Updating the LocObjLink number with ID = ' + @ID
UPDATE A_LOCATIONS_OBJECT_LINK SET
LOCATION_ID = @LOCATION_ID,
LOCATION_TYPE = @LOCATION_TYPE,
MODBY = @strNTLogin,
DRCM = getDate() WHERE ID = @ID







