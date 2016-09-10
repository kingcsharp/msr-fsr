







CREATE       PROCEDURE A_SP_WF_CREATE_WORKFLOW_GROUP_DATA
	@gsID nvarchar(50),
	@strNTLogin nvarchar(50)

AS
BEGIN TRANSACTION
print 'Creating WF_GROUP_DATA for gsID = ' + @gsID
--Get The WorkFlow Started ID
declare @myWFSID as nvarchar(50)
--Get the group ID
declare @myGID as nvarchar(50)
SELECT @myGID = WF_GROUP_ID,@myWFSID = WFS_ID FROM A_WORKFLOW_GROUP_STARTED WHERE ID = @gsID
print 'MyGID = ' + @myGID + ' myWFSID = ' + @myWFSID
--First Clear all old data
print 'Deleting all the people from Pending with gsID = ' + @gsID
DELETE FROM A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS WHERE WFGS_ID = @gsID
if @@error <> 0 goto PROBLEM
--Now Recreate the data
--First the people in the group
print 'Inserting the people from the group '
print 'INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
SELECT newID(),''' + @gsID + ''',USER_ID,getDate(),''' + @strNTLogin + ''' FROM A_WF_GROUP_PEOPLE_LINK 
WHERE WF_GROUP_ID = ''' + @myGID + ''' AND NOT EXISTS(SELECT * FROM  
A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS WHERE WFGS_ID = ''' + @gsID + ''' AND PERSON = USER_ID)'
INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
SELECT newID(),@gsID,USER_ID,getDate(),@strNTLogin FROM A_WF_GROUP_PEOPLE_LINK WHERE USER_ID IS NOT NULL AND WF_GROUP_ID = @myGID
AND NOT EXISTS(SELECT * FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS WHERE WFGS_ID = @gsID AND PERSON = USER_ID)
if @@error <> 0 goto PROBLEM
--Now the People in the Roles in the Group
print 'Inserting the people in the roles in the group'
print 'INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
SELECT newID(),''' + @gsID + ''',v.PERSON,getDate(),''' + @strNTLogin + '''FROM 
A_V_WF_GROUP_ROLES_WITH_MEMBERS v WHERE v.GROUP_ID = ''' + @myGID + ''' AND NOT 
EXISTS(SELECT p.* FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p 
WHERE p.WFGS_ID = ''' + @gsID + ''' AND p.PERSON = v.PERSON)'
INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
SELECT newID(),@gsID,v.PERSON,getDate(),@strNTLogin FROM A_V_WF_GROUP_ROLES_WITH_MEMBERS v WHERE v.PERSON is not null and v.GROUP_ID = @myGID
AND NOT EXISTS(SELECT p.* FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p WHERE p.WFGS_ID = @gsID AND p.PERSON = v.PERSON)
--Now the People in the advanced Roles in the Group
print 'Inserting the people in the roles in the group'
print 'INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
SELECT newID(),''' + @gsID + ''',v.PERSON,getDate(),''' + @strNTLogin + '''FROM 
A_V_WF_GROUP_ROLE_PEOPLE_2 v WHERE v.GROUP_ID = ''' + @myGID + ''' AND NOT 
EXISTS(SELECT p.* FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p 
WHERE p.WFGS_ID = ''' + @gsID + ''' AND p.PERSON = v.PERSON)'
INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
SELECT newID(),@gsID,v.PERSON,getDate(),@strNTLogin FROM A_V_WF_GROUP_ROLE_PEOPLE_2 v WHERE v.PERSON IS NOT NULL AND v.GROUP_ID = @myGID
AND NOT EXISTS(SELECT p.* FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p WHERE p.WFGS_ID = @gsID AND p.PERSON = v.PERSON)

if @@error <> 0 goto PROBLEM
--Now the Special People in the Groups
declare @curMem Cursor
Set @CurMem = Cursor for SELECT SPECIAL_ID from A_V_WF_GROUP_SPECIAL_MEMBERS WHERE GROUP_ID = @myGID
declare @mySpec nvarchar(50)
Open @CurMem
Fetch Next from @CurMem Into @mySpec
while (@@fetch_status = 0)
	Begin
		if @mySpec = '1'
			INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
			SELECT newID(),@gsID,b.BOSS,getDate(),@strNTLogin FROM A_APPROVED_PEOPLE b WHERE 
			b.ID = (select STARTED_BY FROM A_WORKFLOWS_STARTED WHERE ID = @myWFSID)
			AND NOT EXISTS(SELECT p.* FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p WHERE p.WFGS_ID = @gsID AND p.PERSON = b.BOSS)
			if @@error <> 0 goto PROBLEM
		if @mySpec = '2'
			INSERT INTO A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS (ID,WFGS_ID,PERSON,DRCM,MODBY)
			SELECT newID(),@gsID,bb.BOSS,getDate(),@strNTLogin FROM A_APPROVED_PEOPLE b,A_APPROVED_PEOPLE bb WHERE
			b.boss = bb.id AND 
			b.ID = (select STARTED_BY FROM A_WORKFLOWS_STARTED WHERE ID = @myWFSID)
			AND NOT EXISTS(SELECT p.* FROM  A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p WHERE p.WFGS_ID = @gsID AND p.PERSON = bb.BOSS)
			if @@error <> 0 goto PROBLEM
		Fetch Next from @CurMem Into @mySpec
	End

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_WF_CREATE_WORKFLOW_GROUP_DATA with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_CREATE_WORKFLOW_GROUP_DATA and we will terminate and not finish anything '
return 1





