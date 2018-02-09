

CREATE                                PROCEDURE A_SP_OBJECT_WF_FINISHED
@wfsID nvarchar(50),
@strNTLogin  nvarchar(50)
as
BEGIN TRANSACTION
print 'Entering A_SP_OBJECT_WF_FINISHED'
declare @myTable as nvarchar(50)
declare @myID as nvarchar(50)
declare @objID as nvarchar(50),@root varchar(50)
SELECT @objID = ID,@myTable = OBJ_TABLE,@myID = OBJ_ID,@root = ROOT 
	FROM A_OBJECTS WHERE WFS_ID = @wfsID

print 'MyTable = ' + @myTable
if @myTable = 'A_ROLES_HISTORY'
	begin
		print'Finishing Role WorkFlow'
		exec A_SP_ROLES_FINISH_APPROVAL_WF @myID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_PEOPLE_HISTORY'
	begin
		print'Finishing People WorkFlow'
		exec A_SP_PEOPLE_FINISH_APPROVAL_WF @myID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_LOCATIONS_HISTORY'
	begin
		print 'finishing the wf for A_LOCATIONS' + @myID
		exec A_SP_LOCATIONS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_REGIONS_HISTORY'
	begin
		print 'finishing the wf for A_REGIONS' + @myID
		exec A_SP_REGIONS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_COMPANIES_HISTORY'
	begin
		print 'finishing the wf for A_COMPANIES' + @myID
		exec A_SP_COMPANIES_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_PARTS_HISTORY'
	begin
		print 'finishing the wf for A_PARTS_HISTORY' + @myID
		exec A_SP_PARTS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_PART_TYPES_HISTORY'
	begin
		print 'finishing the wf for A_PART_TYPES_HISTORY' + @myID
		exec A_SP_PART_TYPES_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_TT_VERBS_HISTORY'
	begin
		print 'finishing the wf for A_TT_VERBS_HISTORY' + @myID
		exec A_SP_TT_VERBS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_PREPOP_HISTORY'
	begin
		print 'finishing the wf for A_PREPOP_HISTORY' + @myID
		exec A_SP_PREPOP_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end 
if @myTable = 'A_NOUN_HIERARCHIES_HISTORY'
	begin
		print 'Finishing the WF for A_NOUN_HIERARCHIES_HISTORY' + @myID
		exec A_SP_NOUN_HIERARCHY_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_PROCEDURES_HISTORY'
	begin
		print 'Finishing the WF for A_PROCEDURES_HISTORY' + @myID
		exec A_SP_PROCEDURES_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_THEORY_HISTORY'
	begin
		print 'Finishing the WF for A_THEORY_HISTORY' + @myID
		exec A_SP_THEORY_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_PRODUCTS_HISTORY'
	begin
		print 'Finishing the WF for A_PRODUCTS_HISTORY' + @myID
		exec A_SP_PRODUCTS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end

if @myTable = 'A_ACTUAL_PARTS_HISTORY'
	begin
		print 'Finishing the WF for A_ACTUAL_PARTS_HISTORY' + @myID
		exec A_SP_ACTUAL_PARTS_FINISH_WF @myID,@objID,@strNTLogin
		exec A_SP_ACTUAL_PARTS_UPDATE_SAFETY_LEVEL_DATA @root
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_COUNTERS_HISTORY'
	begin
		print 'Finishing the WF for A_COUNTERS_HISTORY' + @myID
		exec A_SP_COUNTERS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_PROD_PRICE_LIST_HISTORY'
	begin
		print 'Finishing the WF for A_PROD_PRICE_LIST_HISTORY' + @myID
		exec A_SP_PROD_PRICE_LIST_FINISH_APPROVAL_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_ACCOUNTS_HISTORY'
	begin
		print 'Finishing the WF for A_ACCOUNTS_HISTORY' + @myID
		exec A_SP_ACCOUNTS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_FORECASTS_HISTORY'
	begin
		print 'Finishing the WF for A_FORECASTS_HISTORY' + @myID
		exec A_SP_FORECASTS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_ORDERS_HISTORY'
	begin
		print 'Finishing the WF for A_ORDERS_HISTORY' + @myID
		exec A_SP_ORDERS_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_QUOTES_HISTORY' 	begin
		print 'Finishing the WF for A_QUOTES_HISTORY' + @myID
		exec A_SP_QUOTES_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
if @myTable = 'A_PURCHASES_HISTORY'
	begin
		print 'Finishing the WF for A_PURCHASES_HISTORY' + @myID
		exec A_SP_PURCHASES_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end

if @myTable = 'A_EQUIP_EXP_HISTORY'
	begin
		print 'Finishing the WF for A_EQUIP_EXP_HISTORY' + @myID
		exec A_SP_EQUIP_EXP_FINISH_WF @myID,@objID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end


declare @stat as varchar(50),
@finDate as varchar(50)
SELECT @stat = STATUS_ON_COMPLETION, @finDate = FINISHED_DATE 
FROM A_WORKFLOWS_STARTED WHERE ID = @wfsID
if @stat = 'APPROVED'
	begin
	UPDATE A_OBJECTS SET APPROVAL_DATE = @finDate WHERE ID = @objID
	end

if @strNTLogin <> (SELECT STARTED_BY FROM A_WORKFLOWS_STARTED WHERE ID = @wfsID)
	exec A_SP_OBJECT_WF_FINISHED_SEND_EMAIL @objID



fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_WF_FINISH_GROUP_STARTED with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_FINISH_GROUP_STARTED and we will terminate and not finish anything '
return 1










