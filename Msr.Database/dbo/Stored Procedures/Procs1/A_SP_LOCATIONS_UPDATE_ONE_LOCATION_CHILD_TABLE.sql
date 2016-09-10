


CREATE    PROCEDURE dbo.A_SP_LOCATIONS_UPDATE_ONE_LOCATION_CHILD_TABLE
@ID varchar(50)
AS
print 'First Delete wherever the id is a child  ' + @ID
DELETE FROM A_LOCATIONS_CHILD_LOOKUP WHERE CHILD_LOC = @ID
print 'fixing to run through the list and add all the parents to the top for id = ' + @ID
declare @curLocation as varchar(50)
select @curLocation = PARENT_LOCATION FROM A_LOCATIONS_APPROVED_DATA WHERE ID = @ID
while @curLocation is not null
	begin
	INSERT INTO A_LOCATIONS_CHILD_LOOKUP (ID,LOCATION,CHILD_LOC)
	VALUES (newID(),@curLocation,@ID)
	exec A_SP_LOCATIONS_UPDATE_ONE_LOCATION_CHILD_TABLE @curLocation
	SELECT @curLocation = PARENT_LOCATION FROM A_LOCATIONS_APPROVED_DATA WHERE ID = @curLocation
	end



