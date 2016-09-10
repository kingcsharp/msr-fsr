


CREATE                  PROCEDURE A_SP_MESSAGES_GET_DATA_BY_ID
@ID varchar(50),
@strNTLogin nvarchar(50)
AS
declare @isRead varchar(50)
declare @senderIsInList varchar(50)
declare @sender varchar(50)
declare @hasChild varchar(50)

SELECT @sender = SENDER 
FROM A_MESSAGES 
WHERE ID = @ID

SELECT @senderIsInList = ID 
FROM A_MESSAGES_PEOPLE_LINK 
WHERE PERSON_ID = (SELECT SENDER FROM A_MESSAGES WHERE ID = @ID)
AND MESSAGE_ID = @ID

if @senderIsInList is not null
	set @senderIsInList = '1'
else 
	set @senderIsInList = '0'



SELECT @isRead = IS_READ 
FROM A_MESSAGES_PEOPLE_LINK 
WHERE MESSAGE_ID = @ID AND PERSON_ID = @strNTlogin

SELECT @hasChild = hasChild 
FROM A_V_MESSAGES_SEARCH_DATA 
WHERE ID = @ID 

SELECT *, 	
		@isRead AS IS_READ, 
		@senderIsInList as IS_SENDER_IN_PEOPLE_LIST, 
		@hasChild as hasChild, 
		dbo.timeToLocal(DATE_SENT, @strNTLogin) AS LOCAL_DATE_SENT
FROM A_V_MESSAGES_DATA
WHERE ID =@ID









