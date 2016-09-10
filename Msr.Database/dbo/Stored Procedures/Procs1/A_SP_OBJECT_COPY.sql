




CREATE       procedure A_SP_OBJECT_COPY
	@returnObjID varchar(50) OUTPUT,
	@msg varchar(500) OUTPUT,
	@objID varchar(50),
	@copyPrefix varchar(50),
	@strNTLogin varchar(50)
as
declare @strTable as nvarchar(50)
declare @strID as nvarchar(50)
declare @newObjID as nvarchar(50)
print 'Copying object number ' + @objID
set @copyPrefix = isNull(@copyPrefix,'')

SELECT @strTable = OBJ_TABLE,@strID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
print 'The table is ' + @strTable + ' the ID is ' + @strID
if @strTable = 'A_ROLES_HISTORY'
	exec A_SP_ROLE_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin, @copyPrefix
if @strTable = 'A_PEOPLE_HISTORY'
	exec A_SP_PEOPLE_COPY_ONE @newObjID OUTPUT,@strID,@copyPrefix, @strNTLogin
if @strTable = 'A_COMPANIES_HISTORY'
	exec A_SP_COMPANIES_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_LOCATIONS_HISTORY'
	exec A_SP_LOCATIONS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_REGIONS_HISTORY'
	exec A_SP_REGIONS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_PARTS_HISTORY'
	exec A_SP_PARTS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_PART_TYPES_HISTORY'
	exec A_SP_PART_TYPES_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_TT_VERBS_HISTORY'
	exec A_SP_TT_VERBS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_PREPOP_HISTORY'
	exec A_SP_PREPOP_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_NOUN_HIERARCHIES_HISTORY'
	exec A_SP_NOUN_HIER_COPY_ONE @newObjID OUTPUT,@strID,@copyPrefix, @strNTLogin
if @strTable = 'A_PROCEDURES_HISTORY'
	exec A_SP_PROCEDURE_COPY_ONE @newObjID OUTPUT,@strID,@copyPrefix, @strNTLogin
if @strTable = 'A_PRODUCTS_HISTORY'
	exec A_SP_PRODUCTS_COPY_ONE @newObjID OUTPUT,@strID,@copyPrefix, @strNTLogin
if @strTable = 'A_ACTUAL_PARTS_HISTORY'
	exec A_SP_ACTUAL_PARTS_COPY_ONE @newObjID OUTPUT,@strID,@strNTLogin,@copyPrefix
if @strTable = 'A_COUNTERS_HISTORY'
	exec A_SP_COUNTERS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_PROD_PRICE_LIST_HISTORY'
	exec A_SP_PROD_PRICE_LIST_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_ACCOUNTS_HISTORY'
	exec A_SP_ACCOUNTS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_FORECASTS_HISTORY'
	exec A_SP_FORECASTS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
--if @strTable = 'A_ORDERS_HISTORY'
--	exec A_SP_ORDERS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
--if @strTable = 'A_QUOTES_HISTORY'
--	exec A_SP_QUOTES_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_EQUIP_EXP_HISTORY'
	exec A_SP_EQUIP_EXP_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_NEEDS_HISTORY'
	exec A_SP_NEEDS_COPY_ONE @newObjID OUTPUT,@strID, @strNTLogin,@copyPrefix
if @strTable = 'A_THEORY_HISTORY'
	exec A_SP_THEORY_COPY_ONE @newObjID OUTPUT,@strID, @copyPrefix, @strNTLogin
if @strTable = 'A_PROPOSALS_HISTORY'
	exec A_SP_PROPOSALS_COPY_ONE @newObjID OUTPUT,@strID, @copyPrefix, @strNTLogin

print 'Copying all Object Items'
exec A_SP_OBJECT_COPY_ALL_OBJECT_ITEMS @objID,@newObjID,@strNTLogin
print 'All Object Items Copied now Making this one Created'
exec A_SP_OBJECT_MAKE_CREATING @newObjID,@strNTLogin
print 'Finished making Created...   Now returning the objID of ' + @newObjID
set @returnObjID = @newObjID

























