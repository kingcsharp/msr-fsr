


CREATE                        PROCEDURE A_SP_MESSAGES_UPDATE_ONE_MESSAGE
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@parentID varchar(50),
@messageID varchar(50),
@message nvarchar(4000),
@notify nvarchar(4000),
@importance varchar(50),
@personSent varchar(8000),
@peopleSent varchar(8000),
@rolesSent varchar(8000),
@companySent varchar(8000),
@personSentCC varchar(8000),
@peopleSentCC varchar(8000),
@rolesSentCC varchar(8000),
@companySentCC varchar(8000),
@refProcedures varchar(8000),
@refFiles varchar(8000),
@strNTLogin varchar(50)
AS
print 'inside update messages'
if @messageID is null 
	begin
	print 'we are making a New message'
	exec SP_GETUNIQUEID3 @newID OUTPUT 
	print 'This is a brand new one'
	INSERT INTO A_MESSAGES ([ID] ,SENDER,DATE_CREATED,STATUS,HIDE_MESSAGE,TO_READ_COUNT,CC_READ_COUNT,DRCM,MODBY)
	VALUES(@newID,@strNTLogin,getDate(),'CREATING',0,0,0,getDate(),@strNTLogin)
	if @parentID IS NOT NULL 
		begin
		UPDATE A_MESSAGES 
		set PARENT_ID = @parentID
		WHERE ID = @newID 
		end 
	set @messageID = @newID
	end
UPDATE A_MESSAGES 
set MESSAGE = @message,
IMPORTANCE = @importance,
NOTIFY = @notify
WHERE ID = @messageID 

CREATE TABLE #TempItems (IT varchar(50))
CREATE TABLE #TempDistinctItems (IT varchar(50))
declare @countTempRows int


if @peopleSent IS NOT NULL OR @personSent IS NOT NULL
	begin
	DELETE FROM #TempItems
	print 'Deleting from A_MESSAGES_PEOPLE_LINK where IS_CC_MESSAGE = 0'
	DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0
	if @personSent IS NOT NULL 
		begin
		print 'processing @personSent of' + @personSent
		print 'Inserting my one person in @personSent into #TempItems'
		INSERT INTO #TempItems (IT) VALUES (@personSent)
		end
	print 'We are now going to update the people sent to'
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @peopleSent,','
	DELETE FROM #TempDistinctItems
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	SELECT @countTempRows = COUNT(IT) from #TempDistinctItems
	INSERT INTO A_MESSAGES_PEOPLE_LINK (ID,MESSAGE_ID,PERSON_ID,IS_CC_MESSAGE,IS_READ,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),0,0,getDate(),@strNTlogin FROM #TempDistinctItems
	end 

DELETE FROM A_MESSAGES_ROLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0
if @rolesSent IS NOT NULL 
	begin
	print 'We are now going to update the roles sent to'
	DELETE FROM #TempItems
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @rolesSent,','
	INSERT INTO A_MESSAGES_ROLE_LINK (ID,MESSAGE_ID,ROLE_ID,IS_CC_MESSAGE,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),0,getDate(),@strNTlogin FROM #TempItems
	end

DELETE FROM A_MESSAGES_COMPANY_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0
if @companySent IS NOT NULL 
	begin
	print 'We are now going to update the companies sent to'
	DELETE FROM #TempItems
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @companySent,','
	INSERT INTO A_MESSAGES_COMPANY_LINK (ID,MESSAGE_ID,COMPANY_ID,IS_CC_MESSAGE,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),0,getDate(),@strNTlogin FROM #TempItems
	end 	


DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 1
if @peopleSentCC IS NOT NULL OR @personSentCC IS NOT NULL
	begin
	DELETE FROM #TempItems
	DELETE FROM #TempDistinctItems
	print 'Deleting from A_MESSAGES_PEOPLE_LINK where IS_CC_MESSAGE = 1'
	if @personSentCC IS NOT NULL
		begin
		print 'processing @personSentCC of' + @personSentCC
		print 'Inserting my one person in @personSentCC into #TempItems'
		INSERT INTO #TempItems (IT) VALUES (@personSentCC)
		end 
	print 'We are now going to update the people sent to CCed'
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @peopleSentCC,','
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	INSERT INTO A_MESSAGES_PEOPLE_LINK (ID,MESSAGE_ID,PERSON_ID,IS_CC_MESSAGE,IS_READ,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),1,0,getDate(),@strNTlogin FROM #TempDistinctItems
	print 'Deleting any person in CC thats in in person TO'
	DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE IS_CC_MESSAGE = 1 AND MESSAGE_ID = @messageID
	AND PERSON_ID IN 
	(SELECT PERSON_ID FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0) 
	end 


DELETE FROM A_MESSAGES_ROLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 1
if @rolesSentCC IS NOT NULL 
	begin
	print 'We are now going to update the roles sent to CCed'
	DELETE FROM #TempItems
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @rolesSentCC,','
	DELETE FROM #TempDistinctItems
	INSERT INTO A_MESSAGES_ROLE_LINK (ID,MESSAGE_ID,ROLE_ID,IS_CC_MESSAGE,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),1,getDate(),@strNTlogin FROM #TempItems
	print 'Deleting any role in CC thats in in role TO'
	DELETE FROM A_MESSAGES_ROLE_LINK WHERE IS_CC_MESSAGE = 1 AND MESSAGE_ID = @messageID
	AND ROLE_ID IN 
	(SELECT ROLE_ID FROM A_MESSAGES_ROLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0) 
	end

DELETE FROM A_MESSAGES_COMPANY_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 1
if @companySentCC IS NOT NULL 
	begin
	print 'We are now going to update the companies sent to CC'
	DELETE FROM #TempItems
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @companySentCC,','
	INSERT INTO A_MESSAGES_COMPANY_LINK (ID,MESSAGE_ID,COMPANY_ID,IS_CC_MESSAGE,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),1,getDate(),@strNTlogin FROM #TempItems
	print 'Deleting any company in CC thats in in company TO'
	DELETE FROM A_MESSAGES_COMPANY_LINK WHERE IS_CC_MESSAGE = 1 AND MESSAGE_ID = @messageID
	AND COMPANY_ID IN 
	(SELECT COMPANY_ID FROM A_MESSAGES_COMPANY_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0) 
	end

if @refFiles IS NOT NULL 
	begin
	print 'We are now going to update the reference files'
	DELETE FROM #TempItems
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @refFiles,','
	INSERT INTO #TempDistinctItems SELECT DISTINCT IT FROM #TempItems
	DELETE FROM A_MESSAGES_ATTACHMENTS WHERE MESSAGE_ID = @messageID 
	INSERT INTO A_MESSAGES_ATTACHMENTS (ID,MESSAGE_ID,DOC_ID,DRCM,MODBY)
		SELECT newID(),@messageID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems
	end


DELETE FROM A_MESSAGES_PROCEDURE_LINK WHERE MESSAGE_ID = @messageID 
if @refProcedures is not null
begin
print 'We are now going to update the procedures'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @refProcedures,','
INSERT INTO A_MESSAGES_PROCEDURE_LINK (ID,MESSAGE_ID,PROCEDURE_ID,DRCM,MODBY)
SELECT newID(),@messageID,ltrim(IT),getDate(),@strNTlogin FROM #TempItems
end
set @newID = @messageID

--DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND PERSON_ID = @strNTlogin

print 'done with updating one message'





