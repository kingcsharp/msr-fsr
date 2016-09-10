









CREATE  PROCEDURE dbo.A_SP_PROPOSALS_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a Proposal'
print 'Updating all the links for the object ' + @objID
declare @myRoot as nvarchar(50)
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @objID
print 'The Root is ' + @myRoot
declare @tester as nvarchar(50)
SELECT @tester = ID FROM A_PROPOSALS WHERE ID = @myRoot --it is not so add it
if @tester is Null
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PROPOSALS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES 
		(@myRoot,@myID,getDate(),@strNTLogin)
	end

declare @curCo as nvarchar(50)
declare @curCoName as nvarchar(50)
SELECT @curCo = OBJ_ID,@curCoName = OBJ_DESC FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curCo,@myRoot,'A_PROPOSALS','HISTORY_REF_ID'
if @curCo is null UPDATE A_PROPOSALS SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_PROPOSALS SET STATUS = 'APPROVED' WHERE ID = @myRoot

if @curCoName is Null
	DELETE FROM A_Z_DROP_DOWN_POPULATOR WHERE VAL = @myRoot
else
	UPDATE A_Z_DROP_DOWN_POPULATOR SET SHOW = @curCoName WHERE VAL = @myRoot
UPDATE A_OBJECTS SET CREATING_CO_NAME = 
	(SELECT NAME FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = A_OBJECTS.CREATING_CO)
	WHERE CREATING_CO = @myRoot





