








CREATE      procedure A_SP_PART_TYPES_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_PART_TYPES_HISTORY (
			ID,
			NAME ,
			SPARE ,
			CONSUMABLE ,
			UNIT ,
			UNIT_SHIPPING_WEIGHT , 
			DRCM, 
			MODBY)
SELECT @newID as ID,
		@copyPrefix + NAME ,
		SPARE ,
		CONSUMABLE ,
		UNIT ,
		UNIT_SHIPPING_WEIGHT , 
		getDate(), 
		@strNTLogin FROM A_PART_TYPES_HISTORY WHERE ID = @strID

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_PART_TYPES_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID








