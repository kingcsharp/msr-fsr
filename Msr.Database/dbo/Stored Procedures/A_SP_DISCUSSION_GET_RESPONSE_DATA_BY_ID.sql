




/*
STORED PROCEDURE CALLED IN disucssion/viewDiscussion.asp
*/
CREATE           PROCEDURE A_SP_DISCUSSION_GET_RESPONSE_DATA_BY_ID
@responseID varchar(50),
@strNTLogin varchar(50)
AS
declare @parentID as varchar(50),@nextID as varchar(50), @replyID as varchar(50), @myDate datetime

SELECT @parentID = PARENT_ID , @myDate = DRCM
FROM A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID 
WHERE ID=@responseID 

SELECT TOP 1 @nextID = ID FROM A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID  
WHERE PARENT_ID=@parentID AND DRCM > @myDate
ORDER BY DRCM

print '@nextID is' + @nextID

SELECT TOP 1 @replyID = ID FROM A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID  
WHERE PARENT_ID=@responseID 
ORDER BY DRCM

print '@replyID is' + @replyID

SELECT @nextID as NEXT_ID, @replyID AS REPLY_ID,
d.* 
FROM A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID d 
WHERE ID=@responseID