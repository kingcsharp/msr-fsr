


CREATE       PROCEDURE A_SP_MEETING_GET_TASKS_FOR_ONE_MEETING
@meetingID nvarchar(50),
@strNTLogin varchar(50)
AS
print 'Getting meetings. Data comes from A_V_MEETING_TASKS '
SELECT *
FROM A_V_MEETING_TASKS
WHERE @meetingID= MEETING_ID