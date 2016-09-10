







/*
STORED PROCEDURE CALLED IN messages/sendMessages.asp

*/

CREATE                         PROCEDURE A_SP_MESSAGES_SEND_MESSAGE
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@messageID varchar(50),
@strNTLogin nvarchar(50)
AS
declare @it nvarchar(50)
declare @isCCMessage varchar(50)
declare @curs Cursor

UPDATE A_MESSAGES 
SET STATUS = 'SENT',
DATE_SENT = getDate()
WHERE ID = @messageID

CREATE TABLE #TempPeople (ID varchar(50))
CREATE TABLE #TempCCPeople (ID varchar(50))
CREATE TABLE #TempAllPeople (ID varchar(50),IS_CC_MESSAGE varchar(50))


print 'inserting the companies people into the temp table'
INSERT INTO #TempPeople (ID)  
(SELECT DISTINCT ID FROM A_APPROVED_PEOPLE 
WHERE COMPANY IN (SELECT COMPANY_ID FROM A_MESSAGES_COMPANY_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE =0))

print 'inserting the roles people into the temp table'
INSERT INTO #TempPeople (ID)  
(SELECT DISTINCT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS 
WHERE PERSON IS NOT NULL AND ROLE_ID IN (SELECT ROLE_ID FROM A_MESSAGES_ROLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0))

print 'inserting the CC companies people into the temp table'
INSERT INTO #TempCCPeople (ID)  
(SELECT DISTINCT ID FROM A_APPROVED_PEOPLE 
WHERE COMPANY IN (SELECT COMPANY_ID FROM A_MESSAGES_COMPANY_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE =1))

print 'inserting the CC roles people into the temp table'
INSERT INTO #TempCCPeople (ID)  
(SELECT DISTINCT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS 
WHERE PERSON IS NOT NULL AND ROLE_ID IN (SELECT ROLE_ID FROM A_MESSAGES_ROLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 1))

declare @idAlreadyThere varchar(50)
declare @countPeopleTo int
declare @countPeopleCCed int

SELECT * FROM #TempPeople
SELECT * FROM #TempCCPeople

print 'Creating Cursor for people sent to'
set @curs = Cursor For SELECT DISTINCT * FROM #TempPeople
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'Processing ID=' + @it
	DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND PERSON_ID = @it AND IS_CC_MESSAGE = 0 
	if @it IS NOT NULL 
		begin
		print 'Inserting ' + @it + ' into A_MESSAGES_PEOPLE_LINK'
		INSERT INTO A_MESSAGES_PEOPLE_LINK (ID, MESSAGE_ID, PERSON_ID, DRCM, MODBY, IS_CC_MESSAGE, IS_READ) 
		values(newID(), @messageID, @it, getdate(), @strNTLogin,0,0)
		end
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs

print 'Creating Cursor for CC people sent to'
set @idAlreadyThere = NULL
set @curs = Cursor For SELECT DISTINCT * FROM #TempCCPeople
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'Processing ID=' + @it
	DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND PERSON_ID = @it AND IS_CC_MESSAGE = 1 	
	if @it IS NOT NULL 
		begin
		print 'Inserting ' + @it + ' into A_MESSAGES_PEOPLE_LINK'
		INSERT INTO A_MESSAGES_PEOPLE_LINK (ID, MESSAGE_ID, PERSON_ID, DRCM, MODBY, IS_CC_MESSAGE, IS_READ) 
		values(newID(), @messageID, @it, getdate(), @strNTLogin,1,0)
		end 
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs

SELECT @countPeopleTo = COUNT (ID) FROM A_MESSAGES_PEOPLE_LINK 
WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0

SELECT @countPeopleCCed = COUNT (ID) FROM A_MESSAGES_PEOPLE_LINK 
WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 1


if @countPeopleTo > 0 
	begin
	print 'Updating people sent to count because this message has To recipients'
	UPDATE A_MESSAGES
	SET TO_COUNT = @countPeopleTo
	WHERE ID = @messageID
	end

if @countPeopleCCed > 0 
	begin
	print 'Updating CC people sent to count because this message has CC recipients'
	UPDATE A_MESSAGES
	SET CC_COUNT = @countPeopleCCed
	WHERE ID = @messageID
	end

--if @countPeopleCCed = 0 
--	begin
--	print 'Updating CC people sent to count because this message has CC recipients'
--	UPDATE A_MESSAGES
--	SET CC_COUNT = -1,
--	 CC_READ_COUNT = -1
--	WHERE ID = @messageID
--	end

if @countPeopleCCed = 0 
	begin
	print 'Updating CC people sent to count because this message has CC recipients'
	UPDATE A_MESSAGES
	SET CC_COUNT = NULL,
	 CC_READ_COUNT = NULL
	WHERE ID = @messageID
	end



print 'deleting my duplicates already in to '
DELETE FROM A_MESSAGES_PEOPLE_LINK 
WHERE IS_CC_MESSAGE = 1 AND MESSAGE_ID = @messageID 
		AND PERSON_ID IN (SELECT PERSON_ID FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND IS_CC_MESSAGE = 0) 

--print 'deleting me out of the table because i am the sender '
--DELETE FROM A_MESSAGES_PEOPLE_LINK WHERE MESSAGE_ID = @messageID AND PERSON_ID = @strNTlogin 

print 'Emailing my message'
exec A_SP_MESSAGES_SEND_MESSAGE_EMAIL @messageID,@strNTLogin











