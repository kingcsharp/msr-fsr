



CREATE                 PROCEDURE A_SP_ACTUAL_PARTS_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
begin transaction
print 'Finishing the wf for a A_ACTUAL_PARTS'
declare @rev int
declare @myRoot as nvarchar(50) --get the root which is the ID
declare @ID as nvarchar(50) --get my ID  in the A_PREPOP_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID,@rev = REV FROM A_OBJECTS WHERE ID = @objID
if @myRoot is null
	begin
	print 'ERROROROROR the root for objID = ' + isNull(@objID,'NULL') + ' COULD NOT BE FOUND '
	goto PROBLEM
	end
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_ACTUAL_PARTS WHERE ID = @myRoot 
if @tester is Null --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_ACTUAL_PARTS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_PREPOP is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_ACTUAL_PARTS','HISTORY_REF_ID'
if @@ERROR <> 0 goto problem
if @curID is null UPDATE A_ACTUAL_PARTS SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_ACTUAL_PARTS SET STATUS = 'APPROVED' WHERE ID = @myRoot


declare @subPartAction varchar(10)
if @rev = 1
	begin
	print '%%%%%%%%%%%%%%%%deciding what to do'
	SELECT @subPartAction = SUB_PART_ACTION from A_ACTUAL_PARTS_HISTORY WHERE ID = @ID
	if @subPartAction = 'GET_AT_LOC'
		begin
		print 'Getting from a location'
		exec A_SP_ACTUAL_PARTS_GRAB_CHILDREN_PARTS @myRoot,@strNTLogin
		end
	else
		begin
		exec A_SP_ACTUAL_PARTS_CREATE_CHILDREN_FROM_SOURCE_PART @myRoot ,@strNTLogin
		end

	end
declare @sql varchar(2000)
set @sql = 'exec A_SP_ACTUAL_PARTS_UPDATE_SAFETY_LEVEL_DATA ''' + @myRoot + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin



fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_ACTUAL_PARTS_FINISH_WF with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ACTUAL_PARTS_FINISH_WF and we will terminate and not finish anything '
return 1















