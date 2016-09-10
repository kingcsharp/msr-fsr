







CREATE     procedure A_SP_LOCATIONS_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_LOCATIONS_HISTORY ([ID], [NAME], [PARENT_LOCATION], [PARENT_LOCATION_NAME], [ADDRESS_1], [ADDRESS_2], [FULL_ADDRESS], [CITY], [STATE], [COUNTRY], [POSTAL_CODE], [REGION], [REGION_NAME], [INTERNAL_ADDRESS], [DRCM], [MODBY])
SELECT @newID as ID, @copyPrefix + [NAME], [PARENT_LOCATION], [PARENT_LOCATION_NAME], [ADDRESS_1], [ADDRESS_2], [FULL_ADDRESS], [CITY], [STATE], [COUNTRY], [POSTAL_CODE], [REGION], [REGION_NAME], [INTERNAL_ADDRESS], getDate() as DRCM, @strNTLogin as MODBY
FROM A_LOCATIONS_HISTORY WHERE ID = @strID

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_LOCATIONS_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID







