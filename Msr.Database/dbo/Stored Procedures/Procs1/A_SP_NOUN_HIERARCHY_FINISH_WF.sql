








CREATE       PROCEDURE A_SP_NOUN_HIERARCHY_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a Noun Hierarchy'
declare @myRoot as nvarchar(50) --get the root which is the ID of A_VERBS
declare @ID as nvarchar(50) --get my ID  in the A_NOUN_HIERARCHIES_HISTORY table
SELECT @myRoot = ROOT,@ID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_NOUN_HIERARCHIES WHERE ID = @myRoot 
if @tester is Null --it is not in there so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_NOUN_HIERARCHIES(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@ID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_NOUN_HIERARCHIES is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_NOUN_HIERARCHIES','HISTORY_REF_ID'
--Now we need to delete and copy all the children information

--Delete the old Hierarchy in A_NOUN_HIERARCHY_CHILDREN
DELETE FROM A_NOUN_HIERARCHY_CHILDREN WHERE HIERARCHY_ID = @myROOT
--Now put the current Approved Hierarchies Children in here.
INSERT INTO A_NOUN_HIERARCHY_CHILDREN
(ID,ROOT_ID,APPROVED_OBJ_ID,PARENT_ID,HIERARCHY_ID,DRCM,MODBY,HIDDEN)
SELECT ID,ROOT_ID,APPROVED_OBJ_ID,PARENT_ID,@myROOT as HIERARCHY_ID,
DRCM,MODBY,HIDDEN FROM A_NOUN_HIERARCHY_CHILDREN_EDITING WHERE HIERARCHY_ID = @curID
--Now make those things in editing children been Approved
UPDATE A_NOUN_HIERARCHY_CHILDREN_EDITING SET BEEN_APPROVED = 'TRUE' WHERE HIERARCHY_ID = @curID












