











CREATE              PROCEDURE [dbo].[A_SP_PEOPLE_FINISH_APPROVAL_WF] 
@myID nvarchar(50),
@strNTLogin nvarchar(50)
as
print 'Finishing the People Approval WF'
declare @myObj as nvarchar(50)
declare @myStatus as nvarchar(50)
declare @myRoot as nvarchar(50)
SELECT @myObj = OBJECT_ID FROM A_PEOPLE_HISTORY WHERE ID = @myID
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @myObj
declare @curs as Cursor
declare @thisID as nvarchar(50)
declare @thisObjID as nvarchar(50)
print 'The Root is ' + @myRoot
set @curs = Cursor for SELECT OBJ_ID,ID FROM A_OBJECTS WHERE ROOT = @myRoot
open @curs
declare @curHistStat as varchar(50)
Fetch NEXT from @curs Into @thisObjID,@thisID
while (@@fetch_status = 0)
	begin
		print 'Updating object # ' + @thisObjID
		SELECT @myStatus = STATUS FROM A_OBJECTS WHERE ID = @thisID
		print 'The object status is ' + isNull(@myStatus,'NULL')
		declare @systemStatus as nvarchar(50)
		set @systemStatus = NULL
		if @myStatus = 'APPROVED'
			set @systemStatus = 'ACTIVE'
		if @myStatus = 'APPROVED_BUT_REVISING'
			set @systemStatus = 'ACTIVE'
		if @myStatus = 'APPROVED_BUT_DELETING'
			set @systemStatus = 'ACTIVE'
		if @myStatus = 'DELETED'
			set @systemStatus = 'INACTIVE'
		if @myStatus = 'OLD'
			set @systemStatus = 'INACTIVE'
		print 'The new PEOPLE Stat is ' + + isNull(@systemStatus,'NULL')
		if @systemStatus = 'ACTIVE'
			begin
				SELECT @curHistStat = SYSTEM_STATUS FROM A_PEOPLE_HISTORY WHERE ID = @thisObjID
				if @curHistStat = 'ACTIVE'
					begin
					print'We are now setting this person to active because people history is TRUE=ACTIVE'
					UPDATE A_PEOPLE_HISTORY SET SYSTEM_STATUS = @systemStatus WHERE ID = @thisObjID
					end
				else
					begin
					print'We are now setting this person to inactive because people history is FALSE=INACTIVE'
					UPDATE A_PEOPLE_HISTORY SET SYSTEM_STATUS = 'INACTIVE' WHERE ID = @thisObjID
					end
			end
		else		
			begin
				UPDATE A_PEOPLE_HISTORY SET SYSTEM_STATUS = @systemStatus WHERE ID = @thisObjID
			end
		Fetch NEXT from @curs Into @thisObjID,@thisID
	end

print 'Updating to latest rev'
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_PEOPLE WHERE ID = @myRoot 
print 'got the data i needed'
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record'
		INSERT INTO A_PEOPLE(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)
	end
--so now make sure that the value in A_PART_TYPES is Approved
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')

declare @curPerson as nvarchar(50)
SELECT @curPerson = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS LIKE 'APPROVED%'
if @curPerson is null UPDATE A_PEOPLE SET STATUS = 'DELETED' WHERE ID = @myRoot
else UPDATE A_PEOPLE SET STATUS = 'APPROVED' WHERE ID = @myRoot

declare @sql varchar(2000)
set @sql = 
'exec A_SP_UPDATE_TABLE_FIELD ''' + @curID + ''',''' + @myRoot + ''',
	''A_PEOPLE'',''HISTORY_REF_ID'''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @SQL,@strNTLogin
exec A_SP_UPDATE_TABLE_FIELD @curID, @myRoot, 'A_PEOPLE','HISTORY_REF_ID'



--Now we need to make sure that the old Position in the Role Assignee table is the
--same as the new position and if it is not set the old one to old and the new one
--to active.
set @sql = 
'exec A_SP_PEOPLE_UPDATE_ROLES_WITH_ONE_PERSONS_POSITON ''' + @myRoot + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @SQL,@strNTLogin

--Now we need to update the subordinates table since it could have changed because of this person
set @sql = 
'exec A_SP_PEOPLE_COMPLETELY_UPDATE_SUBORDINATES_TABLE'
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @SQL,@strNTLogin



print 'Checking to see if this is a new user.'

declare @isNewUserTest varchar(50)

SELECT @isNewUserTest = ID 
FROM A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS
WHERE HISTORY_REF_ID = @myID

if @isNewUserTest IS NOT NULL 
	begin
	print 'Now email the person his username and tell him to change his password.'
	exec A_SP_PEOPLE_EMAIL_SEND_EMAIL @myRoot,@strNTLogin
	end 

declare @fullName varchar(50)
SELECT @fullName = FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @myRoot
UPDATE A_PEOPLE_SEARCH_TABLE SET BOSS_NAME = @fullName WHERE BOSS_ID = @myRoot
if @fullName is Null
	DELETE FROM A_Z_DROP_DOWN_POPULATOR WHERE VAL = @myRoot
else
	UPDATE A_Z_DROP_DOWN_POPULATOR SET SHOW = @fullName WHERE VAL = @myRoot






