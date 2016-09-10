





CREATE    PROCEDURE A_SP_COUNTERS_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a counter'

declare @myObj as nvarchar(50)
declare @myRoot as nvarchar(50)
SELECT @myObj = OBJECT_ID FROM A_COUNTERS_HISTORY WHERE ID = @myID
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @myObj
print 'Updating A_LOCATIONS to latest rev'
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_COUNTERS WHERE ID = @myRoot 
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record in A_COUNTERS for this item'
		INSERT INTO A_COUNTERS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)
	end

--so now make sure that the value in A_LOCATIONS is the latest
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_COUNTERS','HISTORY_REF_ID'

--Now Make sure all the records in the counters recordings are set to the latest approved one as well
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_COUNTERS_RECORDINGS','COUNTER'






