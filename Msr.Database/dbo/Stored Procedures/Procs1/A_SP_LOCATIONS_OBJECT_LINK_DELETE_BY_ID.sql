



CREATE  procedure A_SP_LOCATIONS_OBJECT_LINK_DELETE_BY_ID
	@ID nvarchar(50)
as
print 'Deleting the Location with ID = ' + @ID
DELETE FROM A_LOCATIONS_OBJECT_LINK WHERE ID = @ID






