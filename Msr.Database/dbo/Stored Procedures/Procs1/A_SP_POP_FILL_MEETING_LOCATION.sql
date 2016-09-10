







/*
STORED PROCEDURE CALLED IN 

*/
CREATE           PROCEDURE A_SP_POP_FILL_MEETING_LOCATION
@meetingID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_MEETING_LOC_WITH_NAMES
WHERE M_ID=@meetingID











