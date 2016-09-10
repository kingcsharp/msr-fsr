


CREATE    procedure dbo.A_SP_TASK_FIGURE_OUT_PROCEDURE_START_DATE_TIME 
@taskID varchar(50)
AS
declare @procID varchar(50),@stepID varchar(50),@recurNumber varchar(50),@parentID varchar(50),
	@opsd datetime,@cpsd datetime,@asd datetime,@durType varchar(50),@dur float,
	@opStop datetime,@cpStop datetime
SELECT @recurNumber = RECURSION_NUMBER,@parentID = PARENT_ID,@procID = PROCEDURE_ID,@stepID = PROCEDURE_STEP_ID FROM A_TASKS 
	WHERE ID = @taskID
SELECT @dur = DURATION,@durType = DURATION_TYPE FROM A_PROCEDURE_STEPS WHERE ID =  @stepID



if @recurNumber = 1
	begin
		if not exists (SELECT * From A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @stepID)
			begin
			print 'This one is supposed to start right when the procedure starts.'
			SELECT @opsd = ORIG_PLANNED_START_DATE, @cpsd = isNull(CUR_PLANNED_START_DATE,ORIG_PLANNED_START_DATE),
				@asd = isNull(ACTUAL_START_DATE,ORIG_PLANNED_START_DATE) FROM A_TASKS WHERE ID = @parentID
			select @opStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),@cpStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_START', @opsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_STOP', @opStop,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_START', @cpsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_STOP', @opStop,'Sys'


-- 			UPDATE A_TASKS SET 
-- 				ORIG_PLANNED_START_DATE = @opsd, 
-- 				ORIG_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),
-- 				CUR_PLANNED_START_DATE = @cpsd,
-- 				CUR_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@cpsd,@dur)
-- 				WHERE ID = @taskID
			end
		else
			begin
			print 'This one depends on some others finishing first' + @stepID
			SELECT
				
				@opsd = max(ORIG_PLANNED_STOP_DATE) 
				FROM A_TASKS 
				WHERE PARENT_ID=@parentID AND 
					RECURSION_NUMBER = @recurNumber AND
					PROCEDURE_STEP_ID IN (SELECT PREV_STEP From A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @stepID)

			select @opStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),@cpStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_START', @opsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_STOP', @opStop,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_START', @cpsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_STOP', @opStop,'Sys'

-- 			UPDATE A_TASKS SET 
-- 				ORIG_PLANNED_START_DATE = @opsd, 
-- 				ORIG_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),
-- 				CUR_PLANNED_START_DATE = @opsd,
-- 				CUR_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
-- 				WHERE ID = @taskID
			end
	end
else
	begin
	print 'this is a recur number of '
	print @recurNumber
	declare @calledFromTask varchar(50),@callStepID varchar(50),@delType varchar(50),@delUnit varchar(30),
		@delCounter varchar(50),@delVal float,@timesToRepeat int,@recurType varchar(50),
		@beginOfTaskThatCalledMe datetime,@endOfTaskThatCalledMe datetime
	SELECT @callStepID = STEP_ID,@delVal = DELAY_VALUE,@timesToRepeat = TIMES_TO_REPEAT,
		@delType = DELAY_TYPE,@delUnit = DELAY_UNIT,@delCounter = DELAY_COUNTER,@recurType = RECUR_TYPE
		FROM A_PROCEDURE_STEP_RECURRENCE WHERE NEXT_STEP = @stepID
	SELECT @calledFromTask = ID 
		FROM A_TASKS 
		WHERE RECURSION_NUMBER = (@recurNumber - 1) and PROCEDURE_STEP_ID = @callStepID and PARENT_ID = @parentID

	print 'Del Unit = '
	print @delUnit
	print 'Del val = '
	print @delVal


	if @recurType is null
		begin
			print 'this one is dependent on some others finishing first'
			SELECT 
				@opsd = max(ORIG_PLANNED_STOP_DATE) 
				FROM A_TASKS 
				WHERE PARENT_ID=@parentID AND 
					RECURSION_NUMBER = @recurNumber AND
					PROCEDURE_STEP_ID IN (SELECT PREV_STEP From A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @stepID)

			SELECT 
				*
				FROM A_TASKS 
				WHERE PARENT_ID=@parentID AND 
					RECURSION_NUMBER = @recurNumber AND
					PROCEDURE_STEP_ID IN (SELECT PREV_STEP From A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @stepID)

			select @opStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),@cpStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_START', @opsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_STOP', @opStop,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_START', @cpsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_STOP', @opStop,'Sys'

-- 			UPDATE A_TASKS SET 
-- 				ORIG_PLANNED_START_DATE = @opsd, 
-- 				ORIG_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),
-- 				CUR_PLANNED_START_DATE = @opsd,
-- 				CUR_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
-- 				WHERE ID = @taskID


		end
	else
		begin
		if @recurType = 'FROM_START'
			begin
			print 'this one needs to start the delay from the beginnuing of the task that called it'
			SELECT @beginOfTaskThatCalledMe = ORIG_PLANNED_START_DATE FROM A_TASKS WHERE ID = @calledFromTask
			set @opsd = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@delUnit,@beginOfTaskThatCalledMe,@delVal)

			select @opStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),@cpStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_START', @opsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_STOP', @opStop,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_START', @cpsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_STOP', @opStop,'Sys'


-- 				UPDATE A_TASKS SET 
-- 					ORIG_PLANNED_START_DATE = @myStart, 
-- 					ORIG_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@myStart,@dur),
-- 					CUR_PLANNED_START_DATE = @myStart,
-- 					CUR_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@myStart,@dur)
-- 					WHERE ID = @taskID
	
			
	
	
			end
		if @recurType = 'FROM_END'
			begin
			print 'this one needs to start the delay from the end of the task that called it'
			print 'Called from task = '
			print @calledFromTask
			SELECT @endOfTaskThatCalledMe = ACTUAL_STOP_DATE FROM A_TASKS WHERE ID = @calledFromTask
			print 'The End of the task that called me is '
			print @endOfTaskThatCalledMe
			set @opsd = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@delUnit,@endOfTaskThatCalledMe,@delVal)
			print 'Original Planned Start Date should be = '
			print @opsd
			select @opStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur),@cpStop = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@opsd,@dur)
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_START', @opsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'ORIG_PLANNED_STOP', @opStop,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_START', @cpsd,'Sys'
			exec A_SP_TASK_DATES_UPDATE @taskID,'CUR_PLANNED_STOP', @opStop,'Sys'


-- 				UPDATE A_TASKS SET 
-- 					ORIG_PLANNED_START_DATE = @myStart, 
-- 					ORIG_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@myStart,@dur),
-- 					CUR_PLANNED_START_DATE = @myStart,
-- 					CUR_PLANNED_STOP_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@durType,@myStart,@dur)
-- 					WHERE ID = @taskID
	
			
	
	
			end
		end
		
	





	end



