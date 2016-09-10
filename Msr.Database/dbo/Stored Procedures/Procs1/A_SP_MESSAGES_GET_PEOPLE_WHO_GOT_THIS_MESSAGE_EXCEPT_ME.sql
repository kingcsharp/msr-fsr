









/*
STORED PROCEDURE CALLED IN projects/editProjects.asp

*/

CREATE            PROCEDURE A_SP_MESSAGES_GET_PEOPLE_WHO_GOT_THIS_MESSAGE_EXCEPT_ME
@messageID varchar(50),
@isCCMessage varchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * 
FROM A_V_MESSAGES_PEOPLE_SENT_TO_DATA
WHERE MESSAGE_ID =@messageID
AND IS_CC_MESSAGE = @isCCMessage
AND P_ID <> @strNTLogin
ORDER BY LAST_NAME









