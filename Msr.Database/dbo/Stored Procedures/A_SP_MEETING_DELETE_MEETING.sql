









/*
STORED PROCEDURE CALLED IN meeting/deleteMeeting.asp
*/



CREATE        PROCEDURE A_SP_MEETING_DELETE_MEETING 
@meetingID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_MEETINGS WHERE 
ID = @meetingID