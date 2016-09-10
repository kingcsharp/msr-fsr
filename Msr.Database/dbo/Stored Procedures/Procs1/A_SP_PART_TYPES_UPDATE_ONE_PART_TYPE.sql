

CREATE    PROCEDURE A_SP_PART_TYPES_UPDATE_ONE_PART_TYPE
@newObjID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@objID varchar(50),
@NAME nvarchar(4000),
@SPARE  nvarchar(50),
@CONSUMABLE  nvarchar(50),
@UNIT  nvarchar(50),
@UNIT_SHIPPING_WEIGHT  nvarchar(50),
@strNTLogin varchar(50)
AS

print 'Saving Part Part Type'

if not(@objID is null)
begin
	set @messages = @messages + 'The objId is not null it = ' + @objID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_PART_TYPES_HISTORY WHERE OBJECT_ID = @objID 
	if @tester is null
		print 'Error cannot find the Part Type ' + @objID + ' to update IT '
	else
		begin
			print 'Updating the part Type whose objID = ' + @objID
			UPDATE A_PART_TYPES_HISTORY SET
			DRCM = getDate(),
			MODBY = @strNTLogin,
			NAME = @NAME ,
			SPARE = @SPARE  ,
			CONSUMABLE = @CONSUMABLE  ,
			UNIT = @UNIT  ,
			UNIT_SHIPPING_WEIGHT = @UNIT_SHIPPING_WEIGHT
			WHERE OBJECT_ID = @objID
		end
	set @newObjID = @objID
end
else
	begin
		print 'The object ID is null'
		declare @newID as nvarchar(50)
		exec sp_getUniqueID3 @newID OUTPUT
		print 'The new ID is ' + isnull(@newID,'NULL??')
		INSERT INTO A_PART_TYPES_HISTORY (
			ID,
			NAME ,
			SPARE ,
			CONSUMABLE ,
			UNIT ,
			UNIT_SHIPPING_WEIGHT , 
			DRCM, 
			MODBY) VALUES(
			@newID ,
			@NAME,
			@SPARE,
			@CONSUMABLE,
			@UNIT,
			@UNIT_SHIPPING_WEIGHT,
			getDate(),
			@strNTLogin)
		print 'Inserted'
		declare @myObjID as nvarchar(50)
		SELECT @myObjId = OBJECT_ID FROM A_PART_TYPES_HISTORY WHERE ID = @newID
		set @messages = @messages + 'Making the objectID creating ' + @myObjID
		exec A_SP_OBJECT_MAKE_CREATING @myObjID,@strNTLogin
		set @messages = @messages + 'Done making it Creating ' + @strNTLogin
		select @newID as ID,@myObjID as OBJECT_ID
		set @newObjId = @myObjId
	end
