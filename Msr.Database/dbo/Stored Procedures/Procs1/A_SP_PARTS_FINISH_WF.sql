








CREATE        PROCEDURE A_SP_PARTS_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
BEGIN TRANSACTION
print 'Finishing the wf for a Part'
--We need to make sure that we got this part in the A_PARTS TAble
declare @myRoot as nvarchar(50) --get the root which is the ID of A_PARTS
declare @ID as nvarchar(50) --get my ID  in the A_PARTS_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PARTS WHERE ID = @myRoot --it is not so add it
print 'got the data i needed'
if @tester is Null
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PARTS(ID,PARTS_HISTORY_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
		if @@ERROR <> 0 goto problem
	end
--so now make sure that the value in A_PARTS is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PARTS','PARTS_HISTORY_ID'
if @@ERROR <> 0 goto problem
--exec A_SP_PARTS_CREATE_AUTO_PRODUCT @objID,@strNTlogin
if @@ERROR <> 0 goto problem

declare @curPart as varchar(50),@creatingCo varchar(50)
SELECT @curPart = OBJ_ID,@creatingCo = CREATING_CO 
	FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS LIKE 'APPROVED%'
if @curPart is null UPDATE A_PARTS SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_PARTS SET STATUS = 'APPROVED',CREATING_CO = @creatingCo WHERE ID = @myRoot

declare @makeProd tinyInt,@prodType varchar(50)
SELECT @makeProd = CREATE_PROD,@prodType = PRODUCT_TYPE FROM A_PARTS_HISTORY WHERE OBJECT_ID = @objID
if @makeProd = 1
	begin
	if @prodType = 'GOOD'
		exec A_SP_PARTS_CREATE_AUTO_PRODUCT @objID,@strNTlogin
	else
		exec A_SP_PART_CREATE_SERVICE_PRODUCT @objID,@strNTlogin
	end

declare @curApprovedObj as varchar(50)
SELECT @curApprovedObj = ID FROM A_OBJECTS WHERE ROOT = @myRoot AND STATUS LIKE 'APPROVED%'
IF @curApprovedObj is null 
	UPDATE A_PARTS_SAFETY_STOCK_LEVELS SET STATUS = 'OLD' WHERE PART_ID = @myRoot 
else
	begin
	UPDATE A_PARTS_SAFETY_STOCK_LEVELS SET STATUS = 'OLD' WHERE PART_ID = @myRoot AND PART_OBJ_ID <> @curApprovedObj
	UPDATE A_PARTS_SAFETY_STOCK_LEVELS SET STATUS = 'APPROVED' WHERE PART_ID = @myRoot AND PART_OBJ_ID = @curApprovedObj
	end








fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_PARTS_FINISH_WF with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_PARTS_FINISH_WF and we will terminate and not finish anything '
return 1







