
/*
STORED PROCEDURE CALLED IN messages/editServiceCall.asp
*/



CREATE  PROCEDURE A_SP_MESSAGES_DELETE_ONE_MESSAGE 
@messageID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_MESSAGES WHERE 
ID = @messageID

