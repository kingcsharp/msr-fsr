









CREATE                 PROCEDURE dbo.A_SP_COMPANIES_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a Company'
print 'Updating all the links for the object ' + @objID
declare @myRoot as nvarchar(50)
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @objID
print 'The Root is ' + @myRoot
declare @tester as nvarchar(50)
SELECT @tester = ID FROM A_COMPANIES WHERE ID = @myRoot --it is not so add it
if @tester is Null
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_COMPANIES(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES 
		(@myRoot,@myID,getDate(),@strNTLogin)
	end

declare @curCo as nvarchar(50)
declare @curCoName as nvarchar(50)
SELECT @curCo = OBJ_ID,@curCoName = OBJ_DESC FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curCo,@myRoot,'A_COMPANIES','HISTORY_REF_ID'
if @curCo is null UPDATE A_COMPANIES SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_COMPANIES SET STATUS = 'APPROVED', NAME = @curCoName WHERE ID = @myRoot
exec A_SP_COMPANIES_REASSIGN_CREATING_CO_AND_ROOT_CO @objID,@strNTLogin
exec A_SP_COMPANIES_CREATE_NEW_ROOT_COMPANY @objID,@strNTLogin
declare @sql varchar(2000)
set @sql = 'exec A_SP_COMPANIES_COMPLETELY_UPDATE_CHILDREN_TABLE'
declare @myDate datetime
set @myDate = dateAdd(n,30,getDate())
if not exists(SELECT * FROM A_ADMIN_SQL_TO_RUN WHERE CODE = @sql AND STATUS IN ('QUED','WAITING'))
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @sql,@myDate,@strNTLogin

--exec A_SP_COMPANIES_UPDATE_COMPANY_FULL_NAME_TABLE @myRoot

print 'Checking if this is a root company then we need to make it its own creator'
declare @parentID varchar(50)
SELECT @parentID = PARENT FROM A_COMPANIES_HISTORY WHERE ID = @curCo
if @parentID is null
	begin
	print 'this is a root company, so just make sure they are their own creator'
	UPDATE A_OBJECTS SET CREATING_CO = ROOT WHERE ROOT = @myROOT
	
	
	
	end

UPDATE A_PEOPLE_SEARCH_TABLE SET COMPANY_NAME = @curCoName WHERE COMPANY_ID = @myRoot
if @curCoName is Null
	DELETE FROM A_Z_DROP_DOWN_POPULATOR WHERE VAL = @myRoot
else
	UPDATE A_Z_DROP_DOWN_POPULATOR SET SHOW = @curCoName WHERE VAL = @myRoot
UPDATE A_OBJECTS SET CREATING_CO_NAME = 
	(SELECT NAME FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = A_OBJECTS.CREATING_CO)
	WHERE CREATING_CO = @myRoot





