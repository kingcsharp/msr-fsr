




CREATE                 procedure A_SP_PROCEDURE_STEP_COPY
	@newStepID varchar(50) OUTPUT,
	@oldStep nvarchar(50),
	@oldProc nvarchar(50),
	@newProc nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT

exec A_SP_PROCEDURE_STEP_ACTUALLY_COPY @newID OUTPUT,
@oldStep,@oldProc,@newProc,@strNTLogin

print 'Copying the Step Recurrence info'
INSERT INTO A_PROCEDURE_STEP_RECURRENCE 
	([ID], [STEP_ID], [RECUR_TYPE], 
	[NEXT_STEP], [DELAY_TYPE], [DELAY_UNIT], 
	[DELAY_COUNTER], [DELAY_VALUE], [DRCM], 
	[MODBY], [TIMES_TO_REPEAT],SCHED_NUM)
	SELECT newID(),@newID, [RECUR_TYPE], 
	[NEXT_STEP], [DELAY_TYPE], [DELAY_UNIT], 
	[DELAY_COUNTER], [DELAY_VALUE], getDate(), 
	@strNTLogin, [TIMES_TO_REPEAT],SCHED_NUM
	FROM A_PROCEDURE_STEP_RECURRENCE 
	WHERE STEP_ID = @oldStep

declare @ns varchar(50)
SELECT @ns = NEXT_STEP FROM A_PROCEDURE_STEP_RECURRENCE 
	WHERE STEP_ID = @oldStep
declare @sql varchar(7500)
set @sql = 'UPDATE A_PROCEDURE_STEP_RECURRENCE SET NEXT_STEP = 
			(SELECT ID FROM A_PROCEDURE_STEPS WHERE OLD_STEP_ID = ''' + @newID + ''') WHERE
		STEP_ID = ''' + @newID + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @SQL,@strNTLogin
set @sql = 'UPDATE A_MONITOR_SKIP_STEPS SET STEP_ID = 
	(SELECT ID FROM A_PROCEDURE_STEPS WHERE OLD_STEP_ID = A_MONITOR_SKIP_STEPS.STEP_ID
	AND PROCEDURE_ID = (SELECT PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = ''' + @newID + ''')
	) 
	WHERE MONITOR_ID IN (SELECT ID FROM A_MONITOR_TEMPLATES WHERE STEP_ID = ''' + @newID + ''')'
declare @runTime datetime
set @runTime = dateAdd(s,20,getDate())
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @SQL,@runTime,@strNTLogin
set @sql = 'UPDATE A_PROCEDURE_STEP_RECURRENCE 
			SET NEXT_STEP = 
 			(SELECT ID 
				FROM A_PROCEDURE_STEPS 
				WHERE OLD_STEP_ID = A_PROCEDURE_STEP_RECURRENCE.NEXT_STEP
			  	AND 
		 		PROCEDURE_ID = (SELECT PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = 
			''' + @newID + ''')
			) 
 		WHERE A_PROCEDURE_STEP_RECURRENCE.STEP_ID = ''' + @newID + ''''
set @runTime = dateAdd(s,20,getDate())
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @SQL,@runTime,@strNTLogin
set @newStepID = @newID


