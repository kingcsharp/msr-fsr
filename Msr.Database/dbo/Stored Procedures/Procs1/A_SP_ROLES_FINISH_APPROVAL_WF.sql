




CREATE           PROCEDURE A_SP_ROLES_FINISH_APPROVAL_WF 
@myID nvarchar(50),
@strNTLogin nvarchar(50)
as
print 'Finishing the Role Approval WF'
declare @myObj as nvarchar(50)
declare @myStatus as nvarchar(50)
declare @myRoot as nvarchar(50)
SELECT @myObj = OBJECT_ID FROM A_ROLES_HISTORY WHERE ID = @myID
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @myObj
declare @curs as Cursor
declare @thisID as nvarchar(50)
declare @thisObjID as nvarchar(50)
print 'The Root is ' + @myRoot
set @curs = Cursor for SELECT OBJ_ID,ID FROM A_OBJECTS WHERE ROOT = @myRoot
open @curs
Fetch NEXT from @curs Into @thisObjID,@thisID
while (@@fetch_status = 0)
	begin
		print 'Updating object # ' + @thisObjID
		SELECT @myStatus = STATUS FROM A_OBJECTS WHERE ID = @thisID
		declare @raStat as nvarchar(50)
		set @raStat = NULL
		if @myStatus = 'APPROVED'
			set @raStat = 'ACTIVE'
		if @myStatus = 'APPROVED_BUT_REVISING'
			set @raStat = 'ACTIVE'
		if @myStatus = 'APPROVED_BUT_DELETING'
			set @raStat = 'ACTIVE'
		if @myStatus = 'DELETED'
			set @raStat = 'INACTIVE'
		if @myStatus = 'OLD'
			set @raStat = 'INACTIVE'
		print 'The new RA Stat is ' + @raStat
		UPDATE A_ROLE_ASSIGNEE SET STATUS = @raStat WHERE ROLE = @thisObjID
		print 'updated the status of role assignees of this role'
		Fetch NEXT from @curs Into @thisObjID,@thisID
	end
print 'Updating A_ROLES to latest rev'
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_ROLES WHERE ID = @myRoot 
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record in A_ROLES for this item'
		INSERT INTO A_ROLES(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_ROLES is the latest
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_ROLES','HISTORY_REF_ID'


if @curID is null UPDATE A_ROLES SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_ROLES SET STATUS = 'APPROVED' WHERE ID = @myRoot



print 'get my role id and name to update the A_PEOPLE_SEARCH_TABLE'

declare @roleName varchar(50)

SELECT @roleName = NAME
FROM A_V_ROLE_DATA_BY_APPROVED_ID
WHERE ID =@myRoot

UPDATE A_PEOPLE_SEARCH_TABLE 
SET POSITION_NAME = @roleName
WHERE POSITION_ID =  @myRoot

INSERT INTO A_ROLE_ASSIGNEE (ID,ROLE,STATUS,SOURCE,ROLE_ASSIGNED,DRCM,MODBY)
	SELECT newID(),@myID,'ACTIVE','DEPTSUBROLES',SUB_ROLE,getDate(),@strNTLogin 
	FROM A_ROLES_DEPARTMENT_SUB_ROLES r WHERE ROLE_HIST_ID = @myID 
		and
		r.SUB_ROLE not in 
		(SELECT ROLE_ASSIGNED FROM A_ROLE_ASSIGNEE WHERE ROLE = @myID and ROLE_ASSIGNED IS NOT NULL)



if @roleName is Null
	DELETE FROM A_Z_DROP_DOWN_POPULATOR WHERE VAL = @myRoot
else
	UPDATE A_Z_DROP_DOWN_POPULATOR SET SHOW = @roleName WHERE VAL = @myRoot


