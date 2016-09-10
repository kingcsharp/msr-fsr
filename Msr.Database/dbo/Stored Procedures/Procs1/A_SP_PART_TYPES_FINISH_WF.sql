




CREATE   PROCEDURE A_SP_PART_TYPES_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a Part Type'
--We need to make sure that we got this part in the A_PART_TYPES TAble
declare @myRoot as nvarchar(50) --get the root which is the ID of A_PART_TYPES
declare @ID as nvarchar(50) --get my ID  in the A_PART_TYPES_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PART_TYPES WHERE ID = @myRoot 
print 'got the data i needed'
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PART_TYPES(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_PART_TYPES is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_PART_TYPES','HISTORY_REF_ID'





