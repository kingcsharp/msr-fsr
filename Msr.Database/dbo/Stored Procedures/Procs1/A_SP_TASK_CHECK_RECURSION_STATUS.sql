

CREATE        PROCEDURE dbo.A_SP_TASK_CHECK_RECURSION_STATUS
@taskID varchar(50)
AS
declare @recurNum int,@procID varchar(50),@stepID varchar(50),@parentID varchar(50),@sql varchar(4000),
@requestor varchar(50)
SELECT 
	@recurNum = RECURSION_NUMBER,
	@procID = PROCEDURE_ID,
	@stepID = PROCEDURE_STEP_ID,
	@parentID = PARENT_ID,
	@requestor = REQUESTOR
FROM A_TASKS WHERE ID = @taskID
print 'StepID = ' + @stepID
print 'parentID = ' + @parentID
print 'My Recursion number = '
print @recurNum
declare @RECUR_TYPE as varchar(50),@DELAY_TYPE as varchar(50),@SCHED_NUM int,
	@timesToRepeat int

SELECT @RECUR_TYPE = RECUR_TYPE, 
	@DELAY_TYPE = DELAY_TYPE,
	@SCHED_NUM = SCHED_NUM,
	@timesToRepeat = TIMES_TO_REPEAT	
	FROM A_PROCEDURE_STEP_RECURRENCE WHERE STEP_ID = @stepID
if @RECUR_TYPE is null
	goto fin

declare @nmberAlreadyScheduledTo int,@nmberCompleted int
SELECT @nmberAlreadyScheduledTo = max(RECURSION_NUMBER) FROM A_TASKS WHERE PARENT_ID = @parentID
	AND PROCEDURE_STEP_ID = @stepID
print 'max Recursion number in the tasks = '
print @nmberAlreadyScheduledTo
SELECT @nmberCompleted = isnull(max(RECURSION_NUMBER),0) FROM A_TASKS WHERE PARENT_ID = @parentID
	AND PROCEDURE_STEP_ID = @stepID and status in ('COMPLETED','FINISHED','CLOSED')
print 'Max Recur Number in tasks that are already finished = '
print @nmberCompleted
print 'I am supposed to schedule out this many = '
print @SCHED_NUM
print 'I am supposed to repeat this many times = '
print @timesToRepeat
declare @nextStepID varchar(50),@nextStepParentID varchar(50),@myParentID varchar(50)
if @RECUR_TYPE = 'FROM_START'
	begin
	if (@nmberAlreadyScheduledTo - @nmberCompleted > @SCHED_NUM) OR
		(@timesToRepeat <> -1 and @nmberAlreadyScheduledTo >= @timesToRepeat)
		begin
		print 'this one is already scheduled out far enough'
		goto fin
		end
	print 'We have a recurrence to schedule' + @RECUR_TYPE + ' ' + @DELAY_TYPE
	SELECT @nextStepID = NEXT_STEP FROM A_PROCEDURE_STEP_RECURRENCE WHERE STEP_ID = @stepID
	SELECT @nextStepParentID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @nextStepID
	SELECT @myParentID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @stepID
	if @myParentID <> @nextStepParentID
		goto ProcedureMatchError

	set @recurNum = @nmberAlreadyScheduledTo + 1
	exec A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP
		@nextStepID,@recurNum,@parentID,@stepID
	end
if @RECUR_TYPE = 'FROM_END'
	begin
	print 'this is a from end so we are processing it now'
	if (@timesToRepeat <> -1 and @nmberAlreadyScheduledTo >= @timesToRepeat + @recurNum)
		begin
			print 'This one has already been scheduled out enough'
			goto fin
		end
		
		


	if (@nmberAlreadyScheduledTo - @nmberCompleted = 0)
		begin
		print 'The number completed = the number already scheduled so maybe need to schedule next one'
		if (@timesToRepeat <> -1 and @nmberAlreadyScheduledTo >= @timesToRepeat + @recurNum)
			begin
			print 'this one has already been done the correct number of times.'
			goto fin
			end
		print 'It appears we need to schedule another one.'
		print 'We have a recurrence to schedule' + @RECUR_TYPE + ' ' + @DELAY_TYPE
		SELECT @nextStepID = NEXT_STEP FROM A_PROCEDURE_STEP_RECURRENCE WHERE STEP_ID = @stepID
		SELECT @nextStepParentID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @nextStepID
		SELECT @myParentID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @stepID
		if @myParentID <> @nextStepParentID
			goto ProcedureMatchError
		set @recurNum = @nmberAlreadyScheduledTo + 1
		exec A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP
			@nextStepID,@recurNum,@parentID,@stepID
		end
	end


goto fin
procedureMatchError:
print 'The goto step and the recur step procedures do not match.  Please Edit the procedure to make it correct'

fin: 


