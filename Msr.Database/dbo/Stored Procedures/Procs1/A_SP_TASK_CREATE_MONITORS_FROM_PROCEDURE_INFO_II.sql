


CREATE  PROCEDURE dbo.A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO_II 
@taskID varchar(50),
@procID varchar(50),
@stepID varchar(50),
@notShowingSteps tinyInt,
@strNTLogin varchar(50)

AS
if exists(SELECT * FROM #tempProcsAlreadyProcessed WHERE PROC_ID = @procID and STEP_ID = @stepID) goto fin
INSERT INTO #tempProcsAlreadyProcessed(PROC_ID,STEP_ID)VALUES(@procID,@stepID)
declare @showSteps varchar(50),@it nvarchar(50),@curs Cursor,@procHistID varchar(50),@newMonitorID varchar(50)

print 'Creating the monitors for a task for a procedure'
if @stepID is null
	begin
	print 'the step is null so we need to go ahead and do this for the whole procedure'
	SELECT @showSteps = STEPS_IN_AP FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @procID
	print 'show Steps = ' + isNull(@showSteps,'NULL')
	if @showSteps is null or @showSteps = 0 or @notShowingSteps = 1
		begin
		print 'We do not show the steps so we need to call this procedure for every step'
		set @curs = Cursor For SELECT STEP_ID FROM A_V_APPROVED_PROCEDURE_STEPS WHERE APPROVED_PROC_ID = @procID
		open @curs
		Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
			Begin
			print 'processig step = ' + @it
			exec A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO_II @taskID,@procID,@it,1,@strNTLogin
			Fetch Next from @curs Into @it
			End
		close @curs
		Deallocate @curs	
		end
	end
else
	begin
	print 'We are dealing with a step, so we just need to get all the monitors from the linked procedures.'
	set @curs = Cursor For SELECT PROCEDURE_LINK FROM A_PROCEDURE_STEP_PROCEDURE_LINK WHERE PROCEDURE_STEP = @stepID
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'processig proc = ' + @it
		exec A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO_II @taskID,@it,null,1,@strNTLogin
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs	
	end
	





	print 'so we made all the monitors for the sub data, now to make all the monitors for this item'
	SELECT @procHistID = HISTORY_REF_ID FROM A_PROCEDURES WHERE ID = @procID
	if @stepID is null
		begin
		print 'Step ID is Null'
		print 'SELECT ID FROM A_MONITOR_TEMPLATES WHERE PROCEDURE_ID = ''' + isNull(@procHistID,'NULL') + ''' and STEP_ID = ''' + isNull(@stepID,'NULL') + ''' and TASK_ID IS NULL'
		set @curs = Cursor For SELECT ID FROM A_MONITOR_TEMPLATES WHERE PROCEDURE_ID = @procHistID and STEP_ID is null AND TASK_ID is NULL
		end
	else
		begin
		print 'We are using this to get them '
		print 'SELECT ID FROM A_MONITOR_TEMPLATES WHERE PROCEDURE_ID = ''' + isNull(@procHistID,'NULL') + ''' and STEP_ID = ''' + @stepID + ''' and TASK_ID IS NULL'
		set @curs = Cursor For SELECT ID FROM A_MONITOR_TEMPLATES WHERE PROCEDURE_ID = @procHistID and STEP_ID = @stepID and TASK_ID IS NULL

		end

	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'processig monitor = ' + @it
		exec A_SP_MONITOR_COPY @newMonitorID OUTPUT,@it,1,@strNTLogin
		UPDATE A_MONITOR_TEMPLATES SET TASK_ID = @taskID,PROCEDURE_ID = NULL,STEP_ID = null WHERE ID = @newMonitorID
		UPDATE A_TASKS SET HAS_MONITOR = 1 WHERE ID = @taskID
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs	
	
	





	print 'Now we need to add all the info for the monitors for the main procedure or step'






fin:





