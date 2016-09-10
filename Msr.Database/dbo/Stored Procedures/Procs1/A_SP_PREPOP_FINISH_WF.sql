







CREATE      PROCEDURE A_SP_PREPOP_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a A_PREPOP'
declare @myRoot as nvarchar(50) --get the root which is the ID of A_PREPOP
declare @ID as nvarchar(50) --get my ID  in the A_PREPOP_HISTORY table
declare @creatingCo varchar(50),@STATUS varchar(50)
if @objID is null
	SELECT @objID = OBJECT_ID FROM A_PREPOP_HISTORY WHERE ID = @myID
SELECT @myRoot = ROOT,@ID = OBJ_ID,@creatingCo = CREATING_CO FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PREPOP WHERE ID = @myRoot 
if @tester is Null --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PREPOP(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_PREPOP is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PREPOP','HISTORY_REF_ID'
if @curID is null UPDATE A_PREPOP SET STATUS = 'DELETED',CREATING_CO = @creatingCo WHERE ID = @myRoot
else UPDATE A_PREPOP SET STATUS = 'APPROVED',CREATING_CO = @creatingCo WHERE ID = @myRoot









