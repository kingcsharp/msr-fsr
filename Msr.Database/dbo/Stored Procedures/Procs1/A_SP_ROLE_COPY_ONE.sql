








CREATE            procedure A_SP_ROLE_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_ROLES_HISTORY (ID,NAME,SOURCE,HIDDEN,DRCM,MODBY,IS_ADMIN,SECURITY_LEVEL)
SELECT @newID as ID,NAME + @copyPrefix as NAME,'A_SP_COPY' as SOURCE,HIDDEN,DRCM,@strNTLogin,IS_ADMIN,SECURITY_LEVEL FROM A_ROLES_HISTORY WHERE ID = @strID
--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_ROLES_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID
--Assign everyone to this role that was assigned to the old role
INSERT INTO A_ROLE_ASSIGNEE (ID,ROLE,PERSON,STATUS,SOURCE,ROLE_ASSIGNED,DRCM,MODBY,StartDate,EndDate)
SELECT newID(),@newID,PERSON,STATUS,'A_SP_ROLE_COPY_ONE',ROLE_ASSIGNED,getDATE(),@strNTLogin,StartDate,EndDate
FROM A_ROLE_ASSIGNEE WHERE ROLE = @strID
--Copy department Roles
INSERT INTO A_ROLES_DEPARTMENT_SUB_ROLES (ID,ROLE_HIST_ID,DEPARTMENT,DRCM,MODBY,SUB_ROLE)
SELECT newID(),@newID,DEPARTMENT,getDATE(),@strNTLogin,SUB_ROLE
FROM A_ROLES_DEPARTMENT_SUB_ROLES WHERE ROLE_HIST_ID = @strID












