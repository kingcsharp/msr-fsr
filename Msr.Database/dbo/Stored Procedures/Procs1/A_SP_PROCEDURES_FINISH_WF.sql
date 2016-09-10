









CREATE                 PROCEDURE A_SP_PROCEDURES_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a Procedure with ObjectID = ' + isNull(@objID,'Fin Null')
if @myID is null
	SELECT @myID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @myRoot as nvarchar(50) --get the root which is the ID of A_VERBS
declare @ID as nvarchar(50) --get my ID  in the A_NOUN_HIERARCHIES_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PROCEDURES WHERE ID = @myRoot 
if @tester is Null --it is not in there so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PROCEDURES(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_NOUN_HIERARCHIES is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PROCEDURES','HISTORY_REF_ID'
if @curID is null UPDATE A_PROCEDURES SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_PROCEDURES SET STATUS = 'APPROVED' WHERE ID = @myRoot

print 'The Workflow is finished.  Now we need to create the E-Access Product if there is not one'
--exec A_SP_PROCEDURE_CREATE_E_ACCESS_PRODUCT @myRoot,@strNTLogin
print 'The Workflow is finished.  Now we need to create the Product if there is not one'
--exec A_SP_PROCEDURE_CREATE_FIRST_PRODUCT @myRoot,@strNTLogin
print 'Now that the workflow is finished we need to update all the products for this procedure'
exec A_SP_PROCEDURE_UPDATE_PRODUCT_SUB_PRICES @myRoot,@strNTLogin
print 'Now set the last mod date for each of the steps'
--exec A_SP_PROCEDURE_STEPS_UPDATE_LAST_MODIFIED_DATE @myID,@objID
declare @sql varchar(3000)
set @sql = 'exec A_SP_PROCEDURE_STEPS_CREATE_ALL_RELATED_STEPS ''' + @ID + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin

















