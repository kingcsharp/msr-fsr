

CREATE   PROCEDURE dbo.A_SP_TASK_FIGURE_COMPLETION_DATA
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @taskHasChild tinyint,@stat varchar(50),@parentID varchar(50),@procStepID varchar(50),@curStepText varchar(4000),@realID varchar(50)
SELECT 
	@realID = ID,
	@taskHasChild = HAS_CHILD, 
	@stat = STATUS,
	@procStepID = PROCEDURE_STEP_ID,
	@parentID = PARENT_ID
FROM A_TASKS 
WHERE ID = @taskID


if @realID is null
	goto fin
else
	print 'Processing Task = ' + @realID

declare @numSubTasks float,@subTasksComplete float,@numSubTaskHours float,@numSubTaskHoursComplete float,
@percComplete float,@numSubTasksComplete int,@timeComplete float

if isNull(@taskHASChild,0) = 0
	begin
	set @numSubTasks = 1
	SELECT @numSubTaskHours = DURATION FROM A_PROCEDURE_STEPS WHERE ID = @procStepID
	print 'This task has no children so its completion is based on itself'
	if @stat in ('FINISHED','CLOSED','COMPLETED')
		begin
		SET @numSubTasksComplete = 1
		set @numSubTaskHoursComplete = @numSubTaskHours
		set @percComplete = 100
		set @timeComplete = 100
		end
	else
		begin
		SET @numSubTasksComplete = 0
		set @numSubTaskHoursComplete = 0
		set @percComplete = 0
		set @timeComplete = 0
		end
	end
else
	begin
	SELECT @numSubTasks = count(ID) FROM A_TASKS WHERE PARENT_ID = @taskID
	SELECT @numSubTasksComplete = count(ID) FROM A_TASKS WHERE PARENT_ID = @taskID AND STATUS in ('FINISHED','CLOSED','COMPLETED')
	SELECT top 1 @curStepText = DESCRIPTION FROM A_TASKS WHERE PARENT_ID = @taskID AND STATUS in ('REQUESTED','ACCEPTED')
	print 'Cur Step = ' + @curStepText
	SELECT 
		@numSubTasks = count(NUM_SUB_TASKS),
		@numSubTasksComplete = sum(NUM_SUB_TASKS_COMPLETE),
		@numSubTaskHours = sum(MY_TOT_HOURS),
		@numSubTaskHoursComplete = sum(MY_COMP_HOURS)
	FROM A_TASK_COMPLETION_STATS 
	WHERE PARENT_TASK_ID = @taskID
	if isnull(@numSubTasks,0) <> 0
		set @percComplete = (@numSubTasksComplete / @numSubTasks) * 100
	else
		print 'PercComplete not figured'
	if isnull(@numSubTaskHours,0) <> 0
		set @timeComplete = (@numSubTaskHoursComplete / @numSubTaskHours) * 100
	end

DELETE FROM A_TASK_COMPLETION_STATS WHERE TASK_ID = @taskID
INSERT INTO A_TASK_COMPLETION_STATS 
	(TASK_ID,PERC_COMPLETE,TIME_COMPLETE,
	DRCM,MODBY,NUM_SUB_TASKS,MY_TOT_HOURS,
	MY_COMP_HOURS,PARENT_TASK_ID,NUM_SUB_TASKS_COMPLETE,CUR_STEP_TEXT)
VALUES
	(@taskID,@percComplete,@timeComplete,
	getDate(),@strNTLogin,@numSubTasks,@numSubTaskHours,
	@numSubTaskHoursComplete,@parentID,@numSubTasksComplete,@curStepText)
declare @fillID varchar(50),@tID varchar(50)
SELECT @fillID = FILL_ITEM_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
declare @curs as CURSOR,@it varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_FILLS WHERE BATCH_PARENT = @fillID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	SELECT @tID = TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @it
	print 'updating batched task ' + @tID
	DELETE FROM A_TASK_COMPLETION_STATS WHERE TASK_ID = @tID
	INSERT INTO A_TASK_COMPLETION_STATS 
		(TASK_ID,PERC_COMPLETE,TIME_COMPLETE,
		DRCM,MODBY,NUM_SUB_TASKS,MY_TOT_HOURS,
		MY_COMP_HOURS,PARENT_TASK_ID,NUM_SUB_TASKS_COMPLETE,CUR_STEP_TEXT)
	VALUES
		(@tID,@percComplete,@timeComplete,
		getDate(),@strNTLogin,@numSubTasks,@numSubTaskHours,
		@numSubTaskHoursComplete,@parentID,@numSubTasksComplete,@curStepText)
	fetch next from @curs into @it
	end
close @curs
deallocate @curs


if @parentID is not null
	exec A_SP_TASK_FIGURE_COMPLETION_DATA @parentID,@strNTLogin

fin:

