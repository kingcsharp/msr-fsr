









CREATE        PROCEDURE A_SP_LOCATIONS_UPDATE_ALL_DATA_FOR_ONE
@ID as NVARCHAR(50)
AS
declare @myFullAddress as nvarchar(200)
declare @myParentPAth as nvarchar(200)
declare @parentName as nvarchar(100)
declare @regionName as nvarchar(100)

exec A_SP_LOCATIONS_GET_FULL_ADDRESS @myFullAddress OUTPUT,@ID
exec A_SP_LOCATIONS_GET_PARENT_PATH @myParentPath OUTPUT,@ID
select @parentName = NAME FROM A_APPROVED_LOCATIONS WHERE ID IN (SELECT PARENT_LOCATION FROM A_LOCATIONS_HISTORY WHERE ID = @ID)
select @regionName = NAME FROM A_APPROVED_REGIONS WHERE ID IN (SELECT REGION FROM A_LOCATIONS_HISTORY WHERE ID = @ID)

print 'updating the full addy to ' + isNull(@myFullAddress,'NULL')
print 'updating the full Path to ' + isNull(@myParentPath,'NULL')
print 'updating the parent name to ' + isNull(@parentName,'NULL')
print 'updating the region name to ' + isNull(@regionName,'NULL')

if (select PARENT_PATH FROM A_LOCATIONS_HISTORY WHERE ID = @ID) != @myParentPath
	begin
	print 'Updating the parent path'
	UPDATE A_LOCATIONS_HISTORY SET PARENT_PATH = @myParentPath WHERE ID = @ID
	end
if (select FULL_ADDRESS FROM A_LOCATIONS_HISTORY WHERE ID = @ID) != @myFullAddress
	begin
	print 'Updating the FULL_ADDRESS'
	UPDATE A_LOCATIONS_HISTORY SET FULL_ADDRESS = @myFullAddress WHERE ID = @ID
	end
if (select PARENT_LOCATION_NAME FROM A_LOCATIONS_HISTORY WHERE ID = @ID) != @parentName
	begin
	print 'Updating the PARENT_LOCATION_NAME'
	UPDATE PARENT_LOCATION_NAME SET FULL_ADDRESS = @parentName WHERE ID = @ID
	end
if (select REGION_NAME FROM A_LOCATIONS_HISTORY WHERE ID = @ID) != @regionName
	begin
	print 'Updating the REGION_NAME'
	UPDATE REGION_NAME SET FULL_ADDRESS = @regionName WHERE ID = @ID
	end

print 'Done with A_SP_LOCATIONS_UPDATE_ALL_DATA_FOR_ONE'



















