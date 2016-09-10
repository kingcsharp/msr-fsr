




--This is called from:
--A_SP_PROCEDURE_COPY_ONE
--A_SP_PROCEDURE_STEP_COPY_ONE

CREATE      PROCEDURE A_SP_PROCEDURE_COPY_MONITORS
@oldProcID nvarchar(50),
@oldStepID nvarchar(50),
@newProcID nvarchar(50),
@newStepID nvarchar(50),
@strNTLogin nvarchar(50)
AS
Declare @newMonitorID as nvarchar(50)
Declare @myPID nvarchar(50)
declare @myObjID varchar(50)
Declare @pCursor Cursor

SELECT @myObjID = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @newProcID
print 'looking for monitors for procID = ' + isNull(@oldProcID,'Null') + ' AND Step ID = ' + isNull(@oldStepID,'Null')
If @newStepID is null
	set @pCursor = Cursor For SELECT ID FROM A_MONITOR_TEMPLATES WHERE PROCEDURE_ID = @oldProcID AND STEP_ID is NULL
else
	set @pCursor = Cursor For SELECT ID FROM A_MONITOR_TEMPLATES WHERE STEP_ID = @oldStepID

open @pCursor
Fetch Next from @pCursor
Into @myPID
while (@@fetch_status = 0)
Begin
	print 'Gonna Copy Monitor ID = ' + @myPID
	exec A_SP_MONITOR_COPY @newMonitorID OUTPUT,@myPID,1,@strNTLogin
	UPDATE A_MONITOR_TEMPLATES SET RELATED_OBJECT_ID = @myObjID,PROCEDURE_ID = @newProcID, STEP_ID = @newStepID WHERE ID = @newMonitorID
	Fetch Next from @pCursor Into @myPID
End
close @pCursor
Deallocate @pCursor





