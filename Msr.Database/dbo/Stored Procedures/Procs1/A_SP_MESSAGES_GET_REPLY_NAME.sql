





/*
STORED PROCEDURE CALLED IN messages/editMessage.asp

*/

CREATE        PROCEDURE A_SP_MESSAGES_GET_REPLY_NAME
@messageID varchar(50),
@cced varchar(50),
@strNTLogin nvarchar(50)
AS
SELECT SENDER_NAME AS PERSON_NAME, SENDER AS P_ID
FROM A_V_MESSAGES_SEARCH_DATA
WHERE ID =@messageID




