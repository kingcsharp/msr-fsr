


/*
STORED PROCEDURE CALLED IN meeting/emailMeeting.asp
*/

CREATE       PROCEDURE A_SP_MEETING_GET_EMAIL_DATA_BY_ID
@ID nvarchar(50),
@strNTLogin nvarchar(50)

AS


print 'updating the date sent value in A_MEETINGS'

UPDATE A_MEETINGS 
SET DATE_EMAIL_SENT = getdate()
where ID = @ID


SELECT * ,dbo.timeToLocal(START_DATE, @strNTLogin) AS LOCAL_START_DATE,
dbo.timeToLocal(STOP_DATE, @strNTLogin) AS LOCAL_STOP_DATE,
dbo.timeToLocal(DATE_CREATED, @strNTLogin) AS LOCAL_DATE_CREATED
FROM A_V_MEETING_EMAIL_DATA
WHERE ID=@ID



