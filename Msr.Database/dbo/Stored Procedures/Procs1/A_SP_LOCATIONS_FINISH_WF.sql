





CREATE     PROCEDURE A_SP_LOCATIONS_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a location'

declare @myObj as nvarchar(50)
declare @myRoot as nvarchar(50)
SELECT @myObj = OBJECT_ID FROM A_LOCATIONS_HISTORY WHERE ID = @myID
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @myObj
print 'Updating A_LOCATIONS to latest rev'
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_LOCATIONS WHERE ID = @myRoot 
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record in A_ROLES for this item'
		INSERT INTO A_LOCATIONS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)
	end

--so now make sure that the value in A_LOCATIONS is the latest
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_LOCATIONS','HISTORY_REF_ID'
if @curID is null UPDATE A_LOCATIONS SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_LOCATIONS SET STATUS = 'APPROVED' WHERE ID = @myRoot

declare @sql varchar(3000),@sID varchar(50)
set @sql = 'exec A_SP_LOCATIONS_COMPLETELY_UPDATE_CHILDREN_TABLE'
SELECT @sID = ID FROM A_ADMIN_SQL_TO_RUN WHERE CODE = @sql AND STATUS = 'WAITING'
declare @myDate datetime
set @myDate = dateAdd(n,30,getdate())
if @sID is null
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @sql,@myDate,@strNTLogin
else
	UPDATE A_ADMIN_SQL_TO_RUN SET RUN_AT_DATE = @myDate



