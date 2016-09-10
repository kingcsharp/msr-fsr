



/*
STORED PROCEDURE CALLED IN messages/processMessages.asp

*/

CREATE              PROCEDURE A_SP_MESSAGES_MARK_AS_READ
@messageID varchar(50),
@strNTLogin nvarchar(50)
AS
declare @hasToRead varchar(50)
declare @hasCCRead varchar(50)
declare @currentToCount int
declare @currentCCedCount int
declare @notify int


print 'check to see if i read this message' 
SELECT @hasToRead = IS_READ
FROM A_MESSAGES_PEOPLE_LINK 
WHERE MESSAGE_ID = @messageID
	AND PERSON_ID = @strNTLogin
	AND IS_CC_MESSAGE = 0

print 'check to see if i read this message cced to me' 
SELECT @hasCCRead = IS_READ
FROM A_MESSAGES_PEOPLE_LINK 
WHERE MESSAGE_ID = @messageID
	AND PERSON_ID = @strNTLogin
	AND IS_CC_MESSAGE = 1

print '@hasToRead'+ convert(varchar(50),@hasToRead) 
print '@hasCCRead'+ convert(varchar(50),@hasCCRead) 

if @hasToRead = 0 
	begin
	print 'have not read the message so setting my status to read where its TO me'
	UPDATE A_MESSAGES_PEOPLE_LINK  
	SET IS_READ = 1,
		DATE_READ = getDate()
	WHERE MESSAGE_ID = @messageID
		AND PERSON_ID = @strNTLogin
		AND IS_CC_MESSAGE = 0
	end

if @hasCCRead = 0 
	begin
	print 'have not read the cced message setting my status to read where CCed'
	UPDATE A_MESSAGES_PEOPLE_LINK  
	SET IS_READ = 1,
		DATE_READ = getDate()
	WHERE MESSAGE_ID = @messageID
		AND PERSON_ID = @strNTLogin
		AND IS_CC_MESSAGE = 1
	end
	
SELECT 	@currentToCount = TO_READ_COUNT,
		@currentCCedCount = CC_READ_COUNT,
		@notify = NOTIFY 
FROM A_MESSAGES WHERE ID = @messageID

print 'my current read TO count is ' + convert(varchar(50),@currentToCount)
print 'my current read CCEd count is ' + convert(varchar(50),@currentCCedCount)
	
print 'adding my read count to A_MESSAGES'

if @hasToRead = 0 	
	begin
	print 'Have not read the message so adding my read count to A_MESSAGES'
	UPDATE A_MESSAGES   
	SET	TO_READ_COUNT = @currentToCount  + 1
	WHERE ID = @messageID
	end 
	
if @hasCCRead = 0 	
	begin
	print 'Have not read the message so adding my read CC count to A_MESSAGES'
	UPDATE A_MESSAGES   
	SET	CC_READ_COUNT = @currentCCEDCount  + 1
	WHERE ID = @messageID
	end 

if @notify = 1 
	begin
	print 'Emailing my reading to sender'
	exec A_SP_MESSAGES_SEND_MARK_AS_READ_EMAIL @messageID,@strNTLogin
	end





