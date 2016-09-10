



CREATE  procedure A_SP_LOCATIONS_GET_BY_OBJECT_ID
	@objID nvarchar(50)
	
as
print 'Getting all Locations for ' + @objID
SELECT * FROM A_LOCATIONS_OBJECT_LINK WHERE OBJECT_ID = @objID




