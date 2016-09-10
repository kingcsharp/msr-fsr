




CREATE     procedure A_SP_OBJECT_UNLOCK_AND_DELETE
@objID nvarchar(50),
@strNTLogin nvarchar(50)
as
BEGIN TRANSACTION
declare @strTable as nvarchar(50)
declare @strID as nvarchar(50)
SELECT @strTable = OBJ_TABLE,@strID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
print 'The table is ' + @strTable + ' the ID is ' + @strID
if @strTable = 'A_ROLES_HISTORY'
	exec A_SP_ROLE_DELETE @strID, @strNTLogin
if @strTable = 'A_PEOPLE_HISTORY'
	exec A_SP_PEOPLE_DELETE @strID, @strNTLogin
if @strTable = 'A_LOCATIONS'
	exec A_SP_LOCATIONS_DELETE @strID, @strNTLogin
if @strTable = 'A_COMPANIES_HISTORY'
	exec A_SP_COMPANIES_DELETE @strID, @strNTLogin
if @strTable = 'A_PARTS_HISTORY'
	exec A_SP_PARTS_DELETE @strID, @strNTLogin
if @strTable = 'A_TT_VERBS_HISTORY'
	exec A_SP_TT_VERBS_DELETE @strID, @strNTLogin
if @strTable = 'A_PREPOP_HISTORY'
	exec A_SP_PREPOP_DELETE @strID, @strNTLogin
if @strTable = 'A_NOUN_HIERARCHIES_HISTORY'
	exec A_SP_NOUN_HIER_DELETE @strID, @strNTLogin
if @strTable = 'A_PROCEDURES_HISTORY'
	begin
	print'Its a procedure'
	exec A_SP_PROCEDURE_DELETE @strID, @strNTLogin
	end
if @strTable = 'A_THEORY_HISTORY'
	begin
	print'Its a theory'
	exec A_SP_THEORY_DELETE @strID, @strNTLogin
	end
if @strTable = 'A_PRODUCTS_HISTORY'
	exec A_SP_PRODUCTS_DELETE @strID, @strNTLogin
if @strTable = 'A_ACTUAL_PARTS_HISTORY'
	exec A_SP_ACTUAL_PARTS_HISTORY_DELETE @strID, @strNTLogin
if @strTable = 'A_COUNTERS_HISTORY'
	exec A_SP_COUNTERS_HISTORY_DELETE @strID, @strNTLogin
if @strTable = 'A_PROD_PRICE_LIST_HISTORY'
	exec A_SP_PROD_PRICE_LIST_DELETE @strID, @strNTLogin
if @strTable = 'A_ACCOUNTS_HISTORY'
	exec A_SP_ACCOUNTS_DELETE @strID, @strNTLogin
if @strTable = 'A_FORECASTS_HISTORY'
	exec A_SP_FORECASTS_DELETE @strID, @strNTLogin
if @strTable = 'A_QUOTES_HISTORY'
	exec A_SP_QUOTES_DELETE @strID, @strNTLogin
if @strTable = 'A_EQUIP_EXP_HISTORY'
	exec A_SP_EQUIP_EXP_DELETE @strID, @strNTLogin
if @strTable = 'A_NEEDS_HISTORY'
	exec A_SP_NEEDS_DELETE @strID, @strNTLogin
if @strTable = 'A_PURCHASES_HISTORY'
	exec A_SP_PURCHASE_DELETE @strID, @strNTLogin

print 'Finished updating the source Table'
if @@ERROR <> 0 goto problem

exec A_SP_OBJECT_RELEASE_OLD_REVS @objID,@strNTLogin
if @@ERROR <> 0 goto problem
DELETE FROM A_OBJECTS WHERE ID = @objID
if @@ERROR <> 0 goto problem

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_OBJECT_UNLOCK_AND_DELETE with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_OBJECT_UNLOCK_AND_DELETE and we will terminate and not finish anything '
return 1























