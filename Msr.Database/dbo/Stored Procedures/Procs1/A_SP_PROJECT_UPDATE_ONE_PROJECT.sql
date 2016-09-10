





CREATE               PROCEDURE A_SP_PROJECT_UPDATE_ONE_PROJECT
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@projectID varchar(50),
@NAME nvarchar(1000),
@PRIORITY_LEVEL varchar(50),
@OBJECTIVE nvarchar(1000),
@SPONSORS varchar(8000),
@LEADER varchar(50),
@MEMBERS varchar(8000),
@COMPANIES_TO_VIEW varchar(8000),
@ROLES_TO_VIEW varchar(8000),
@PEOPLE_TO_VIEW varchar(8000),
@SECURITY_LEVEL varchar(50),
@ORIGINAL_PLANNED_STOP_DATE varchar(50),
@CURRENT_PLANNED_STOP_DATE varchar(50),
@ACTUAL_STOP_DATE varchar(50),
@MONTH_GOALS nvarchar(1000),
@ISSUES nvarchar(1000),
@SUMMARY nvarchar(1000),
@strNTLogin varchar(50)
AS
declare @newDiscussionID varchar(50)
declare @openingStatement varchar(300)
declare @newOpeningStatementID varchar(50)

declare @newDiscussionProjectName varchar(1000)

print 'inside update project'
if @projectID is null
begin
 	print 'we are making a New project'
 	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_PROJECTS ([ID] , DATE_CREATED,INITIATOR, STATUS)
	VALUES(@newID, getDate(), @strNTLogin, 'OPEN')
	set @projectID=@newID
	print 'Created a new project with the id of ' + @projectID
end
else set @newID = @projectID

print 'we are updating the project with project id of ' + @projectID
UPDATE A_PROJECTS 
set [NAME] = @NAME,
PRIORITY_LEVEL = @PRIORITY_LEVEL,
OBJECTIVE =@OBJECTIVE,
LEADER = @LEADER,
SECURITY_LEVEL = @SECURITY_LEVEL,
ORIGINAL_PLANNED_STOP_DATE = @ORIGINAL_PLANNED_STOP_DATE,
CURRENT_PLANNED_STOP_DATE = @CURRENT_PLANNED_STOP_DATE,
ACTUAL_STOP_DATE = @ACTUAL_STOP_DATE,
MONTH_GOALS = @MONTH_GOALS,
ISSUES = @ISSUES,
SUMMARY = @SUMMARY,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @projectID

SELECT @newDiscussionID = DISCUSSION_ID FROM A_PROJECTS WHERE ID = @newID

if @newDiscussionID is not null and @NAME is not null 
	begin
		print 'Updating discussion'
		set @newDiscussionProjectName = isNull(@NAME,'') + ' Main Discussion' 
		UPDATE A_DISCUSSIONS
		SET SUBJECT = @newDiscussionProjectName
		where ID = @newDiscussionID
	end



print 'The discussion ID for this project is ' + isNull(@newDiscussionID,'NULL')
if @newDiscussionID is null
	begin
	print 'Since the discussion is null we are goin gto make a new one'
	set @newDiscussionProjectName = isNull(@NAME,'') + ' Main Discussion' 
	print 'We are going to name it ' + @newDiscussionProjectName
	set	@openingStatement = 'The system automatically makes a main discussion for the project.  Initiator, sponsors, leader, & members are invited.  This is where members can write daily updates, for example.'  
	print 'The opening statement is ' + @openingStatement
	exec A_SP_DISCUSSION_UPDATE_ONE_DISCUSSION @newDiscussionID OUTPUT, @msg OUTPUT,
		null,@newDiscussionProjectName,@openingStatement,@strNTLogin
	UPDATE A_PROJECTS SET DISCUSSION_ID = @newDiscussionID WHERE ID = @newID
    exec A_SP_DISCUSSION_UPDATE_OPENING_STATEMENT @newOpeningStatementID OUTPUT, @msg OUTPUT,
		@newDiscussionID, @openingStatement, '0,102,255',@strNTLogin
	print 'Going to make this discussion related to this project'
	INSERT INTO A_PROJECT_ITEM_LINK (ID,PROJECT_ID,ITEM_ID,ITEM_TYPE,DRCM,MODBY)
	SELECT newID(),@projectID,@newDiscussionID,'A_DISCUSSIONS',getDate(),@strNTLogin
	end






CREATE TABLE #TempItems (IT varchar(50))

print 'We are now going to update the sponsors'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SPONSORS,','
DELETE FROM A_PROJECT_SPONSORS WHERE PROJECT_ID = @projectID
INSERT INTO A_PROJECT_SPONSORS (ID,PROJECT_ID,PEOPLE_ID,DRCM,MODBY)
	SELECT newID(),@projectID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems

