



CREATE    PROCEDURE dbo.A_SP_TASK_UPDATE_MINUTES
@taskID varchar(50),
@strNTLogin varchar(50)
AS

print 'updating the minutes for task = ' + @taskID
declare @manual int,
	@actStopTime datetime,@actStartTime dateTime,
	@planStopTime datetime,@planStartTime dateTime,
	@myActMinutes int,@myPlanMinutes int,
	@mySecs float,@workerID varchar(50)
SELECT @actStopTime = ACTUAL_STOP_DATE, @actStartTime = ACTUAL_START_DATE,
	@planStopTime = CUR_PLANNED_STOP_DATE, @planStartTime = CUR_PLANNED_START_DATE,
	@workerID = REQUESTEE_ID
	FROM A_V_TASK_SEARCH WHERE ID = @taskID
if @workerID is null
	goto fin

print 'Start Time = '
print @actStartTime
print 'Stop Time = '
print @actStopTime
declare @myStartDate datetime
set @myStartDate = getDate()
if @actStopTime is not null and @actStartTime is null
	begin
	print 'We need to get the act start time from the request time.'
	SELECT @actStartTime = LAST_REQUEST_DATE FROM A_TASKS WHERE ID = @taskID
	exec A_SP_TASK_DATES_UPDATE @taskID,'ACTUAL_START',@myStartDate,@strNTLogin
	exec A_SP_TASKS_UPDATE_DATA @taskID,@strNTLogin
	end




SELECT @manual = MANUAL FROM A_TASK_MINUTES WHERE TASK_ID = @taskID AND WORKER_ID = @workerID
if @manual = 1 goto fin

DELETE FROM A_TASK_MINUTES WHERE TASK_ID = @taskID and WORKER_ID = @workerID
if exists(SELECT ID FROM A_TASKS WHERE PARENT_ID = @taskID)
	begin
	print 'Need to get the minutes from the child tasks'
	SELECT 	@myActMinutes = sum(ACTUAL_MINUTES),
			@myPlanMinutes = sum(PLANNED_MINUTES)
		FROM A_TASK_MINUTES WHERE TASK_ID IN 
			(SELECT ID FROM A_TASKS WHERE PARENT_ID = @taskID)
	end 

if @myActMinutes is null and 
	(@actStartTime is not null 
	and @actStopTime is not null)
	begin
	SET @myActMinutes = 
		ceiling(convert(float,dateDiff(s,@actStartTime,@actStopTime))/60.0)
	end
if @myPlanMinutes is null and 
	(@planStartTime is not null 
	and @planStopTime is not null)
	begin
	SET @myPlanMinutes = 
		ceiling(convert(float,dateDiff(s,@planStartTime,@planStopTime))/60.0)
	end

if @myPlanMinutes is null
	set @myPlanMinutes = @myActMinutes

if @myPlanMinutes is not null and @myActMinutes is not null
INSERT INTO A_TASK_MINUTES (ID,WORKER_ID,TASK_ID,ACTUAL_MINUTES,PLANNED_MINUTES,
					MANUAL,DRCM,MODBY)
	VALUES (newID(),@workerID,@taskID,@myActMinutes,@myPlanMinutes,0,getDate(),@strNTLogin)


fin:







