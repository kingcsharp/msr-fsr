





/*
STORED PROCEDURE CALLED IN meeting/editMeeting.asp
*/
CREATE    PROCEDURE A_SP_MEETING_GET_HOST_NAME
@meetingID nvarchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * FROM A_V_MEETING_GET_HOST_NAME 
WHERE ID=@meetingID











