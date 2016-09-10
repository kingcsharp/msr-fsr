






/*
STORED PROCEDURE CALLED IN meeting/saveMeeting.asp
purpose: to add meeting to actoin plan after user accepts the meeting
*/
CREATE                    PROCEDURE A_SP_MEETING_UPDATE_ONE_ACTION_PLAN
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@meetingID varchar(50),
@strNTLogin varchar(50)
AS
print 'Inside A_SP_MEETING_UPDATE_ONE_ACTION_PLAN '
--Declare @newID varchar(50) 
Declare @taskID varchar(50) 
--Declare @messages varchar(500) 
Declare @meeting_name varchar(100) 
Declare @initiatorID varchar(100) 
Declare @comment varchar(500) 
Declare @start_date datetime
Declare @stop_date datetime

SELECT @meeting_name = MEETING_NAME,  
		@initiatorID = OWNER,
       @comment=COMMENT,
       @start_date = dbo.TimeToLocal(START_DATE,@strNTLogin),
       @stop_date =dbo.TimeToLocal(STOP_DATE,@strNTLogin)
FROM A_MEETINGS
WHERE ID=@meetingID 
/*
exec A_SP_TASKS_UPDATE_TASK @newID OUTPUT, @messages OUTPUT, 
null, -- @ID
null,  --@parent id
@initiatorID, --@requestor
@strNTLogin, --@requestee_id
null, --@GROUP_REQUESTEE_ID
@meeting_name,--@DESCRIPTION
null,--@SYSTEM_TASK
@comment, --@comment
null, --@SECURITY_LEVEL
null, --@COUNTER
@start_date, --@ORIG_PLANNED_START_DATE
@stop_date, --@ORIG_PLANNED_STOP_DATE
null, --@ORIG_PLANNED_COUNTER_START
null, --@ORIG_PLANNED_COUNTER_STOP
@start_date, --@CUR_PLANNED_START_DATE
@stop_date, --@CUR_PLANNED_STOP_DATE
null,--@CUR_PLANNED_COUNTER_START 
null, --@CUR_PLANNED_COUNTER_STOP
null, --@ACTUAL_START_DATE
null, --@ACTUAL_STOP_DATE
null,  --@ACTUAL_COUNTER_START
null, --@ACTUAL_COUNTER_STOP
null, --@REF_PROC_LIST
null,--@REF_FILE_LIST
null,  --@DISCUSSIONS
null, --@SURVEYS
null, --@meetings -- this is for setting a task for a meeting. not accpting one. 
null, --@OBJECTS 
null, --@COMPANIES
null, --@SYSTEM_PEOPLE_TO_EMAIL,
1, --@PRIORITY  1 is High Urgency and easy
null, -- @ADDITIONAL_ASSIGNEES
@strNTLogin

print 'just ran A_SP_TASKS_UPDATE_TASK with the taskID of  ' + isNull(@newID,'NULL')
*/
-- FIXME::: add this code back after we figure out what the problem is.
-- PROBLEM: This won't run unless it's directly ran inside Query Analyzer. Had to put another function call in saveMeeting.asp.


--Declare @RET_STATUS varchar(50) 
--exec A_SP_TASK_ACCEPT 
--@RET_STATUS OUTPUT, 
--@messages OUTPUT,
--@taskID,
--@strNTLogin








