








/*
STORED PROCEDURE CALLED IN /editSurvey.asp
*/




create         PROCEDURE A_SP_MEETING_DELETE_INV_COMPANY 
@meetingID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_MEETING_INV_COMPANY WHERE 
MEETING_ID = @meetingID