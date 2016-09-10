
CREATE PROCEDURE dbo.A_SP_PREPOP_STEP_COPY
@oldPrePopHistID varchar(50),
@newPrePopHistID varchar(50),
@strNTLogin nvarchar(50)
as
declare @oldStep varchar(50), @newID as varchar(50), @sql varchar(4000)
	SELECT @oldStep = PROC_STEP_ID 
	FROM A_PREPOP_HISTORY 
	WHERE ID = @oldPrePopHistID

exec A_SP_PROCEDURE_STEP_ACTUALLY_COPY 
		@newID OUTPUT,@oldStep,null,null,@strNTLogin

UPDATE A_PREPOP_HISTORY SET PROC_STEP_ID = @newID WHERE ID = @newPrePopHistID

print 'Got a new step ID = ' + @newID
print 'Copying the Step Recurrence info'
UPDATE A_MONITOR_TEMPLATES SET RELATED_OBJECT_ID = 
	(SELECT OBJECT_ID FROM A_PREPOP_HISTORY WHERE ID = @newPrePopHistID)
	WHERE STEP_ID = @newID

set @sql = 'UPDATE A_MONITOR_SKIP_STEPS SET STEP_ID = 
			(SELECT ID FROM A_PROCEDURE_STEPS WHERE OLD_STEP_ID = ''' + @oldStep + ''') WHERE
		STEP_ID = ''' + @newID + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @SQL,@strNTLogin