print 'We are now going to update the sponsors in table A_DISCUSSIONS_INV_PEOPLE '
INSERT INTO A_DISCUSSION_INV_PEOPLE
	([DISCUSSION_ID],[PEOPLE_ID], [DRCM],[MODBY])
		SELECT @newDiscussionID,ltrim(IT), getDate() ,@strNTLogin FROM #TempItems  
		WHERE NOT EXISTS (SELECT * FROM A_DISCUSSION_INV_PEOPLE 
						WHERE DISCUSSION_ID = @newDiscussionID AND PEOPLE_ID=ltrim(IT))	

print 'We are now going to update the leader in table A_DISCUSSIONS_INV_PEOPLE '
INSERT INTO A_DISCUSSION_INV_PEOPLE
	([DISCUSSION_ID],[PEOPLE_ID], [DRCM],[MODBY])
		SELECT @newDiscussionID,@LEADER, getDate() ,@strNTLogin    		WHERE NOT EXISTS (SELECT * FROM A_DISCUSSION_INV_PEOPLE 
						 WHERE DISCUSSION_ID = @newDiscussionID AND PEOPLE_ID=@LEADER)	

print 'We are now going to update the members'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @MEMBERS,','
DELETE FROM A_PROJECT_MEMBERS WHERE PROJECT_ID = @projectID
INSERT INTO A_PROJECT_MEMBERS (ID,PROJECT_ID,PEOPLE_ID,DRCM,MODBY)
	SELECT newID(),@projectID,ltrim(IT),getDate(),@strNTlogin  FROM #TempItems

print 'We are now going to invite the members to the discussion A_DISCUSSIONS_INV_PEOPLE '
INSERT INTO A_DISCUSSION_INV_PEOPLE
	([DISCUSSION_ID],[PEOPLE_ID], [DRCM],[MODBY])
		SELECT @newDiscussionID,ltrim(IT), getDate() ,@strNTLogin FROM #TempItems  
		WHERE NOT EXISTS (SELECT * FROM A_DISCUSSION_INV_PEOPLE 
						WHERE DISCUSSION_ID = @newDiscussionID AND PEOPLE_ID=ltrim(IT))	

print 'We are now going to update the companies to view'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @COMPANIES_TO_VIEW,','
DELETE FROM A_PROJECT_ALLOWED_COMPANIES WHERE PROJECT_ID = @projectID
INSERT INTO A_PROJECT_ALLOWED_COMPANIES (ID,PROJECT_ID,COMPANY_ID,DRCM,MODBY)
	SELECT newID(),@projectID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems

--print 'We are now going to update the allowed companies in table A_DISCUSSION_INV_COMPANY '
--INSERT INTO A_DISCUSSION_INV_COMPANY
--	([DISCUSSION_ID],[COMPANY_ID], [DRCM],[MODBY])
--		SELECT @newDiscussionID,ltrim(IT), getDate() ,@strNTLogin FROM #TempItems  
--		WHERE NOT EXISTS (SELECT * FROM A_DISCUSSION_INV_COMPANY 
--						WHERE DISCUSSION_ID = @newDiscussionID AND COMPANY_ID=ltrim(IT))	

print 'We are now going to update the roles to view'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @ROLES_TO_VIEW,','
DELETE FROM A_PROJECT_ALLOWED_ROLES WHERE PROJECT_ID = @projectID
INSERT INTO A_PROJECT_ALLOWED_ROLES (ID,PROJECT_ID,ROLE_ID,DRCM,MODBY)
	SELECT newID(),@projectID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems

--print 'We are now going to update the allowed  roles in table A_DISCUSSION_INV_ROLE '
--INSERT INTO A_DISCUSSION_INV_ROLE
--	([DISCUSSION_ID],[ROLE_ID], [DRCM],[MODBY])
--		SELECT @newDiscussionID,ltrim(IT), getDate() ,@strNTLogin FROM #TempItems  
--		WHERE NOT EXISTS (SELECT * FROM A_DISCUSSION_INV_ROLE 
--						WHERE DISCUSSION_ID = @newDiscussionID AND ROLE_ID=ltrim(IT))	

print 'We are now going to update the people to view'
DELETE FROM #TempItems
DELETE FROM A_PROJECT_ALLOWED_PEOPLE WHERE PROJECT_ID = @projectID
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @PEOPLE_TO_VIEW,','
INSERT INTO A_PROJECT_ALLOWED_PEOPLE (ID,PROJECT_ID,PEOPLE_ID,DRCM,MODBY)
SELECT newID(),@projectID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

--print 'We are now going to update the allowed people in table A_DISCUSSION_INV_PEOPLE '
--INSERT INTO A_DISCUSSION_INV_PEOPLE
--	([DISCUSSION_ID],[PEOPLE_ID], [DRCM],[MODBY])
--		SELECT @newDiscussionID,ltrim(IT), getDate() ,@strNTLogin FROM #TempItems  
--		WHERE NOT EXISTS (SELECT * FROM A_DISCUSSION_INV_PEOPLE 
--						WHERE DISCUSSION_ID = @newDiscussionID AND PEOPLE_ID=ltrim(IT))	


print 'The ID for this Project is ' + @projectID
print 'The ID for this Projects Main Discussion is ' + @newDiscussionID






