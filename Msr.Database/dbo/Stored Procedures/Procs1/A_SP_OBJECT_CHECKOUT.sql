









CREATE                   procedure A_SP_OBJECT_CHECKOUT
	@newObjID nvarchar(50) OUTPUT,
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
--declare @strTable as nvarchar(50)
--declare @strID as nvarchar(50)
declare @msg as nvarchar(50)

--SELECT @strTable = OBJ_TABLE,@strID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
--print 'The table is ' + @strTable + ' the ID is ' + @strID
--if @strTable = 'A_ROLES_HISTORY'
--	exec A_SP_ROLE_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_PEOPLE_HISTORY'
--	exec A_SP_PEOPLE_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_LOCATIONS_HISTORY'
--	exec A_SP_LOCATIONS_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_REGIONS_HISTORY'
--	exec A_SP_REGIONS_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_COMPANIES_HISTORY'
--	exec A_SP_COMPANIES_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_PARTS_HISTORY'
--	exec A_SP_PARTS_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_PART_TYPES_HISTORY'
--	exec A_SP_PART_TYPES_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--if @strTable = 'A_TT_VERBS_HISTORY'
--	exec A_SP_TT_VERBS_CHECKOUT   @newObjID OUTPUT, @strID, @strNTLogin
--exec A_SP_FILES_COPY_FROM_ONE_OBJ_TO_NEXT @objID,@newObjID,@strNTLogin

exec A_SP_OBJECT_COPY @newObjID OUTPUT,@msg OUTPUT,@objID,null,@strNTLogin
print 'Copied the Object and got back an objID of ' + isNull(@newObjID,'NULL')
print 'Finishing Checkout now with objID = ' + @objID + ' and new ObjID = ' + @newObjID
exec A_SP_OBJECT_FINISH_CHECKOUT @objID,@newObjid,@strNTLogin

print 'Finished Checking out an object and the new ID is ' + isNull(@newObjID,'NULL')






















