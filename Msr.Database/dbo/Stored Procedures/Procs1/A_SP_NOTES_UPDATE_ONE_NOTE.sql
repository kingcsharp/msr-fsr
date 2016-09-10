




CREATE     PROCEDURE dbo.A_SP_NOTES_UPDATE_ONE_NOTE
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@noteType varchar(50),
@parentID varchar(8000),
@ID varchar(50),
@txt nvarchar(4000),
@refFiles varchar(8000),
@securityType varchar(50),
@personToView varchar(8000),
@peopleToView varchar(8000),
@roleToView varchar(8000),
@rolesToView varchar(8000),
@companyToView varchar(8000),
@companiesToView varchar(8000),
@securityClearance varchar(50),
@responseAllowed varchar(50),
@notify varchar(50),
@strNTLogin varchar(50)
AS
print 'inside update one note'
print 'The security type = ' + @securityType
if @ID is null 
	begin
	print 'we are making a New note'
	exec SP_GETUNIQUEID3 @ID OUTPUT 
	INSERT INTO A_NOTES (ID,TXT,DATE_CREATED,STATUS,HIDE_NOTE,TO_READ_COUNT,DRCM,MODBY,REVISION,AUTHOR)
	VALUES(@ID,@txt,getDate(),'CREATING',0,0,getDate(),@strNTLogin,0,@strNTLogin)
	if @parentID is not null
		INSERT INTO A_NOTE_CHILDREN (ID,PARENT,CHILD,DRCM,MODBY)
			VALUES (newID(),@parentID,@ID,getdate(),@strNTLogin)
	end
set @newID = @ID

UPDATE A_NOTES 
set 
	TXT = @txt,
	SECURITY_TYPE = @securityType,
	RESPONSE_ALLOWED = @responseAllowed,
	NOTIFY = @notify,
	SECURITY_LEVEL = @securityClearance,
	NOTE_TYPE = @noteType
WHERE ID = @ID 

CREATE TABLE #TempItems (IT varchar(50))
CREATE TABLE #TempDistinctItems (IT varchar(50))
declare @countTempRows int

DELETE FROM A_NOTE_CHILDREN WHERE CHILD = @ID
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @parentID,','
DELETE FROM #TempDistinctItems
INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
INSERT INTO A_NOTE_CHILDREN (ID,PARENT,CHILD,DRCM,MODBY)
	SELECT newID(),ltrim(IT),@ID,getDate(),@strNTlogin FROM #TempDistinctItems 


DELETE FROM A_NOTE_PEOPLE_TO_VIEW WHERE NOTE_ID = @ID  AND CREATED_BY = @strNTLogin
if @peopleToView IS NOT NULL OR @personToView IS NOT NULL
	begin
	DELETE FROM #TempItems
	if @personTOVIew IS NOT NULL 
		INSERT INTO #TempItems (IT) VALUES (@personToView)
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @peopleToView,','
	DELETE FROM #TempDistinctItems
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	INSERT INTO A_NOTE_PEOPLE_TO_VIEW (ID,NOTE_ID,PERSON_ID,DRCM,MODBY,IS_READ,CREATED_BY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTlogin,0,@strNTlogin FROM #TempDistinctItems 
			WHERE not (exists(SELECT PERSON_ID FROM A_NOTE_PEOPLE_TO_VIEW WHERE PERSON_ID = ltrim(IT) AND NOTE_ID = @ID))
	SELECT 'People To View',* FROM A_NOTE_PEOPLE_TO_VIEW WHERE NOTE_ID = @ID
	end 

DELETE FROM A_NOTE_ROLES_TO_VIEW WHERE NOTE_ID = @ID AND CREATED_BY = @strNTLogin
if @roleToView IS NOT NULL OR @rolesToView IS NOT NULL
	begin
	DELETE FROM #TempItems
	if @roleToView IS NOT NULL 
		INSERT INTO #TempItems (IT) VALUES (@roleToView)
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @rolesToView,','
	DELETE FROM #TempDistinctItems
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	INSERT INTO A_NOTE_ROLES_TO_VIEW (ID,NOTE_ID,ROLE_ID,DRCM,MODBY,CREATED_BY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTlogin,@strNTlogin FROM #TempDistinctItems 
			WHERE not (exists(SELECT ROLE_ID FROM A_NOTE_ROLES_TO_VIEW WHERE ROLE_ID = ltrim(IT) AND NOTE_ID = @ID))
	print 'inserted roles'
	SELECT 'ROLES To View',* FROM A_NOTE_ROLES_TO_VIEW WHERE NOTE_ID = @ID
	end 


DELETE FROM A_NOTE_COMPANIES_TO_VIEW WHERE NOTE_ID = @ID AND CREATED_BY = @strNTLogin
if @companyToView IS NOT NULL OR @companiesToView IS NOT NULL
	begin
	DELETE FROM #TempItems
	if @companyToView IS NOT NULL 
		INSERT INTO #TempItems (IT) VALUES (@companyToView)
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @companiesToView,','
	DELETE FROM #TempDistinctItems
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	INSERT INTO A_NOTE_COMPANIES_TO_VIEW (ID,NOTE_ID,COMPANY_ID,DRCM,MODBY,CREATED_BY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTlogin,@strNTLogin FROM #TempDistinctItems 
			WHERE not (exists(SELECT COMPANY_ID FROM A_NOTE_COMPANIES_TO_VIEW WHERE COMPANY_ID = ltrim(IT) AND NOTE_ID = @ID))
	print 'inserted roles'
	SELECT 'Companies to View',* FROM A_NOTE_COMPANIES_TO_VIEW WHERE NOTE_ID = @ID
	end 



--Ref files
if @refFiles IS NOT NULL 
	begin
	DELETE FROM #TempItems
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @refFiles,','
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	DELETE FROM A_NOTE_FILES WHERE NOTE_ID = @ID 
	INSERT INTO A_NOTE_FILES (ID,NOTE_ID,FILE_ID,DRCM,MODBY)
		SELECT newID(),@ID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems
	end

if @securityType = 'SENT'
	begin
	print '%%%%%%%%%%%%%%%%%%%%%%%sending email'
	exec A_SP_NOTE_SEND_EMAIL @ID,@strNTLogin
	end






